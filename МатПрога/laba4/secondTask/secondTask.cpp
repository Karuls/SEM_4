//#include <algorithm>
//#include <iostream>
//#include <ctime>
//#include <iomanip>
//#include "Levenshtein.h"
//int main()
//{
//    setlocale(LC_ALL, "rus");
//    clock_t t1 = 0, t2 = 0, t3, t4;
//    char x[] = "abcdefghklmnoxm", y[] = "xyabcdefghomnkm";
//    int  lx = sizeof(x) - 1, ly = sizeof(y) - 1;
//    std::cout << std::endl;
//    std::cout << std::endl << "-- расстояние Левенштейна -----" << std::endl;
//    std::cout << std::endl << "--длина --- рекурсия -- дин.програм. ---"
//        << std::endl;
//    for (int i = 8; i < std::min(lx, ly); i++)
//    {
//
//        t1 = clock(); levenshtein_r(i, x, i - 2, y); t2 = clock();
//        t3 = clock(); levenshtein(i, x, i - 2, y); t4 = clock();
//        std::cout << std::right << std::setw(2) << i - 2 << "/" << std::setw(2) << i
//            << "        " << std::left << std::setw(10) << (t2 - t1)
//            << "   " << std::setw(10) << (t4 - t3) << std::endl;
//    }
//    system("pause");
//    return 0;
//}

#include <iostream>
#include <ctime>
#include <iomanip>
#include <algorithm>

int min3(int x1, int x2, int x3) {
    return std::min(std::min(x1, x2), x3);
}

int levenshtein_recursive(int lx, const char x[], int ly, const char y[]) {
    if (lx == 0) return ly;
    if (ly == 0) return lx;

    int cost = (x[lx - 1] == y[ly - 1]) ? 0 : 1;

    return min3(
        levenshtein_recursive(lx - 1, x, ly, y) + 1,      // удаление
        levenshtein_recursive(lx, x, ly - 1, y) + 1,      // вставка
        levenshtein_recursive(lx - 1, x, ly - 1, y) + cost // замена
    );
}

int main() {
    setlocale(LC_ALL, "rus");
    clock_t t1, t2;
    char x[] = "abcdefghklmnoxm", y[] = "xyabcdefghomnkm";
    int lx = sizeof(x) - 1, ly = sizeof(y) - 1;

    std::cout << "\n-- расстояние Левенштейна (рекурсия) --\n";

    for (int i = 8; i < std::min(lx, ly); i++) {
        t1 = clock();
        int distance = levenshtein_recursive(i, x, i - 2, y);
        t2 = clock();

        std::cout << "Длина " << i - 2 << "/" << i << " Время: " << (t2 - t1) << " мс" << std::endl;
    }

    system("pause");
    return 0;
}
