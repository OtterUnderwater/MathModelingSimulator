using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModelingSimulator.Function
{
    internal struct BranchMatrix
    {
        public int id; //уникальный код, по которому будем находить обратный путь
        public int objFunction; //целевая функция
        public (int start, int end, bool isNotBranch) path; // картедж, с значениями начала и конца ветки и то, включена ли эта ветка в решение
        public int[,] matrix; //матрица, которая соответствует решению
        public int previosId; //целевая функция предыдущего элемента
    }
    public class TravelingSalesmanProblem
    {
        static int id = 0;
        static int[,] Mass;
        static int M = int.MaxValue;


        static List<BranchMatrix> Matrix = new List<BranchMatrix>(); //общий лист со всеми матрицами
        static List<BranchMatrix> minBranch = new List<BranchMatrix>(); //лист с матрицами, которые не ветвились

        static int[,] cloneArr(int[,] mas)
        {
            int[,] newMas = new int[mas.GetLength(0), mas.GetLength(1)];
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    newMas[i, j] = mas[i, j];
                }
            }
            return newMas;
        }
        public int MainTravelingSalesmanProblem(int[,] mas)
        {
            InputMas(mas); 


            BranchMatrix previous; //структура , которая хранит в себе значение предыдущего элемента
            previous.id = id;
            previous.previosId = id - 1;
            previous.matrix = Mass;
            previous.objFunction = Reduction(ref Mass);
            previous.path = (0, 0, false);
            Matrix.Add(previous); //добавляем 1 элемент (h0)
            id++;

            while (!isEndIteration(previous.matrix)) //пока не останется матрица 2х2
            {
                BranchMatrix notParth; //с путем
                BranchMatrix withParth; //буз путя
                int MaxZeroCell = 0;

                //оцениваем свободные ячейки, выбираем из этой матрицы макимальное значение (пути: "с = true" или "без = false")
                notParth.path = MaxMas(ZeroCellEvaluation(cloneArr(previous.matrix)), ref MaxZeroCell, false);
                withParth.path = MaxMas(ZeroCellEvaluation(cloneArr(previous.matrix)), ref MaxZeroCell, true);


                notParth.matrix = WithoutAPath(cloneArr(previous.matrix), withParth.path.start, withParth.path.end);

                //подсчитываем значение целефой функции
                notParth.objFunction = previous.objFunction + MaxZeroCell;
                //если у нас остается матрица 2х2:
                if (WithAPath(cloneArr(previous.matrix), withParth.path.start, withParth.path.end, out withParth.matrix) == -1)
                {
                    //высчитываем значение для предпоследнего значения
                    withParth.objFunction = previous.objFunction + WithAPath(cloneArr(previous.matrix), withParth.path.start, withParth.path.end, out withParth.matrix);
                    withParth.previosId = previous.id;
                    withParth.id = id + 1;
                    id++;

                    //высчитываем значение для последнего значения
                    BranchMatrix endParth;
                    endParth.path = EndPath(cloneArr(withParth.matrix), true);
                    endParth.matrix = cloneArr(withParth.matrix);
                    endParth.objFunction = previous.objFunction + WithAPath(cloneArr(previous.matrix), withParth.path.start, withParth.path.end, out withParth.matrix) + 1;
                    endParth.id = id + 1;
                    id++;
                    endParth.previosId = withParth.id;



                    //добавляем последнее и предпоследнее значение
                    Matrix.Add(withParth);
                    Matrix.Add(endParth);

                    break;
                }
                else
                {
                    withParth.objFunction = previous.objFunction + WithAPath(cloneArr(previous.matrix), withParth.path.start, withParth.path.end, out withParth.matrix);
                }

                //присвоение айди для веток с путем или без
                notParth.previosId = previous.id;
                notParth.id = id + 1;
                id++;
                withParth.previosId = previous.id;
                withParth.id = id + 1;
                id++;



                //удляем тут ветку, от которой ветвились
                minBranch.Remove(previous);
                //добавляем ветки, которые получились после ветвления
                minBranch.Add(withParth);
                minBranch.Add(notParth);
                Matrix.Add(withParth);
                Matrix.Add(notParth);
                //сортируем список матриц, от которых еще не ветвились по значению целевой функции
                minBranch = minBranch.OrderBy(x => x.objFunction).ToList();

                //присваиваем переменной предыдущего элемента - первому элементу отсортированного листа (с минимальной целевой функцией). 
                previous.matrix = minBranch[0].matrix;
                previous.objFunction = minBranch[0].objFunction;
                previous.id = minBranch[0].id;
                previous.previosId = minBranch[0].id - 1;
                minBranch.Remove(minBranch[0]);

            }

            //составление листа ответов
            int OptimZnach = Matrix.Last().objFunction;
            int previousId = Matrix.Last().id;
            List<(int s, int e)> otvet = new List<(int s, int e)>(); //лист кортежей (s - начало,e - конец)
            int j = 0;
            while (previousId > 0) //покеа предыдущий id не станет меньше 0
            {
                for (int i = 0; i < Matrix.Count(); i++) //перебираем значения из общей матрицы
                {
                    if (Matrix[i].id == previousId) //если значение id совпало с предыдущим - значит ветка найдена
                    {
                        if (Matrix[i].path.isNotBranch == true) //если включаем путь
                        {
                            previousId = Matrix[i].previosId;
                            (int, int) temp = (Matrix[i].path.start, Matrix[i].path.end);
                            otvet.Add(temp);
                            break;
                        }
                        else
                        {//если не включаем путь
                            previousId = Matrix[i].previosId;
                            break;
                        }
                    }
                }
            }
            return OptimZnach;
        }
        /// <summary>
        /// Это конец итерации?
        /// </summary>
        /// <param name="mas"></param>
        /// <returns></returns>
        static bool isEndIteration(int[,] mas)
        {
            if (mas.GetLength(0) == 2 || mas.GetLength(1) == 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static int Reduction(ref int[,] mas)
        {
            int min = M;
            int sumMin = 0;
            //редукция строк:
            for (int i = 1; i < mas.GetLength(0); i++)
            {
                min = M;
                for (int j = 1; j < mas.GetLength(1); j++)
                {
                    if (min > mas[i, j])
                    {
                        min = mas[i, j];
                    }
                }
                for (int j = 1; j < mas.GetLength(1); j++)
                {
                    mas[i, j] -= min;
                }
                sumMin += min;
            }

            //редукция столбцов:
            for (int j = 1; j < mas.GetLength(1); j++)
            {
                min = M;
                for (int i = 1; i < mas.GetLength(0); i++)
                {
                    if (min > mas[i, j])
                    {
                        min = mas[i, j];
                    }
                }
                for (int i = 1; i < mas.GetLength(0); i++)
                {
                    mas[i, j] -= min;
                }
                sumMin += min;
            }

            return sumMin;

        }
        /// <summary>
        /// Высчитывает максимум из передаваемого массива
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="Max"></param>
        /// <param name="isNotBranch"></param>
        /// <returns></returns>
        static  (int Start, int End, bool isNotBranch) MaxMas(int[,] mas, ref int Max, bool isNotBranch)
        {
            int s = 0, e = 0;
            Max = int.MinValue;
            for (int i = 1; i < mas.GetLength(0); i++)
            {
                for (int j = 1; j < mas.GetLength(1); j++)
                {
                    if (Max < mas[i, j])
                    {
                        Max = mas[i, j];
                        s = mas[i, 0];
                        e = mas[0, j];
                    }
                }
            }

            return (s, e, isNotBranch);
        }
        static  (int Start, int End, bool isNotBranch) EndPath(int[,] mas, bool isNotBranch)
        {
            return (mas[1, 0], mas[0, 1], isNotBranch);
        }
        /// <summary>
        /// оценка нулевых ячеек
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="Start"></param>
        /// <param name="End"></param>
        /// <returns></returns>
        static int[,] ZeroCellEvaluation(int[,] mas)
        {
            int red = Reduction(ref mas);
            int[,] ZeroCell = new int[mas.GetLength(0), mas.GetLength(1)];

            for (int i = 0; i < mas.GetLength(0); i++)
            {
                ZeroCell[i, 0] = mas[i, 0];
            }
            for (int j = 0; j < mas.GetLength(1); j++)
            {
                ZeroCell[0, j] = mas[0, j];
            }

            for (int i = 1; i < mas.GetLength(0); i++)
            {
                for (int j = 1; j < mas.GetLength(1); j++)
                {
                    if (mas[i, j] == 0)
                    {

                        int minRow = M;
                        for (int a = 1; a < mas.GetLength(0); a++)
                        { //i - минимум по строкам
                            if ((minRow > mas[a, j]) && (a != i))
                            {
                                minRow = mas[a, j];
                            }
                        }
                        int minCol = M;
                        for (int b = 1; b < mas.GetLength(1); b++)
                        {
                            if (minCol > mas[i, b] && (b != j))
                            {
                                minCol = mas[i, b];
                            }
                        }
                        ZeroCell[i, j] = minRow + minCol;
                    }
                }
            }

            return ZeroCell;
        }
        /// <summary>
        /// При учитывании пути - вычеркиваем его и возвращаем редукцию от нового массива
        /// </summary>
        /// <param name="mas">массив</param>
        /// <param name="pathStart">индекс начала функции</param>
        /// <param name="pathEnd">индекс конца функции</param>
        /// <param name="PreviousEl">Целевая функция предыдущего элемента</param>
        /// <returns>значение целефой функции при условии включения пути в итог</returns>
        static int WithAPath(int[,] mas, int pathStart, int pathEnd, out int[,] newMas)
        {
            newMas = new int[mas.GetLength(0) - 1, mas.GetLength(1) - 1];

            //находим индексы начала и конца массива по элементу
            int indS = 0, indE = 0;
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                if (mas[i, 0] == pathStart) { indS = i; }
                if (mas[0, i] == pathEnd) { indE = i; }
            }

            //ставим М на обратном пути 
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if ((mas[i, 0] == pathEnd) && (mas[0, j] == pathStart))
                    {
                        mas[i, j] = M;
                    }
                }
            }

            //удаляем строку и столбец, на пересечении которых находится элемент выбранного маршрута
            int newRow = 0, newCol = 0;
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                if (i == indS) { continue; }
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if (j == indE) { continue; }

                    if (i > indS)
                    {
                        newRow = i - 1;
                    }
                    else
                    {
                        newRow = i;
                    }
                    if (j > indE)
                    {
                        newCol = j - 1;
                    }
                    else
                    {
                        newCol = j;
                    }

                    newMas[newRow, newCol] = mas[i, j];
                }
            }
            //если получившаяся таблица 2х2 (учет того, что получившаяся матрица - последняя)
            if (newMas.GetLength(0) == 2 && newMas.GetLength(1) == 2)
            {
                return -1;
            }

            return Reduction(ref newMas);
        }
        /// <summary>
        /// Возвращает массив с М на месте найденного пути
        /// </summary>
        static int[,] WithoutAPath(int[,] mas, int pathStart, int pathEnd)
        {
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    if ((mas[i, 0] == pathStart) && (mas[0, j] == pathEnd))
                    {
                        mas[i, j] = M;
                    }
                }
            }

            return mas;
        }
        /// <summary>
        /// Красивый вывод массива
        /// </summary>
        /// <param name="arr"></param>
        static void InputMas(int[,] mas)
        {
            Mass = new int[mas.GetLength(0)+1, mas.GetLength(0)+1];
            for (int i = 0; i < Mass.GetLength(0); i++)
            {
                Mass[i, 0] = i;
            }
            for (int j = 0; j < Mass.GetLength(1); j++)
            {
                Mass[0, j] = j;
            }
            for (int i = 0, k=1; i < mas.GetLength(0); i++, k++) {
                for (int j = 0, n=1; j < mas.GetLength(1); j++,n++) {
                    if (i == j)
                    {
                        Mass[k, n] = M;
                    }
                    else {
                        Mass[k, n] = mas[i,j];
                    }
                }
            }
        }

    }
}
