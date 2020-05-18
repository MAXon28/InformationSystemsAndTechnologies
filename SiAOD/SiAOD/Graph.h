#pragma once
#include <string>
#include <vector>

using namespace std; 

class Graph 
{
    private:
        int countTops; // Количество вершин
        int** adjacencyMatrix; // Матрица смежности

        void FillingMatrix(); // Заполнение матрицы
    public:
        Graph(int size);
        bool HaveGraphLoops(); // Имеет ли граф петли
        vector<int> GetIsolatedTops(); // Изолированные вершины
        vector<int> GetGraphDegrees(); // Степени вершин графов
        void GetSequenceOfGraphEdges(); // Последовательности рёбер графа
};