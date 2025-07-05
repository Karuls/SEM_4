#include <iostream>
#include <ctime>

using namespace std;

char* generateRandomString(int size) {
	char* str = (char*)malloc(sizeof(char) * size);
	for (int i = 0; i < size; i++) {
		str[i] = rand() % 26 + 'a';
	}
	return str;
}

int main()
{
	setlocale(LC_ALL, "RU");
	char* str1 = generateRandomString(300);
	cout << "Первая строка: ";
	for (int i = 0; i < 300; i++) {
		if (i % 50 == 0) {
			cout << endl;
		}
		cout << str1[i];
	}
	cout << endl;
	char* str2 = generateRandomString(200);
	cout << "Вторая строка: ";
	for (int i = 0; i < 200; i++) {
		if (i % 50 == 0) {
			cout << endl;
		}
		cout << str2[i];
	}
}