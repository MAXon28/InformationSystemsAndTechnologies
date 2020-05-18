#include <iostream>
#include "Graph.h" 

using namespace std;

int main()
{
   setlocale(LC_ALL, "Russian");
   cout << "Введите количество вершин: ";
   int countNode;
   cin >> countNode;
   Graph graph(countNode);
   cout << "Есть ли у графа петли?";
   bool isTrue = graph.HaveGraphLoops();
   if (isTrue)
   {
       cout << " Да" << endl;
   }
   else
   {
       cout << " Нет" << endl;
   }
   cout << "Список изолированных вершин: ";
   vector<int> tops = graph.GetIsolatedTops();
   if (tops.size() == 0)
   {
       cout << "нет изолированных вершин" << endl;
   }
   else
   {
       for (int i = 0; i < tops.size(); i++)
       {
          if (i + 1 == tops.size())
          {
              cout << tops[i] << endl;
          }
          else
          {
              cout << tops[i] << ", ";
          }
       }
   }
   cout << "Степени вершин графа:" << endl;
   vector<int> degrees = graph.GetGraphDegrees();
   for (int i = 0; i < degrees.size(); i++)
   {
       cout << "Степень вершины " << i + 1 << ": " << degrees[i] << endl;
   }
   cout << "Последовательность рёбер графа: ";
   graph.GetSequenceOfGraphEdges();
   return 0;
}