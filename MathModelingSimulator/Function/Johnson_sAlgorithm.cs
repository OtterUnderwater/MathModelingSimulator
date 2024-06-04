using DynamicData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathModelingSimulator.Function
{
    public class Johnson_sAlgorithm
    {
        List<(double, double)> listValues = new List<(double, double)>(); //Лист изначальных значений
        List<(double, double)> listValuesResult = new List<(double, double)>(); //Лист итоговых значений
        List<(double, double)> ignore = new List<(double, double)>(); //Лист игнорируемых значений (тех, которые уже были добавлены в listValuesResult)
        List<(double, double)> startValues = new List<(double, double)>(); //Значения 1 столбца
        List<(double, double)> endValues = new List<(double, double)>(); //Значения 2 столбца
        List<double> timeP = new List<double>(); //Время простоя 2 станка по изначальному плану
        List<double> timePRezult = new List<double>(); //Время простоя 2 станка по итоговому плану

        /// <summary>
        /// Стартовый метод программы
        /// </summary>
        public string Start(int[,] arValues)
        {
            if(arValues.GetLength(1) == 2)
            {
                for(int i = 0; i < arValues.GetLength(0); i++)
                {
                    listValues.Add((arValues[i, 0], arValues[i, 1]));
                }
                MethodJohnson();
                return timePRezult.Max().ToString();
            } 
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Метод Джонсона
        /// </summary>
        void MethodJohnson()
        {
            for (int i = 0; i < listValues.Count; i++)
            {
                double min1 = listValues.Where(it => !ignore.Contains(it)).Select(it => it.Item1).ToList().Min(); //Минимальное время обработки на 1 станке
                double min2 = listValues.Where(it => !ignore.Contains(it)).Select(it => it.Item2).ToList().Min(); //Минимальное время обработки на 2 станке
                if (min1 <= min2)
                {
                    var listMinStart = listValues.Where(it => it.Item1 == min1).ToList();
                    double maxEl1St = listMinStart.Select(it => it.Item2).Max();
                    int indexMax = listMinStart.Select(it => it.Item2).ToList().IndexOf(maxEl1St);
                    startValues.Add(listMinStart[indexMax]);
                    ignore.Add(listMinStart[indexMax]);
                }
                else
                {
                    var listMinEnd = listValues.Where(it => it.Item2 == min2).ToList();
                    double maxEl1St = listMinEnd.Select(it => it.Item1).Max();
                    int indexMax = listMinEnd.Select(it => it.Item1).ToList().IndexOf(maxEl1St);
                    endValues.Insert(0, listMinEnd[indexMax]);
                    ignore.Add(listMinEnd[indexMax]);
                }
            }
            listValuesResult = startValues.Concat(endValues).ToList();
            timeP.Add(listValues[0].Item1);
            timePRezult.Add(listValuesResult[0].Item1);
            for (int i = 1; i < listValuesResult.Count; i++)
            {
                double sumP = 0;
                double sumPRezult = 0;
                double sumP2 = 0;
                double sumPRezult2 = 0;
                for (int j = 0; j <= i; j++)
                {
                    sumP += listValues[j].Item1;
                    sumPRezult += listValuesResult[j].Item1;
                    if (j != i)
                    {
                        sumP2 += listValues[j].Item2;
                        sumPRezult2 += listValuesResult[j].Item2;
                    }
                }
                timeP.Add(sumP - sumP2);
                timePRezult.Add(sumPRezult - sumPRezult2);
            }
        }

    }

}
