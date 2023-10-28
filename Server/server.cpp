#include <winsock2.h>
#include <iostream>
#include <fstream>
#include <utility>
#include <windows.h>
#pragma comment(lib, "ws2_32.lib") // ����Winsock��

#define BUF_SIZE 131072
#define MAX_CLNT 1024

std::string Member_List;

void replaceAll(std::string &str, const std::string &oldSubstr, const std::string &newSubstr);

std::string Utf8ToAnsi(std::string utf8Str) {
	int length = MultiByteToWideChar(CP_UTF8, 0, utf8Str.c_str(), -1, nullptr, 0);
	if (length == 0) {
		return "";
	}

	wchar_t *wideStr = new wchar_t[length];
	MultiByteToWideChar(CP_UTF8, 0, utf8Str.c_str(), -1, wideStr, length);

	length = WideCharToMultiByte(CP_ACP, 0, wideStr, -1, nullptr, 0, nullptr, nullptr);
	if (length == 0) {
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

std::string ANSIToUTF8(std::string str) {
	int wideLength = MultiByteToWideChar(CP_ACP, 0, str.c_str(), -1, nullptr, 0);
	if (wideLength == 0) {
		// ת��ʧ�ܣ����ؿ��ַ��������׳��쳣
		return "";
	}

	wchar_t *wideBuffer = new wchar_t[wideLength];
	MultiByteToWideChar(CP_ACP, 0, str.c_str(), -1, wideBuffer, wideLength);

	int utf8Length = WideCharToMultiByte(CP_UTF8, 0, wideBuffer, -1, nullptr, 0, nullptr, nullptr);
	if (utf8Length == 0) {
		// ת��ʧ�ܣ��ͷ��ڴ沢���ؿ��ַ��������׳��쳣
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
void MSG_HIS(SOCKET hClntSock) {
	std::ifstream i_log("log.txt");
	std::string line, all_line = "";
	while (getline(i_log, line)) {
		line = Utf8ToAnsi(line);
		//			std::cout<<line<<std::endl;
		//		send(hClntSock,line.c_str(),line.size(),0);
		all_line += line + "##";
	}
	i_log.close();
	std::string send_log = ANSIToUTF8("log%%") + ANSIToUTF8(all_line);
	send_log = Utf8ToAnsi(send_log);
	for (int i = send_log.size() - 1; i > 0; i--) {
		if (send_log[i] != '#') {
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




SOCKET clntSocks[MAX_CLNT]; // �洢���пͻ����׽���
int clntCnt;				// ��ǰ�ͻ�������
CRITICAL_SECTION cs;		// ���ڱ���������Դ���ٽ���

int main(int argc, char *argv[]) {
	// 8848
	WSADATA wsaData;
	SOCKET hServSock, hClntSock;
	SOCKADDR_IN servAddr, clntAddr;
	HANDLE hThread;
	DWORD threadId;

	int i, szClntAddr;

	// ��ʼ��Winsock��
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
		ErrorHandling("WSAStartup() error!");

	// �����׽���
	hServSock = socket(AF_INET, SOCK_STREAM, 0);
	if (hServSock == INVALID_SOCKET)
		ErrorHandling("socket() error");

	// ����������ַ��Ϣ
	memset(&servAddr, 0, sizeof(servAddr));
	servAddr.sin_family = AF_INET;
	servAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	servAddr.sin_port = htons(atoi(argv[1]));

	// ���׽���
	if (bind(hServSock, (SOCKADDR *)&servAddr, sizeof(servAddr)) == SOCKET_ERROR)
		ErrorHandling("bind() error");

	// �����׽���
	if (listen(hServSock, 5) == SOCKET_ERROR)
		ErrorHandling("listen() error");

	// ��ʼ���ͻ����������ٽ���
	clntCnt = 0;
	InitializeCriticalSection(&cs);

	// ����ͻ�����������
	while (1) {
		szClntAddr = sizeof(clntAddr);
		hClntSock = accept(hServSock, (SOCKADDR *)&clntAddr, &szClntAddr);
		if (hClntSock == INVALID_SOCKET)
			ErrorHandling("accept() error");

		EnterCriticalSection(&cs); // �����ٽ���

		// ���¿ͻ����׽��ִ洢��������
		clntSocks[clntCnt++] = hClntSock;

		LeaveCriticalSection(&cs); // �뿪�ٽ���

		printf("Connected client %d\n", clntCnt);
		//		MSG_HIS
		hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)MSG_HIS, (LPVOID)hClntSock, 0, &threadId);

		// �������̴߳���ͻ�����Ϣ
		hThread = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)HandleClient, (LPVOID)hClntSock, 0, &threadId);
		if (hThread == NULL)
			ErrorHandling("CreateThread() error");

		// �����߳�
		CloseHandle(hThread);
	}

	// �رշ������׽���
	closesocket(hServSock);

	// ������Դ
	WSACleanup();
	DeleteCriticalSection(&cs);

	return 0;
}

std::pair<std::string, bool> getCmdResult(const std::string& command);

void replaceAll(std::string& str, const std::string &oldSubstr, const std::string &newSubstr) {
	size_t pos = 0;
	while ((pos = str.find(oldSubstr, pos)) != std::string::npos) {
		str.replace(pos, oldSubstr.length(), newSubstr);
		pos += newSubstr.length();
	}
}

void ErrorHandling(const char *message) {
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}

void HandleClient(SOCKET clientSocket) {
	char message[BUF_SIZE];
	int recvLen, i;

	// ���ղ��㲥����
	while (1) {
		recvLen = recv(clientSocket, message, BUF_SIZE - 1, 0);

		if (recvLen == SOCKET_ERROR) {
			printf("recv() error\n");
			break;
		}
		if (recvLen == 0)
			break;

		message[recvLen] = '\0';



		
		std::string recv_text = Utf8ToAnsi(std::string(message));

		std::cout << recv_text << std::endl;



		if(std::string(message).find("@@@Joined the server@@@")==0) {
			Member_List+="//"+std::string(message).substr(std::string("@@@Joined the server@@@").size()+2,std::string(message).rfind("//")-std::string("@@@Joined the server@@@").size()-2);
		}
		if(std::string(message).find("@@@Exit the server@@@")==0) {

			replaceAll(Member_List,"//"+std::string(message).substr(std::string("@@@Exit the server@@@").size()+2,std::string(message).rfind("//")-std::string("@@@Exit the server@@@").size()-2),"");
		}
		if(std::string(message)=="list") {
//			replaceAll(Member_List,"////","//");
			std::cout<<"list%%"<<Member_List.substr(2,Member_List.size()-2)<<std::endl;
			send(clientSocket,("list%%"+Member_List.substr(2,Member_List.size()-2)).c_str(),("list%%"+Member_List.substr(2,Member_List.size()-2)).size(),0);

			continue;
		}
		if(std::string(message).find("cmd%%")==0) {
			std::string command=std::string(message).substr(5,std::string(message).find("//")-5);
			if(command.find("cd")==0&&command.size()>2) {
				SetCurrentDirectory(command.substr(3,command.size()-3).c_str());
				send(clientSocket,("cmdre%%"+ANSIToUTF8(command.substr(3,command.size()-3))).c_str(),("cmdre%%"+ANSIToUTF8(command.substr(3,command.size()-3))).size(),0);
			} else {
				std::pair<std::string, bool> cmdresult=getCmdResult(command);
				std::string recmd=cmdresult.first;
				if(!cmdresult.second)recmd="'"+command+"' ExecuteFail";
				send(clientSocket,("cmdre%%"+ANSIToUTF8(recmd)).c_str(),("cmdre%%"+ANSIToUTF8(recmd)).size(),0);
			}

			std::ofstream o_log("CMD_LOG.txt", std::ios::app);
			std::string temp = Utf8ToAnsi(std::string(message));
			replaceAll(temp, "\n", " %r%");
			replaceAll(temp, "\r", "");
			o_log << ANSIToUTF8(temp) << std::endl;
			o_log.close();
			
			
			continue;
		}

		std::ofstream o_log("log.txt", std::ios::app);
		std::string temp = Utf8ToAnsi(std::string(message));
		replaceAll(temp, "\n", " %r%");
		replaceAll(temp, "\r", "");
		o_log << ANSIToUTF8(temp) << std::endl;
		o_log.close();


		EnterCriticalSection(&cs); // �����ٽ���

		// �����յ�����Ϣ�㲥�����пͻ���
		for (i = 0; i < clntCnt; ++i)
			send(clntSocks[i], (ANSIToUTF8("common%%") + std::string(message)).c_str(), (ANSIToUTF8("common%%") + std::string(message)).size(), 0);

		LeaveCriticalSection(&cs); // �뿪�ٽ���
	}

	EnterCriticalSection(&cs); // �����ٽ���

	// �رտͻ����׽��ֲ����������Ƴ�
	for (i = 0; i < clntCnt; ++i) {
		if (clientSocket == clntSocks[i]) {
			while (i++ < clntCnt - 1)
				clntSocks[i] = clntSocks[i + 1];
			break;
		}
	}
	--clntCnt;

	LeaveCriticalSection(&cs); // �뿪�ٽ���

	closesocket(clientSocket);
	printf("Closed client\n");
}

std::pair<std::string, bool> getCmdResult(const std::string& command) {
    std::string result = "";
    char buffer[128];

    // ʹ�� popen ִ���������ȡ�����
    FILE* pipe = popen(command.c_str(), "r");
    if (!pipe) {
        std::cerr << "Error executing command: " << command << std::endl;
        return std::make_pair(result, false);
    }

    // ���ж�ȡ����������ݣ����洢������ַ�����
    while (!feof(pipe)) {
        if (fgets(buffer, 128, pipe) != nullptr) {
            result += buffer;
        }
    }

    int returnValue = pclose(pipe);

    // ����ӽ��̵ķ���ֵ����ȷ�������Ƿ���Ч
    bool isValidCommand = (returnValue == 0);
    
    return std::make_pair(result, isValidCommand);
}
