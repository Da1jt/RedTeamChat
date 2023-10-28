#include <winsock2.h>
#include <windows.h>
#include <tchar.h>
#include <stdio.h>
#include <time.h>

void SendPacket(const char* ip, int port, const char* content) {
    WSADATA wsaData;
    if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) {
        printf("Failed to initialize Winsock.\n");
        return;
    }

    SOCKET clientSocket = socket(AF_INET, SOCK_STREAM, 0);
    if (clientSocket == INVALID_SOCKET) {
        printf("Failed to create socket.\n");
        WSACleanup();
        return;
    }

    struct sockaddr_in serverAddr;
    serverAddr.sin_family = AF_INET;
    serverAddr.sin_port = htons(port);
    serverAddr.sin_addr.s_addr = inet_addr(ip);

    if (connect(clientSocket, (struct sockaddr*)&serverAddr, sizeof(serverAddr)) == SOCKET_ERROR) {
        printf("Failed to connect.\n");
        closesocket(clientSocket);
        WSACleanup();
        return;
    }

    if (send(clientSocket, content, strlen(content), 0) == SOCKET_ERROR) {
        printf("Failed to send packet.\n");
        closesocket(clientSocket);
        WSACleanup();
        return;
    }

    printf("Packet sent successfully.\n");

    closesocket(clientSocket);
    WSACleanup();
}

int IsWindowExist(const wchar_t* windowName) {
    HWND hWnd = FindWindowW(NULL, windowName);
    return (hWnd != NULL);
}

const char* getCurrentTime() {
    // 获取当前时间
    time_t tnow;
    time(&tnow);

    // 将时间转换为本地时间
    struct tm* ltime = localtime(&tnow);

    // 格式化时间字符串，存储到timeStr字符数组中
    static char timeStr[20];
    sprintf(timeStr, "%d/%d/%d %d:%d:%d",
            ltime->tm_year + 1900, ltime->tm_mon + 1, ltime->tm_mday,
            ltime->tm_hour, ltime->tm_min, ltime->tm_sec);

    // 返回格式化后的时间字符串
    return timeStr;
}

int main(int argc, char* argv[]) {
    if (argc != 4) return 1;
    while (IsWindowExist(L"RedTeamChat") == 1) {
        Sleep(5000);
    }
    char packetContent[1000];
    sprintf(packetContent, "@@@Exit the server@@@//%s//%s", argv[3], getCurrentTime());
    SendPacket(argv[1], atoi(argv[2]), packetContent);
    return 0;
}

