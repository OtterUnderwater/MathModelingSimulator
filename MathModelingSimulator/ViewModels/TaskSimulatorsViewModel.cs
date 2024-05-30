using Avalonia.Controls;
using Avalonia.Markup.Xaml.Templates;
using DynamicData;
using MathModelingSimulator.Models;
using System.Collections.Generic;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace MathModelingSimulator.ViewModels
{
    public class TaskSimulatorsViewModel : MainWindowViewModel
    {
        StackPanel matrix = new StackPanel();
        public StackPanel Matrix { get => matrix; set => SetProperty(ref matrix, value); }
        public TaskSimulatorsViewModel()
        {
            List<SimulatorTask> simulatorTasks = ContextDb.SimulatorTasks.ToList();
            int[,] taskMatrix = simulatorTasks[0].ZadanieMatrix;
            for (int i = 0; i < taskMatrix.GetLength(0); i++)
            {
                StackPanel lbHorizontal = new StackPanel();
                List<TextBox> listTextBox = new List<TextBox>();
                for (int j = 0; j < taskMatrix.GetLength(1); j++)
                {
                    TextBox textBox = new TextBox();
                    textBox.Text = taskMatrix[i, j].ToString();
                    listTextBox.Add(textBox);
                }
                lbHorizontal.Children.Add(listTextBox);
                lbHorizontal.Orientation = Avalonia.Layout.Orientation.Horizontal;
                Matrix.Children.Add(lbHorizontal);
            }
        }
    }
}