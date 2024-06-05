using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModelingSimulator.Function
{
    public class Dijkstra_sAlgorithm
    {
        int cPoint; //Количество точек
        int[,] aMatrix; //Матрица со значениями весов ребер
        int startPMain; //Начало маршрута
        List<(int, int)> aRoutes = new List<(int, int)>(); //(путь, числовая характеристика)

        public string? Start(int cPoint, int startPMain, int[,] aMatrix, List<(int, int)> aRoutes)
        {
            this.cPoint = cPoint;
            this.startPMain = startPMain;
            this.aMatrix = aMatrix;
            this.aRoutes = aRoutes;
            return SearchRoutes();
        }

        string? SearchRoutes()
        {
            //Перебираем все точки, которые нужно найти из aRoutes
            for (int i = 0; i < aRoutes.Count; i++)
            {
                List<int> passedP = new List<int>();
                aRoutes[i] = SearchMin(startPMain, aRoutes[i], 0, passedP);
            }
            string answer = aRoutes.FirstOrDefault(it => it.Item2 == aRoutes.Select(it => it.Item2).Max()).Item2.ToString();
            return answer;
        }

        (int, int) SearchMin(int startP, (int, int) searchP, int sum, List<int> passedP)
        {
            //Когда из первой строки сразу находится искомое значение
            if (aMatrix[startP, searchP.Item1] != -1)
            {
                int startEqual = sum + aMatrix[startP, searchP.Item1];
                if (searchP.Item2 > startEqual) searchP.Item2 = startEqual;
            }
            for (int j = 0; j < cPoint; j++)
            {
                //Проверка, что данная точка ещё не была посещена, не равна искомой (т.к. это проверяется сверху) и не пустая
                if (passedP.IndexOf(j) == -1 && j != searchP.Item1 && aMatrix[startP, j] != -1)
                { 
                    if (startP == startPMain) sum = 0;
                    sum += aMatrix[startP, j];
                    passedP.Add(startP);
                    List<int> buffer = new List<int>(passedP);
                    searchP = SearchMin(j, searchP, sum, buffer);
                }
            }
            return searchP;
        }

        void InputData()
        {
            //Ввод матрицы маршрутов
            aMatrix = new int[cPoint, cPoint];
            int count;
            Console.WriteLine("Заполните матрицу со значениями весов ребер: ");
            for (int i = 0; i < cPoint; i++)
            {
                string[] buffer = new string[cPoint - 1];
                //Вводится строка таблицы с числами через пробел в буферную строку
                buffer = Console.ReadLine().Split(" ").ToArray();
                count = 0;
                //Буферная строка подставляется в массив таблицы метода
                foreach (string k in buffer)
                {
                    if (k != "-") aMatrix[i, count] = Convert.ToInt32(k);
                    else aMatrix[i, count] = -1;
                    count++;
                }
                if (i != startPMain) aRoutes.Add((i, int.MaxValue));
            }
        }
    }
}
