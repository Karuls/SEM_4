#include <iostream>
#include <stack>
#include <queue>
#include <vector>
#include <algorithm>

using namespace std;

const int V = 7;

int matrix[V][V] =
{
    {0, 0, 0, 1, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0},
    {0, 0, 0, 0, 0, 0, 0},
    {0, 1, 0, 0, 1, 1, 1},
    {0, 1, 0, 0, 0, 0, 1},
    {0, 0, 1, 0, 0, 0, 1},
    {0, 0, 0, 0, 0, 0, 0}
};

void printMatix(int matrix[V][V])
{
    cout << "Матрица смежности: " << endl;
    for (int i = 0; i < V; i++) {
        for (int j = 0; j < V; j++) {
            cout << matrix[i][j] << " ";
        }
        cout << endl;
    }
}

void BFS_by_matrix(int start) {
    queue<int> q;
    bool visited[V] = { false };
    q.push(start);
    visited[start] = true;
    while (!q.empty()) {
        int current = q.front();
        q.pop();
        cout << current << " ";
        for (int i = 0; i < V; i++) {
            if (matrix[current][i] == 1 && !visited[i]) {
                q.push(i);
                visited[i] = true;
            }
        }
    }
}

void DFS_by_matrix(int start, vector<bool>& visited, stack<int>& stack) {
    visited[start] = true;
    for (int i = 0; i < V; i++) {
        if (matrix[start][i] == 1 && !visited[i]) {
            DFS_by_matrix(i, visited, stack);
        }
    }
    stack.push(start);
}

void topologicalSort() {
    stack<int> stack;
    vector<bool> visited(V, false);

    for (int i = 0; i < V; i++) {
        if (!visited[i]) {
            DFS_by_matrix(i, visited, stack);
        }
    }

    cout << "Топологическая сортировка: ";
    while (!stack.empty()) {
        cout << stack.top() << " ";
        stack.pop();
    }
    cout << endl;
}

void printAdjacencyList() {
    cout << "Список смежных вершин:" << endl;
    for (int i = 0; i < V; i++) {
        cout << i << ": ";
        for (int j = 0; j < V; j++) {
            if (matrix[i][j] == 1) {
                cout << j << " ";
            }
        }
        cout << endl;
    }
}

void printEdgeList() {
    cout << "Список ребер:" << endl;
    for (int i = 0; i < V; i++) {
        for (int j = 0; j < V; j++) {
            if (matrix[i][j] == 1) {
                cout << "(" << i << " -> " << j << ") ";
            }
        }
    }
    cout << endl;
}

int main() {
    setlocale(LC_ALL, "RU");

    int start = 0;

    printMatix(matrix);

    cout << "Обход в ширину (BFS): ";
    BFS_by_matrix(start);
    cout << endl;

    cout << "Обход в глубину (DFS): ";
    vector<bool> visited(V, false);
    stack<int> stack;
    DFS_by_matrix(start, visited, stack);
    while (!stack.empty()) {
        cout << stack.top() << " ";
        stack.pop();
    }
    cout << endl;

    topologicalSort();

    printAdjacencyList();  
    printEdgeList();       

    return 0;
}