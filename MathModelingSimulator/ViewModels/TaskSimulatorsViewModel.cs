using Avalonia.Controls;
using DynamicData;
using MathModelingSimulator.Models;
using MathModelingSimulator.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


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

		int numberTask = 0;
        #endregion

        public void GetTask(int numberNeedTask)
		{
			numberTask = numberNeedTask;
			string directory = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory))));
			string path = $"{directory}\\Assets\\{ContextDb.Simulators.First(it => it.Id == numberTask).Theory}.txt";
			SimulatorName = $"Задание по теме {ContextDb.Simulators.First(it => it.Id == numberTask).Name}";
			StreamReader reader = new StreamReader(path);
			Theory = reader.ReadToEnd();
			List <SimulatorTask> listTask = ContextDb.SimulatorTasks.Where(it => it.IdSimulator == numberTask).ToList();
			Random random = new Random();
			int randomIndex = random.Next(listTask.Count);
			task = listTask[randomIndex];
			Matrix = new StackPanel();
			for (int i = 0; i < task.ZadanieMatrix.GetLength(0); i++)
			{
				StackPanel lbHorizontal = new StackPanel();
				lbHorizontal.Margin = new Avalonia.Thickness(0, 0, 0, 0);
				List<TextBlock> listTextBlock = new List<TextBlock>();
				for (int j = 0; j < task.ZadanieMatrix.GetLength(1); j++)
				{
					TextBlock textBlock = new TextBlock();
					textBlock.Text = task.ZadanieMatrix[i, j].ToString();
					textBlock.Padding = new Avalonia.Thickness(10);
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
			History history = new History();
			history.IdUser = CurrentUser.Id;
			history.IdSimulator = numberTask;
			history.PassageDateTime = DateTime.Now;
			if (Answer == task.Answer)
			{
				Message = "Задача решена правильно";
				history.Result = true;
			}
			else
			{
				Message = "Задача решена неправильно";
				history.Result = false;
			}
			ContextDb.Histories.Add(history);
			ContextDb.SaveChanges();
		}

	}
}