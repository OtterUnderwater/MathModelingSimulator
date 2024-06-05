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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		public string SelectedSimulator 
		{ 
			get 
            {
                if (selectedSimulator == "Алгоритм Дейкстры")
                {
                    MessageDykstra = "При вводе вручную: если до точки нет пути, оставьте поле пустым\nПри считывании из файла: если до точки нет пути укажите знаком\"-\"\nВ ответ запишите минимальный путь от 1 точки до любой, путь до которой наибольший";
                    IsVisibleMessageDykstra = true;
                }
                else
                {
                    IsVisibleMessageDykstra = false;
                }
                return selectedSimulator;
            } 
			set 
			{ 
				SetProperty(ref selectedSimulator, value); 
				if(selectedSimulator == "Алгоритм Дейкстры")
				{
					MessageDykstra = "При вводе вручную: если до точки нет пути, оставьте поле пустым\nПри считывании из файла: если до точки нет пути укажите знаком\"-\"\nВ ответ запишите минимальный путь от 1 точки до любой, путь до которой наибольший";
					IsVisibleMessageDykstra = true;
				}
				else
				{
					IsVisibleMessageDykstra = false;
				}
			}
		}

        string answer = "";
        public string Answer { get => answer; set => SetProperty(ref answer, value); }
		#endregion

		int[,] _matrixBD;
		int?[,] _matrixBDD;
		int _idTask = 0;

        private string messageRezult = "";
        public string MessageRezult { get => messageRezult; set => this.SetProperty(ref messageRezult, value); }

        private string messageDykstra = "";
        public string MessageDykstra { get => messageDykstra; set => this.SetProperty(ref messageDykstra, value); }

        private bool isVisibleRezult = false;
        public bool IsVisibleRezult { get => isVisibleRezult; set => this.SetProperty(ref isVisibleRezult, value); }

        private bool isVisibleMessageDykstra = false;
        public bool IsVisibleMessageDykstra { get => isVisibleMessageDykstra; set => this.SetProperty(ref isVisibleMessageDykstra, value); }

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
                    case "Алгоритм Дейкстры": GetAnswerZadDeykstra(); break;
                }
            }
		}

        void GetAnswerZadDeykstra()
        {
            if(CountRows == CountColumns)
            {
                Dijkstra_sAlgorithm dijkstra_SAlgorithm = new Dijkstra_sAlgorithm();
                var aMatrix = new int[_matrixBDD.GetLength(0), _matrixBDD.GetLength(0)];
                List<(int, int)> aRoutes = new List<(int, int)>();
                for (int i = 0; i < _matrixBDD.GetLength(0); i++)
                {
                    for (int j = 0; j < _matrixBDD.GetLongLength(1); j++)
                    {
                        if (_matrixBDD[i, j] != null)
                        {
                            aMatrix[i, j] = (int)_matrixBDD[i, j];
                            _matrixBD[i, j] = (int)_matrixBDD[i, j];
                        }
                        else
                        {
                            aMatrix[i, j] = -1;
                            _matrixBD[i, j] = -1;
                        }
                    }
                    if (i != 0) aRoutes.Add((i, int.MaxValue));
                }
                var rezult = dijkstra_SAlgorithm.Start(_matrixBDD.GetLength(0), 0, aMatrix, aRoutes);
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
            else
            {
                IsVisibleRezult = true;
                MessageRezult = "Ваша матрица не подходит для этой задачи";
                Answer = "0";
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
            if (selectedSimulator != "Алгоритм Дейкстры")
            {
                _matrixBD = new int[countRows, countColumns];
            }
            for (int i = 0; i < countRows; i++)
            {
                for (int j = 0; j < countColumns; j++)
                {
                    if (selectedSimulator != "Алгоритм Дейкстры")
                    {
                        var buffer = (matrix.Children[i] as StackPanel).Children[j].Name.Split(" ").Select(int.Parse).ToList();
                        _matrixBD[Convert.ToInt32(buffer[0]), Convert.ToInt32(buffer[1])] = Convert.ToInt32(((matrix.Children[i] as StackPanel).Children[j] as TextBox).Text);
                    }
                    else
                    {
                        if(CountRows == CountColumns)
                        {
                            List<int?> buffer = (matrix.Children[i] as StackPanel)?.Children[j]?.Name
                                .Split(" ")
                                .Select(it => (it == "-") ? (int?)null : int.TryParse(it, out var result) ? (int?)result : null)
                                .ToList();
                            string textValue = ((matrix.Children[i] as StackPanel)?.Children[j] as TextBox)?.Text;
                            int? valueToAdd = null;

                            if (!string.IsNullOrEmpty(textValue))
                            {
                                valueToAdd = Convert.ToInt32(textValue);
                            }

                            _matrixBDD[buffer[0] ?? 0, buffer[1] ?? 0] = valueToAdd;
                        }
                    }
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
			int?[,] newMatrixD = null;
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
				if (selectedSimulator == "Алгоритм Дейкстры")
				{
                    newMatrixD = ReadFilesToTwoArrayD(stream);
                    ShowMatrixD(newMatrixD);
                }
				else
				{
                    newMatrix = ReadFilesToTwoArray(stream);
                    ShowMatrix(newMatrix);
                } 
			}
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
		/// Отображает матрицу
		/// </summary>
		public async void ShowMatrixD(int?[,] taskMatrix)
        {
            Matrix = new StackPanel();
            Matrix.Margin = new Avalonia.Thickness(0, 10, 0, 0);
            _matrixBD = new int[taskMatrix.GetLength(0), taskMatrix.GetLength(1)];
            for (int i = 0; i < taskMatrix.GetLength(0); i++)
            {
                StackPanel lbHorizontal = new StackPanel();
                lbHorizontal.Margin = new Avalonia.Thickness(0, 0, 0, 10);
                List<TextBox> listTextBox = new List<TextBox>();
                int count = 0;
               
                for (int j = 0; j < taskMatrix.GetLength(1); j++)
                {
                    if (taskMatrix[i,j] == null)
                    {
                        _matrixBD[i,j] = -1;
                    }
                    else
                    {
                        _matrixBD[i, j] = (int)taskMatrix[i, j];
                    }
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
                    if(i == j)
                    {
                        textBox.Text = null;
                        textBox.IsEnabled = false;
                    }
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

        /// <summary>
        /// Чтение из файла в массив
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private int?[,] ReadFilesToTwoArrayD(Stream stream)
        {
            try
            {
                List<List<int?>> tempListD = new List<List<int?>>();
                using (StreamReader reader = new StreamReader(stream))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var listBuf = line.Split(';', ',', ' ').ToList();
                        List<int?> tempD = new List<int?>();
                        for (int i = 0; i < listBuf.Count; i++)
                        {
                            if (listBuf[i] == "-") tempD.Add(null);
                            else tempD.Add(Convert.ToInt32(listBuf[i]));
                        }
                        tempListD.Add(tempD);
                    }
                }
                _matrixBDD = new int?[tempListD.Count, tempListD[0].Count];
                for (int i = 0; i < tempListD.Count; i++)
                {
                    for (int j = 0; j < tempListD[i].Count; j++)
                    {
                        _matrixBDD[i, j] = tempListD[i][j];
                    }
                }
                CountRows = _matrixBDD.GetLength(0);
                CountColumns = _matrixBDD.GetLength(1);
                return _matrixBDD;
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