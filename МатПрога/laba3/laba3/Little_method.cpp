#include <iostream>
#include <iomanip> 
#include "TSP.h"
#define N 5

using namespace std;

int main()
{
	setlocale(LC_ALL, "rus");
	int d[N][N] = {   
					{ INF, 14, 28, INF, 7 },    
					{ 7, INF, 22, 61, 77 },    
					{ 9,  21, INF, 86, 56 },   
					{ 24, 51, 28, INF, 21 },   
					{ 86, 73, 52, 20, INF } 
	};   
	int r[N];
	int s = salesman(
		N,          
		(int*)d,    
		r             
	);
	cout << "\n-- Задача коммивояжера -- ";
	cout << "\n-- количество  городов: " << N;
	cout << "\n-- матрица расстояний : ";

	for (int i = 0; i < N; i++)
	{
		cout << "\n";
		for (int j = 0; j < N; j++)

			if (d[i][j] != INF) cout << setw(3) << d[i][j] << " ";

			else cout << setw(3) << "INF" << " ";
	}

	cout << "\n-- оптимальный маршрут: ";
	for (int i = 0; i < N; i++)
		cout << r[i] + 1 << "-->";

	cout << 1;
	cout << "\n-- длина маршрута     : " << s << "\n";
	system("pause");
	return 0;
}

//#include <iostream>
//#include <limits>
//using namespace std;
//const int N = 5;
//
//int final_path[N + 1];
//
//bool visited[N];
//
//int final_res = INT_MAX;
//
//void copyToFinal(int curr_path[])
//{
//	for (int i = 0; i < N; i++)
//		final_path[i] = curr_path[i];
//	final_path[N] = curr_path[0];
//}
//
//int firstMin(int matrix[N][N], int i)
//{
//	int min = INT_MAX;
//	for (int k = 0; k < N; k++)
//		if (matrix[i][k] < min && i != k)
//			min = matrix[i][k];
//	return min;
//}
//
//int secondMin(int matrix[N][N], int i)
//{
//	int first = INT_MAX, second = INT_MAX;
//	for (int j = 0; j < N; j++)
//	{
//		if (i == j)
//			continue;
//
//		if (matrix[i][j] <= first)
//		{
//			second = first;
//			first = matrix[i][j];
//		}
//		else if (matrix[i][j] <= second &&
//			matrix[i][j] != first)
//			second = matrix[i][j];
//	}
//	return second;
//}
//
//void TSPRec(int matrix[N][N], int curr_bound, int curr_weight,
//	int level, int curr_path[])
//{
//	if (level == N)
//	{
//		if (matrix[curr_path[level - 1]][curr_path[0]] != 0)
//		{
//			int curr_res = curr_weight +
//				matrix[curr_path[level - 1]][curr_path[0]];
//
//			if (curr_res < final_res)
//			{
//				copyToFinal(curr_path);
//				final_res = curr_res;
//			}
//		}
//		return;
//	}
//
//	for (int i = 0; i < N; i++)
//	{
//		if (matrix[curr_path[level - 1]][i] != 0 &&
//			visited[i] == false)
//		{
//			int temp = curr_bound;
//			curr_weight += matrix[curr_path[level - 1]][i];
//
//			if (level == 1)
//				curr_bound -= ((firstMin(matrix, curr_path[level - 1]) +
//					firstMin(matrix, i)) / 2);
//			else
//				curr_bound -= ((secondMin(matrix, curr_path[level - 1]) +
//					firstMin(matrix, i)) / 2);
//
//			if (curr_bound + curr_weight < final_res)
//			{
//				curr_path[level] = i;
//				visited[i] = true;
//
//				TSPRec(matrix, curr_bound, curr_weight, level + 1,
//					curr_path);
//			}
//
//			curr_weight -= matrix[curr_path[level - 1]][i];
//			curr_bound = temp;
//
//			memset(visited, false, sizeof(visited));
//			for (int j = 0; j <= level - 1; j++)
//				visited[curr_path[j]] = true;
//		}
//	}
//}
//
//void TSP(int matrix[N][N])
//{
//	int curr_path[N + 1];
//
//	int curr_bound = 0;
//	memset(curr_path, -1, sizeof(curr_path));
//	memset(visited, 0, sizeof(curr_path));
//
//	for (int i = 0; i < N; i++)
//		curr_bound += (firstMin(matrix, i) +
//			secondMin(matrix, i));
//
//	curr_bound = (curr_bound & 1) ? curr_bound / 2 + 1 :
//		curr_bound / 2;
//
//	visited[0] = true;
//	curr_path[0] = 0;
//
//	TSPRec(matrix, curr_bound, 0, 1, curr_path);
//}
//
//int main()
//{
//	setlocale(LC_ALL, "RU");
//	int matrix[N][N] = { 
//		{0, 14, 28, 0, 7},
//		{7, 0, 22, 61, 77},
//		{9, 21, 0, 86, 56},
//		{24, 51, 28, 0, 21},
//		{86, 73, 52, 20, 0},
//	};
//
//	TSP(matrix);
//
//	cout << "Длина минимального маршрута : " << final_res << endl;
//	cout << "Последовательность вершин : ";
//	for (int i = 0; i <= N; i++)
//		cout << final_path[i] << " ";
//
//	return 0;
//}
