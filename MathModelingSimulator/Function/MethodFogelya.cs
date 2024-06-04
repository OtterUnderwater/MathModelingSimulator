using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModelingSimulator.Function
{
    public class BasicMethods
    {
        int[] ai; //массив поставщиков и сколько каждый может продукции поставить
        int[] bj; //массив требуемых поставок потребителю
        int[] aiBuf; //массив поставщиков и сколько каждый может продукции поставить
        int[] bjBuf; //массив требуемых поставок потребителю
        int[,] tarif; //массив исходного тарифного плана
        int[,] tarifResult; //итоговый тарифный план
        int[,] bufferTarif; //массив тарифного плана, для изменения и нахождения следующего элемента (путем зануления прошлых)
        int[,] arrayPriority;
        int iCurrent = 0, jCurrent = 0; //индексы текущей ячейки, с которой будет проводиться подсчёт

        public double? MethodFogelya()
        {
            bufferTarif = new int[tarif.GetLength(0), tarif.GetLength(1)]; //массив тарифов, который будет меняться
            tarifResult = new int[tarif.GetLength(0), tarif.GetLength(1)]; //массив результатов, т.е. куда мы поставляем
            Array.Copy(tarif, bufferTarif, tarif.Length); //скопировали массив тарифов
            Array.Copy(aiBuf, ai, aiBuf.Length);
            Array.Copy(bjBuf, bj, bjBuf.Length);
            arrayPriority = GetMassPriorityForDuoblePredp(tarif);
            Array.Clear(tarifResult);
            if (ZadIsClose(ai, bj))
            {
                while (ai.Sum() > 0 && bj.Sum() > 0)
                {
                    PoiskCurrentIndexFogel(ref iCurrent, ref jCurrent, bufferTarif, ai, bj);
                    if (ai[iCurrent] == bj[jCurrent])
                    {
                        tarifResult[iCurrent, jCurrent] = ai[iCurrent]; //присваиваем массиву распределения ресурсов значение поставщика с текущим индексом по строке
                        bufferTarif[iCurrent, jCurrent] = 0; //зануляем текущий элемент в буферном массиве тарифов,
                                                             //чтобы следующий индекс текущего элемента был другой
                        ai[iCurrent] = 0; //зануляем поставщика, потому что у него закончились ресурсы для поставки
                                          //tarifResult = ZeroingRowMassiv(tarifResult, bn, iMin);
                        bj[jCurrent] = 0; //зануляем потребителя, потому что ему уже не требуется
                                          //что-то поставлять (уже распределили кто поставлять будет)
                        for (int i = 0; i < arrayPriority.GetLength(0); i++) arrayPriority[i, jCurrent] = 0;
                        for (int i = 0; i < arrayPriority.GetLength(1); i++) arrayPriority[iCurrent, i] = 0;
                        continue;
                    }
                    if (ai[iCurrent] > bj[jCurrent])
                    {
                        tarifResult[iCurrent, jCurrent] = bj[jCurrent];
                        bufferTarif = ZeroingColumnMassiv(bufferTarif, jCurrent);
                        for (int i = 0; i < arrayPriority.GetLength(0); i++) arrayPriority[i, jCurrent] = 0;
                        ai[iCurrent] -= bj[jCurrent];
                        bj[jCurrent] = 0;
                        continue;
                    }
                    if (ai[iCurrent] < bj[jCurrent])
                    {
                        tarifResult[iCurrent, jCurrent] = ai[iCurrent];
                        bufferTarif = ZeroingRowMassiv(bufferTarif, iCurrent);
                        for (int i = 0; i < arrayPriority.GetLength(1); i++) arrayPriority[iCurrent, i] = 0;
                        bj[jCurrent] -= ai[iCurrent];
                        ai[iCurrent] = 0;
                        continue;
                    }
                }
                return ShowObjectiveFunctionValue(tarif, tarifResult);
            }
            else
            {
                return null;
            }
        }

        static void GetMinRow(int[,] mass, out int min1, out int min2, int row)
        {
            int[] arrayRow = new int[mass.GetLength(1)];
            for (int i = 0; i < arrayRow.Length; i++)
            {
                if (mass[row, i] == 0)
                {
                    arrayRow[i] = int.MaxValue;
                    continue;
                }
                arrayRow[i] = mass[row, i];
            }
            min1 = arrayRow.Min();
            min2 = int.MaxValue;
            for (int i = 0; i < mass.GetLength(1); i++)
            {
                if (min1 != mass[row, i] && min2 > mass[row, i] && mass[row, i] != 0) min2 = mass[row, i];
            }
            if (min2 == int.MaxValue) min2 = 0;
        }

        static void GetMinCol(int[,] mass, out int min1, out int min2, int col)
        {
            int count;
            int[] arrayCol = new int[mass.GetLength(0)];
            for (int i = 0; i < arrayCol.Length; i++)
            {
                if (mass[i, col] == 0)
                {
                    arrayCol[i] = int.MaxValue;
                    continue;
                }
                arrayCol[i] = mass[i, col];
            }
            min1 = arrayCol.Min();
            min2 = int.MaxValue;
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                if (min1 != mass[i, col] && min2 > mass[i, col] && mass[i, col] != 0) min2 = mass[i, col];
            }
            if (min2 == int.MaxValue) min2 = 0;
        }

        static void PoiskCurrentIndexFogel(ref int iCurrent, ref int jCurrent, int[,] mass, int[] ai, int[] bj)
        {
            int[] rowOst = new int[ai.Length]; //массив остатков по строкам
            int[] colOst = new int[bj.Length]; //массив остатков по столбцам
            for (int i = 0; i < ai.Length; i++) //проходит по каждой строке, находит два минимума и подсчитывает остатки 
            {
                int min1, min2;
                GetMinRow(mass, out min1, out min2, i);
                if (min1 == int.MaxValue || min2 == int.MaxValue) rowOst[i] = 0;
                else rowOst[i] = Math.Abs(min1 - min2);
            }
            for (int i = 0; i < bj.Length; i++) //проходит по каждому столбцу, находит два минимума и подсчитывает остатки 
            {
                int min1, min2;
                GetMinCol(mass, out min1, out min2, i);
                if (min1 == int.MaxValue || min2 == int.MaxValue) colOst[i] = 0;
                else colOst[i] = Math.Abs(min1 - min2);
            }
            if (colOst.Max() > rowOst.Max())
            {
                jCurrent = Array.IndexOf(colOst, colOst.Max());
                int[] curRow = new int[rowOst.Length];
                for (int i = 0; i < rowOst.Length; i++)
                {
                    if (mass[i, jCurrent] != 0) curRow[i] = mass[i, jCurrent];
                    else curRow[i] = int.MaxValue;
                }
                iCurrent = Array.IndexOf(curRow, curRow.Min());
            }
            else
            {
                iCurrent = Array.IndexOf(rowOst, rowOst.Max());
                int[] curCol = new int[colOst.Length];
                for (int i = 0; i < colOst.Length; i++)
                {
                    if (mass[iCurrent, i] != 0) curCol[i] = mass[iCurrent, i];
                    else curCol[i] = int.MaxValue;
                }
                jCurrent = Array.IndexOf(curCol, curCol.Min());
            }
        }

        static int[] RowToArray(int[,] mass, int[] resMass, int numRow)
        {
            for (int i = 0; i < mass.GetLength(1); i++)
            {
                resMass[i] = mass[numRow, i];
            }
            return resMass;
        }

        static int[] ColToArray(int[,] mass, int[] resMass, int numCol)
        {
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                resMass[i] = mass[i, numCol];
            }
            return resMass;
        }

        static int[,] GetMassPriorityForDuoblePredp(int[,] mass)
        {
            int[,] arrayPriority = new int[mass.GetLength(0), mass.GetLength(1)];
            int[] currentRow = new int[mass.GetLength(1)];
            int[] currentCol = new int[mass.GetLength(0)];
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                currentRow = RowToArray(mass, currentRow, i);
                for (int j = 0; j < mass.GetLength(1); j++)
                {
                    if (mass[i, j] == currentRow.Min()) arrayPriority[i, j] = 2; //2 - приоритет минимум x1 (либо в строке, либо в столбце)
                    else arrayPriority[i, j] = 1; //1 - приоритет обычный (не минимум и в строке, и в столбце)
                }
            }
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                currentCol = ColToArray(mass, currentCol, j); //текущий столбец
                for (int i = 0; i < mass.GetLength(0); i++)
                {
                    if (mass[i, j] == currentCol.Min()) //если минимум текущего столбца равен минимуму проверяемой строки
                    {
                        if (currentCol.Min() == RowToArray(mass, currentRow, i).Min()) arrayPriority[i, j] = 3; //3 - приоритет минимум x2 (минимум и в строке, и в столбце)
                        else arrayPriority[i, j] = 2;
                    }
                }
            }
            return arrayPriority;
        }

        static double ShowObjectiveFunctionValue(int[,] massTarif, int[,] massResource)
        {
            double Lx = 0;
            for (int i = 0; i < massResource.GetLength(0); i++)
            {
                for (int j = 0; j < massResource.GetLength(1); j++)
                {
                    if (massResource[i, j] != 0) Lx += (massResource[i, j] * massTarif[i, j]);
                }
            }
            return Lx;
        }

        //row - зануляемая строка
        static int[,] ZeroingRowMassiv(int[,] mass, int row)
        {
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                if (mass[row, j] != 0) mass[row, j] = 0;
            }
            return mass;
        }

        //column - зануляемый столбец
        static int[,] ZeroingColumnMassiv(int[,] mass, int column)
        {
            for (int i = 0; i < mass.GetLength(0); i++)
            {
                if (mass[i, column] != 0) mass[i, column] = 0;

            }
            return mass;
        }

        static bool ZadIsClose(int[] ai, int[] bj)
        {
            if (ai.Sum() == bj.Sum()) return true; else return false;
        }

        public double? Start(int[] ai, int[] bj, int[,] tarif)
        {
            this.ai = ai;
            aiBuf = ai;
            this.bj = bj;
            bjBuf = bj;
            this.tarif = tarif;
            return MethodFogelya();
        }

    }


}
