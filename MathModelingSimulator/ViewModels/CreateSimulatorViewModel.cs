using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using MathModelingSimulator.Models;
using System.IO;
using System.Linq;
using DynamicData;
using System.Diagnostics;
using Avalonia.Media;
using MathModelingSimulator.Views;
using MathModelingSimulator.Function;

namespace MathModelingSimulator.ViewModels
{
	public class CreateSimulatorViewModel : MainWindowViewModel
	{
        #region PropertyObjects
        int? countRows = 0;
		public int? CountRows
		{
			get => countRows;
			set
			{
				countRows = value;
				GenerateMatrix();
			}
		}

        int? countColumns = 0;
		public int? CountColumns
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

		List<Simulator> listSimulators = new List<Simulator>();
		public List<Simulator> ListSimulators { get => listSimulators; set => SetProperty(ref listSimulators, value); }

		List<string> listSimulatorsView = new List<string>();
		public List<string> ListSimulatorsView { get => listSimulatorsView; set => SetProperty(ref listSimulatorsView, value); }

		string selectedSimulator = "";
		public string SelectedSimulator { get => selectedSimulator; set => SetProperty(ref selectedSimulator, value); }

        string answer = "";
        public string Answer { get => answer; set => SetProperty(ref answer, value); }
		#endregion

		int[,] _matrixBD;
		int _idTask = 0;

        private string messageRezult = "";
        public string MessageRezult { get => messageRezult; set => this.SetProperty(ref messageRezult, value); }

        private bool isVisibleRezult = false;
        public bool IsVisibleRezult { get => isVisibleRezult; set => this.SetProperty(ref isVisibleRezult, value); }

        public CreateSimulatorViewModel()
		{
			listSimulators = ContextDb.Simulators.ToList();
			listSimulatorsView = listSimulators.Select(it => it.Name).ToList<string>();
			selectedSimulator = ListSimulatorsView[0];
		}

		public CreateSimulatorViewModel(int[,] updatingMatrix, string selectedSimulator, int idTask)
		{
            listSimulators = ContextDb.Simulators.ToList();
            listSimulatorsView = listSimulators.Select(it => it.Name).ToList<string>();
            CountRows = updatingMatrix.GetLength(0);
			CountColumns = updatingMatrix.GetLength(1);
            ShowMatrix(updatingMatrix);
            IsVisibleEnterMatrix = true;
            SelectedSimulator = ListSimulatorsView.First(it => it == selectedSimulator);
			this._idTask = idTask;
        }

		public void GenerateAnswer()
        {
			if(CountRows != 0 && CountColumns != 0)
			{
                IsVisibleRezult = false;
				fillMatrix();
                switch (selectedSimulator)
                {
                    case "Симплекс метод на минимум": GetMinSimplexMetod(); break;
                    case "Симплекс метод на максимум": GetMaxSimplexMetod(); break;
                    case "Задача Коммивояжера": GetTravelingSalesmanProblem(); break;
                    case "Транспортные задачи. Метод аппроксимации Фогеля": GetAnswerZadFogel(); break;
                    case "Задача Джонсона": GetAnswerZadDzhonsons(); break;
                    case "Алгоритм Дейкстры": MessageRezult = "Я пока не умею такое решать"; break;
                }
            }
		}

		void GetAnswerZadFogel()
		{
            BasicMethods mFodel = new BasicMethods();
			var ai = new int[_matrixBD.GetLength(1) - 1];
            int cI = 0;
            for (int j = 1; j < _matrixBD.GetLength(1); j++)
            {
				ai[cI] = _matrixBD[0,j];
				cI++;
            }
            cI = 0;
            var bj = new int[_matrixBD.GetLength(0) - 1];
            for (int j = 1; j < _matrixBD.GetLength(0); j++)
            {
                bj[cI] = _matrixBD[j, 0];
                cI++;
            }
			var tarif = new int[ai.Length, bj.Length];
			cI = 0;
			for (int j = 1; j < _matrixBD.GetLength(0); j++)
			{
				int cJ = 0;
				for (int k = 1; k < _matrixBD.GetLength(1); k++)
				{
					tarif[cI, cJ] = _matrixBD[j, k];
                    cJ++;
                }
				cI++;
            }
            var rezult = mFodel.Start(bj, ai, tarif);
            if (rezult != null)
            {
                Answer = rezult.ToString();
            }
            else
            {
                IsVisibleRezult = true;
                MessageRezult = "Ваша матрица не подходит для этой задачи";
                Answer = "0";
            }
        }


        void fillMatrix()
		{
            var countRows = matrix.Children.Count;
            var countColumns = (matrix.Children[0] as StackPanel).Children.Count;
            _matrixBD = new int[countRows, countColumns];
            for (int i = 0; i < countRows; i++)
            {
                for (int j = 0; j < countColumns; j++)
                {
                    var buffer = (matrix.Children[i] as StackPanel).Children[j].Name.Split(" ").Select(int.Parse).ToList();
                    _matrixBD[Convert.ToInt32(buffer[0]), Convert.ToInt32(buffer[1])] = Convert.ToInt32(((matrix.Children[i] as StackPanel).Children[j] as TextBox).Text);
                }
            }
        }

        void GetAnswerZadDzhonsons()
		{
            Johnson_sAlgorithm johnson_SAlgorithm = new Johnson_sAlgorithm();
			var rezult = johnson_SAlgorithm.Start(_matrixBD);
			if(rezult != null)
			{
                Answer = rezult.ToString();
            }
			else
			{
				IsVisibleRezult = true;
                MessageRezult = "Ваша матрица не подходит для этой задачи";
				Answer = "0";
            }
		}
		void GetMinSimplexMetod() {
            SimplexMetod simplexMetod = new SimplexMetod();
            var rezult = simplexMetod.MainSimplexMetod(_matrixBD, false);
            if (rezult != null)
            {
                Answer = rezult.ToString();
            }
            else
            {
                IsVisibleRezult = true;
                MessageRezult = "Ваша матрица не подходит для этой задачи";
                Answer = "0";
            }
        }
        void GetMaxSimplexMetod()
        {
            SimplexMetod simplexMetod = new SimplexMetod();
            var rezult = simplexMetod.MainSimplexMetod(_matrixBD, true);
            if (rezult != null)
            {
                Answer = rezult.ToString();
            }
            else
            {
                IsVisibleRezult = true;
                MessageRezult = "Ваша матрица не подходит для этой задачи";
                Answer = "0";
            }
        }
		void GetTravelingSalesmanProblem() {
            TravelingSalesmanProblem travelingSalesmanProblem = new TravelingSalesmanProblem();
			if (CountRows == CountColumns)
			{
				var rezult = travelingSalesmanProblem.MainTravelingSalesmanProblem(_matrixBD);

				if (rezult != null)
				{
					Answer = rezult.ToString();
				}
				else
				{
					IsVisibleRezult = true;
					MessageRezult = "Ваша матрица не подходит для этой задачи";
					Answer = "0";
				}
			}
			else {
                IsVisibleRezult = true;
                MessageRezult = "Ваша матрица не подходит для этой задачи";
                Answer = "0";
            }
            
        }
        public void CreateTask()
		{
			if (CountRows != 0 && CountColumns != 0)
			{
                fillMatrix();
                SimulatorTask newTask = new SimulatorTask();
                var idSimulator = listSimulators.First(it => it.Name == selectedSimulator).Id;
                newTask.IdSimulator = idSimulator;
                newTask.ZadanieMatrix = _matrixBD;
                if (_idTask != 0)
                {
                    newTask.Id = _idTask;
                }
                newTask.Answer = Convert.ToInt32(answer);
                ContextDb.SimulatorTasks.Add(newTask);
                //Добавить функцию генерацию ответа!
                ContextDb.SaveChanges();
                SimulatorsVM = new SimulatorsViewModel();
                PageSwitch.View = new SimulatorsView();
            }
        }


        /// <summary>
        /// Выбор 2. Ввод матрицы
        /// </summary>
        public void EnterMatrix()
		{
			Matrix = new StackPanel();
			IsVisibleEnterMatrix = true; //видимость ввода строк и столбцов
		}

		public void Cancel()
		{
			PageSwitch.View = new SimulatorsView();
		}

		/// <summary>
		/// При изменении строк и столбцов - генерация полей ввода для матрицы
		/// </summary>
		public void GenerateMatrix()
		{
			Matrix = new StackPanel();
			if (countRows > 0 && countColumns > 0)
			{
				int[,] taskMatrix = new int[(int)countRows, (int)countColumns];
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
				int count = 0;
				for (int j = 0; j < taskMatrix.GetLength(1); j++)
				{
					TextBox textBox = new TextBox();
					textBox.Text = taskMatrix[i, j].ToString();
                    textBox.Padding = new Avalonia.Thickness(20);
					textBox.HorizontalContentAlignment = Avalonia.Layout.HorizontalAlignment.Center;
					textBox.VerticalContentAlignment = Avalonia.Layout.VerticalAlignment.Center;
					textBox.CornerRadius = new Avalonia.CornerRadius(10);
                    textBox.BorderThickness = new Avalonia.Thickness(1);
                    count++;
                    textBox.Name = $"{i} {j} {count}"; //i j (номер элемента)
                    textBox.BorderBrush = new SolidColorBrush(0xFF09A0B3);
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
				CountRows = array.GetLength(0);
				CountColumns = array.GetLength(1);
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