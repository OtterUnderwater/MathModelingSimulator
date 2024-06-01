using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using MathModelingSimulator.Models;
using System.IO;
using System.Linq;
using ReactiveUI;
using DynamicData;
using SkiaSharp;
using System.Diagnostics;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace MathModelingSimulator.ViewModels
{
	public class CreateSimulatorViewModel : MainWindowViewModel
	{
		#region PropertyObjects
		int countRows = 0;
		public int CountRows
		{
			get => countRows;
			set
			{
				countRows = value;
				GenerateMatrix();
			}
		}

		int countColumns = 0;
		public int CountColumns
		{
			get => countColumns;
			set
			{
				countColumns = value;
				GenerateMatrix();
			}
		}

		StackPanel? matrix = new StackPanel();
		public StackPanel? Matrix { get => matrix; set => SetProperty(ref matrix, value); }

		bool isVisibleEnterMatrix = false;
		public bool IsVisibleEnterMatrix { get => isVisibleEnterMatrix; set => SetProperty(ref isVisibleEnterMatrix, value); }

		#endregion

		/// <summary>
		/// Выбор 2. Ввод матрицы
		/// </summary>
		public void EnterMatrix()
		{
			Matrix = new StackPanel();
			IsVisibleEnterMatrix = true; //видимость ввода строк и столбцов
		}

		/// <summary>
		/// При изменении строк и столбцов - генерация полей ввода для матрицы
		/// </summary>
		public void GenerateMatrix()
		{
			Matrix = new StackPanel();
			if (countRows > 0 && countColumns > 0)
			{
				int[,] taskMatrix = new int[countRows, countColumns];
				ShowMatrix(taskMatrix);
			}
		}

		/// <summary>
		/// Выбор 1. Чтение из файла
		/// </summary>
		public async void AttachFileClick()
		{
			int[,] newMatrix = null;
			IsVisibleEnterMatrix = false;
			var topLevel = TopLevel.GetTopLevel(PageSwitch.View);
			var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
			{
				Title = "Выберите текстовый файл",
				AllowMultiple = false
			});
			if (files.Count >= 1)
			{
				await using var stream = await files[0].OpenReadAsync(); //Открывает файл для чтения 
				using var streamReader = new StreamReader(stream);
				newMatrix = ReadFilesToTwoArray(stream);
			}
			ShowMatrix(newMatrix);
		}

		/// <summary>
		/// Отображает матрицу
		/// </summary>
		public async void ShowMatrix(int[,] taskMatrix)
		{
			Matrix = new StackPanel();
			Matrix.Margin = new Avalonia.Thickness(0, 10, 0, 0);
            for (int i = 0; i < taskMatrix.GetLength(0); i++)
			{
				StackPanel lbHorizontal = new StackPanel();
				lbHorizontal.Margin = new Avalonia.Thickness(0, 0, 0, 10);
                List<TextBox> listTextBox = new List<TextBox>();
				for (int j = 0; j < taskMatrix.GetLength(1); j++)
				{
					TextBox textBox = new TextBox();
					textBox.Text = taskMatrix[i, j].ToString();
                    textBox.Padding = new Avalonia.Thickness(20);
					textBox.HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center;
					textBox.VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center;
					textBox.CornerRadius = new Avalonia.CornerRadius(10);
                    textBox.BorderThickness = new Avalonia.Thickness(1);
                    textBox.BorderBrush = new BrushConverter().ConvertFrom(Color.Parse("#09A0B3")) as Brush;
					textBox.Margin = new Avalonia.Thickness(10, 0);
                    listTextBox.Add(textBox);
				}
				lbHorizontal.Children.Add(listTextBox);
				lbHorizontal.Orientation = Avalonia.Layout.Orientation.Horizontal;
				Matrix.Children.Add(lbHorizontal);
			}
		}

		/// <summary>
		/// Чтение из файла в массив
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private int[,] ReadFilesToTwoArray(Stream stream)
		{
			try
			{
				List<List<int>> tempList = new List<List<int>>();
				using (StreamReader reader = new StreamReader(stream))
				{
					string? line;
					while ((line = reader.ReadLine()) != null)
					{
						tempList.Add(line.Split(';', ',', ' ').Select(int.Parse).ToList());
					}
				}
				// Преобразование tempList в двумерный массив int[,]
				int[,] array = new int[tempList.Count, tempList[0].Count];
				for (int i = 0; i < tempList.Count; i++)
				{
					for (int j = 0; j < tempList[i].Count; j++)
					{
						array[i, j] = tempList[i][j];
					}
				}
				return array;
			}
			catch
			{
				Trace.Listeners.Add(logFileListener);
				Trace.WriteLine("ERROR CreateSimulatorVM: Пользователь прикрепил пустой файл или неправильный формат");
				return null;
			}
		}

	}
}