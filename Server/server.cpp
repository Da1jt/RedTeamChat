#include <winsock2.h>
#include <iostream>
#include <fstream>
#pragma comment(lib, "ws2_32.lib") // 链接Winsock库

#define BUF_SIZE 131072
#define MAX_CLNT 1024

void replaceAll(std::string &str, const std::string &oldSubstr, const std::string &newSubstr);

std::string Utf8ToAnsi(std::string utf8Str)
{
	int length = MultiByteToWideChar(CP_UTF8, 0, utf8Str.c_str(), -1, nullptr, 0);
	if (length == 0)
	{
		return "";
	}

	wchar_t *wideStr = new wchar_t[length];
	MultiByteToWideChar(CP_UTF8, 0, utf8Str.c_str(), -1, wideStr, length);

	length = WideCharToMultiByte(CP_ACP, 0, wideStr, -1, nullptr, 0, nullptr, nullptr);
	if (length == 0)
	{
		delete[] wideStr;
		return "";
	}

	char *ansiStr = new char[length];
	WideCharToMultiByte(CP_ACP, 0, wideStr, -1, ansiStr, length, nullptr, nullptr);

	std::string result(ansiStr);

	delete[] wideStr;
	delete[] ansiStr;

	return result;
}

std::string ANSIToUTF8(std::string str)
{
	int wideLength = MultiByteToWideChar(CP_ACP, 0, str.c_str(), -1, nullptr, 0);
	if (wideLength == 0)
	{
		// 转换失败，返回空字符串或者抛出异常
		return "";
	}

	wchar_t *wideBuffer = new wchar_t[wideLength];
	MultiByteToWideChar(CP_ACP, 0, str.c_str(), -1, wideBuffer, wideLength);

	int utf8Length = WideCharToMultiByte(CP_UTF8, 0, wideBuffer, -1, nullptr, 0, nullptr, nullptr);
	if (utf8Length == 0)
	{
		// 转换失败，释放内存并返回空字符串或者抛出异常
		delete[] wideBuffer;
		return "";
	}

	char *utf8Buffer = new char[utf8Length];
	WideCharToMultiByte(CP_UTF8, 0, wideBuffer, -1, utf8Buffer, utf8Length, nullptr, nullptr);

	std::string utf8String(utf8Buffer);

	delete[] wideBuffer;
	delete[] utf8Buffer;

	return utf8String;
}
void ErrorHandling(const char *message);
void HandleClient(SOCKET clientSocket);
void MSG_HIS(SOCKET hClntSock)
{
	std::ifstream i_log("log.txt");
	std::string line, all_line = "";
	while (getline(i_log, line))
	{
		line = Utf8ToAnsi(line);
		//			std::cout<<line<<std::endl;
		//		send(hClntSock,line.c_str(),line.size(),0);
		all_line += line + "##";
	}
	i_log.close();
	std::string send_log = ANSIToUTF8("log%%") + ANSIToUTF8(all_line);
	send_log = Utf8ToAnsi(send_log);
	for (int i = send_log.size() - 1; i > 0; i--)
	{
		if (send_log[i] != '#')
		{
			send_log = send_log.substr(0, i + 1);
			break;
		}
	}
	send_log = ANSIToUTF8(send_log);
	//	replaceAll(send_log,ANSIToUTF8("\n"),ANSIToUTF8("%r%"));
	//	replaceAll(send_log,ANSIToUTF8("\r"),ANSIToUTF8(""));
	std::cout << "LOG: " << Utf8ToAnsi(send_log) << std::endl;
	send(hClntSock, send_log.c_str(), send_log.size(), 0);
}

SOCKET clntSocks[MAX_CLNT]; // 存储所有客户端套接字
int clntCnt;				// 当前客户端数量
CRITICAL_SECTION cs;		// 用于保护共享资源的临界区

int main(int argc, char *argv[])
{ // 8848
	WSADATA wsaData;
	SOCKET hServSock, hClntSock;
	SOCKADDR_IN servAddr, clntAddr;
	HANDLE hThread;
	DWORD threadId;

	int i, szClntAddr;

	// 初始化Winsock库
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
		ErrorHandling("WSAStartup() error!");

	// 创建套接字
	hServSock = socket(AF_INET, SOCK_STREAM, 0);
	if (hServSock == INVALID_SOCKET)
		ErrorHandling("socket() error");

	// 填充服务器地址信息
	memset(&servAddr, 0, sizeof(servAddr));
	servAddr.sin_family = AF_INET;
	servAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	servAddr.sin_port = htons(atoi(argv[1]));

	// 绑定套接字
	if (bind(hServSock, (SOCKADDR *)&servAddr, sizeof(servAddr)) == SOCKET_ERROR)
		ErrorHandling("bind() error");

	// 监听套接字
	if (listen(hServSock, 5) == SOCKET_ERROR)
		ErrorHandling("listen() error");

	// 初始化客户端数量和临界区
	clntCnt = 0;
	InitializeCriticalSection(&cs);

	// 处理客户端连接请求
	while (1)
	{
		szClntAddr = sizeof(clntAddr);
		hClntSock = accept(hServSock, (SOCKADDR *)&clntAddr, &szClntAddr);
		if (hClntSock == INVALID_SOCKET)
			ErrorHandling("accept() error");

		EnterCriticalSection(&cs); // 进入临界区

		// 将新客户端套接字存储在数组中
		clntSocks[clntCnt++] = hClntSock;

		LeaveCriticalSection(&cs); // 离开临界区

		printf("Connected client %d\n", clntCnt);
		//		MSG_HIS
		hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)MSG_HIS, (LPVOID)hClntSock, 0, &threadId);

		// 创建新线程处理客户端消息
		hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)HandleClient, (LPVOID)hClntSock, 0, &threadId);
		if (hThread == NULL)
			ErrorHandling("CreateThread() error");

		// 分离线程
		CloseHandle(hThread);
	}

	// 关闭服务器套接字
	closesocket(hServSock);

	// 清理资源
	WSACleanup();
	DeleteCriticalSection(&cs);

	return 0;
}

void replaceAll(std::string &str, const std::string &oldSubstr, const std::string &newSubstr)
{
	size_t pos = 0;
	while ((pos = str.find(oldSubstr, pos)) != std::string::npos)
	{
		str.replace(pos, oldSubstr.length(), newSubstr);
		pos += newSubstr.length();
	}
}

void ErrorHandling(const char *message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}

void HandleClient(SOCKET clientSocket)
{
	char message[BUF_SIZE];
	int recvLen, i;

	// 接收并广播数据
	while (1)
	{
		recvLen = recv(clientSocket, message, BUF_SIZE - 1, 0);

		if (recvLen == SOCKET_ERROR)
		{
			printf("recv() error\n");
			break;
		}
		if (recvLen == 0)
			break;

		message[recvLen] = '\0';
		
		std::ofstream o_log("log.txt", std::ios::app);
		std::string temp = Utf8ToAnsi(std::string(message));
		replaceAll(temp, "\n", " %r%");
		replaceAll(temp, "\r", "");
		o_log << ANSIToUTF8(temp) << std::endl;
		o_log.close();

		std::string recv_text = Utf8ToAnsi(std::string(message));

		std::cout << recv_text << std::endl;

		EnterCriticalSection(&cs); // 进入临界区

		// 将接收到的消息广播给所有客户端
		for (i = 0; i < clntCnt; ++i)
			send(clntSocks[i], (ANSIToUTF8("common%%") + std::string(message)).c_str(), (ANSIToUTF8("common%%") + std::string(message)).size(), 0);

		LeaveCriticalSection(&cs); // 离开临界区
	}

	EnterCriticalSection(&cs); // 进入临界区

	// 关闭客户端套接字并从数组中移除
	for (i = 0; i < clntCnt; ++i)
	{
		if (clientSocket == clntSocks[i])
		{
			while (i++ < clntCnt - 1)
				clntSocks[i] = clntSocks[i + 1];
			break;
		}
	}
	--clntCnt;

	LeaveCriticalSection(&cs); // 离开临界区

	closesocket(clientSocket);
	printf("Closed client\n");
}
