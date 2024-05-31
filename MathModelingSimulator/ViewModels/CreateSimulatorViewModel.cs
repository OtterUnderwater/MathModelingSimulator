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

namespace MathModelingSimulator.ViewModels
{
	public class CreateSimulatorViewModel : MainWindowViewModel
	{
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

		/// <summary>
		/// ����� 2. ���� �������
		/// </summary>
		public void EnterMatrix()
		{
			Matrix = new StackPanel();
			IsVisibleEnterMatrix = true; //��������� ����� ����� � ��������
		}

		/// <summary>
		/// ��� ��������� ����� � �������� - ��������� ����� ����� ��� �������
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
		/// ����� 1. ������ �� �����
		/// </summary>
		public async void AttachFileClick()
		{
			int[,] newMatrix = new int[0, 0];
			IsVisibleEnterMatrix = false;
			var topLevel = TopLevel.GetTopLevel(PageSwitch.View);
			var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
			{
				Title = "�������� ��������� ����",
				AllowMultiple = false
			});
			if (files.Count >= 1)
			{
				await using var stream = await files[0].OpenReadAsync(); //��������� ���� ��� ������ 
				using var streamReader = new StreamReader(stream);
				newMatrix = ReadFilesToTwoArray(stream);
			}
			ShowMatrix(newMatrix);
		}

		/// <summary>
		/// ���������� �������
		/// </summary>
		public async void ShowMatrix(int[,] taskMatrix)
		{
			Matrix = new StackPanel();
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

		/// <summary>
		/// ������ �� ����� � ������
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		private int[,] ReadFilesToTwoArray(Stream stream)
		{
			List<List<int>> tempList = new List<List<int>>();
			using (StreamReader reader = new StreamReader(stream))
			{
				string? line;
				while ((line = reader.ReadLine()) != null)
				{
					tempList.Add(line.Split(';').Select(int.Parse).ToList());
				}
			}
			// �������������� tempList � ��������� ������ int[,]
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

	}
}