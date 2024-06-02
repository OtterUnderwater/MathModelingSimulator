using Avalonia.Controls;
using Avalonia.Media;
using DynamicData;
using MathModelingSimulator.Models;
using MathModelingSimulator.Views;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace MathModelingSimulator.ViewModels
{
	public class TaskSimulatorsViewModel : MainWindowViewModel
	{
		#region PropertyObjects
		string theory = "";
		public string Theory { get => theory; set => SetProperty(ref theory, value); }

		string simulatorName = "";
		public string SimulatorName { get => simulatorName; set => SetProperty(ref simulatorName, value); }

		StackPanel matrix = new StackPanel();
		public StackPanel Matrix { get => matrix; set => SetProperty(ref matrix, value); }
		
		int answer = 0;
		public int Answer { get => answer; set => answer = value; }

		SimulatorTask task = new SimulatorTask();

		string message = "";
		public string Message { get => message; set => SetProperty(ref message, value); }
		#endregion

		public void GetTask(int numberTask)
		{
			string path = "files/" + ContextDb.Simulators.First(it => it.Id == numberTask).Theory + ".txt";
			SimulatorName = $"Задание по теме {ContextDb.Simulators.First(it => it.Id == numberTask).Name}";
			StreamReader reader = new StreamReader(path);
			Theory = reader.ReadToEnd();
			List <SimulatorTask> listTask = ContextDb.SimulatorTasks.Where(it => it.IdSimulator == numberTask).ToList();
			Random random = new Random();
			int randomIndex = random.Next(listTask.Count);
			task = listTask[randomIndex];
			Matrix = new StackPanel();
			Matrix.Margin = new Avalonia.Thickness(0, 10, 0, 0);
			for (int i = 0; i < task.ZadanieMatrix.GetLength(0); i++)
			{
				StackPanel lbHorizontal = new StackPanel();
				lbHorizontal.Margin = new Avalonia.Thickness(0, 0, 0, 10);
				List<TextBlock> listTextBlock = new List<TextBlock>();
				for (int j = 0; j < task.ZadanieMatrix.GetLength(1); j++)
				{
					TextBlock textBlock = new TextBlock();
					textBlock.Text = task.ZadanieMatrix[i, j].ToString();
					textBlock.Padding = new Avalonia.Thickness(20);
					textBlock.Margin = new Avalonia.Thickness(10, 0);
					listTextBlock.Add(textBlock);
				}
				lbHorizontal.Children.Add(listTextBlock);
				lbHorizontal.Orientation = Avalonia.Layout.Orientation.Horizontal;
				Matrix.Children.Add(lbHorizontal);
			}
			PageSwitch.View = new TaskSimulatorsView();
		}

		public void ClickAnswer()
		{
			if (Answer == task.Answer)
			{
				Message = "Задача решена правильно";
			}
			else
			{
				Message = "Задача решена неправильно";
			}		
		}

	}
}