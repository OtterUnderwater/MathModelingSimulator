using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModelingSimulator.Function
{
    internal class SimplexMetod
    {
        public int MainSimplexMetod(int[,] mas, bool isMax)
        {

            while (check(mas, isMax)) //пока в строке функционала есть отрицательные/положительные элементы в последней строке
            {
                //находим минимум в последней строке, это и будет ведущим столбцом
                int leadingColumn = LeadingColumn(mas, isMax); //индекс ведущего столбца
                int leadingRow = LeadingRow(mas, leadingColumn);//находим индекс ведущей строки
                mas[leadingRow, 0] = mas[0, leadingColumn]; //меняем базисы

                int ledingElement = mas[leadingRow, leadingColumn]; //ведущий элемент
                for (int j = 1; j < mas.GetLength(1); j++) //делаем 1 ведущую строку с ведущим элементом
                {
                    mas[leadingRow, j] = mas[leadingRow, j] / ledingElement;
                }


                int otrZnach = 0;
                for (int i = 1; i < mas.GetLength(0); i++) //ведущий столбюец, кроме ведущего элемента - равен 0
                {
                    for (int j = 1; j < mas.GetLength(1); j++)
                    {
                        if (j == 1 && i != leadingRow)
                        {
                            otrZnach = mas[i, leadingColumn] * (-1);
                        }
                        if (i != leadingRow)
                        {
                            mas[i, j] = mas[leadingRow, j] * otrZnach + mas[i, j];
                        }

                    }
                }
            }
            return mas[mas.GetLength(0) - 1, mas.GetLength(1) - 1];
        }
        int LeadingColumn(int[,] mas, bool isMax)
        {
            if (isMax)
            {
                int min = int.MaxValue;
                int IndCol = 0;
                for (int i = mas.GetLength(0) - 1; i < mas.GetLength(0); i++)
                {
                    for (int j = 1; j < mas.GetLength(1); j++)
                    {
                        if (mas[i, j] < min)
                        {
                            min = mas[i, j];
                            IndCol = j;
                        }
                    }
                }
                return IndCol;
            }
            else
            {
                int max = 0;
                int IndCol = 0;
                for (int i = mas.GetLength(0) - 1; i < mas.GetLength(0); i++)
                {
                    for (int j = 1; j < mas.GetLength(1); j++)
                    {
                        if (mas[i, j] > max)
                        {
                            max = mas[i, j];
                            IndCol = j;
                        }
                    }
                }
                return IndCol;
            }
        }
        int LeadingRow(int[,] mas, int indLeadingCol)
        {
            int result;
            int indLeadingRow = 1;
            int indLastColumn = mas.GetLength(1) - 1;
            int min = int.MaxValue;
            for (int i = 1; i < mas.GetLength(0) - 1; i++)
            {
                if (mas[i, indLeadingCol] > 0)
                {
                    result = mas[i, indLastColumn] / mas[i, indLeadingCol];
                    if (result < min)
                    {
                        min = result;
                        indLeadingRow = i;
                    }
                }

            }
            return indLeadingRow;
        }
        bool check(int[,] mas, bool isMax)
        {
            if (isMax)
            {
                int indRow = mas.GetLength(0) - 1;
                for (int j = 1; j < mas.GetLength(1); ++j)
                {
                    if (mas[indRow, j] < 0)
                    {
                        return true; // продолжать проверять
                    }
                }
                return false; //остановиться
            }
            else
            {
                int indRow = mas.GetLength(0) - 1;
                for (int j = 1; j < mas.GetLength(1); ++j)
                {
                    if (mas[indRow, j] > 0)
                    {
                        return true; // продолжать проверять
                    }
                }
                return false; //остановиться
            }
        }
    }
}
