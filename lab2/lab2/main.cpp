#include <iostream>
#include "Tree.h"
#include <iomanip>
#define t 50        // кількість елементів у дереві
#define N 15        // кількість замірів
#define COUNT 100   // максимальний діапазон чисел
int main()
{
    Tree tree;
    double mass[N], time_spent, ser = 0;
    clock_t begin = clock(), end;
    bool find;
    for (int i = 0; i < t; i++)
    {
        tree.insert(rand() % COUNT);
    }
    for (int i = 0; i < N; i++)
    {
        find = tree.search(rand() % COUNT);
        if (find)
        {
            std::cout << "+";
        }
        else
        {
            std::cout << "-";
        }
        end = clock();
        mass[i] = (double)(end - begin) / CLOCKS_PER_SEC;
        std::cout << std::fixed << std::setprecision(10) << " " << mass[i] << " sec" << std::endl;
        begin = end;
    }
    for (int i = 0; i < N; i++)
    {
        ser += mass[i];
    }
    ser /= N;
    std::cout << std::endl << "Avarage time of finds: " << ser;
    return 0;
}