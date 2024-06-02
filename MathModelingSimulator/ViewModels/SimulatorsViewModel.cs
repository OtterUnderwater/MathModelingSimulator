using System.Collections.Generic;
using System.Linq;
using MathModelingSimulator.Views;
using MathModelingSimulator.Models;
using Avalonia.Media;
using Avalonia.Controls;
using DynamicData;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MathModelingSimulator.ViewModels
{
	public class SimulatorsViewModel : MainWindowViewModel
	{
		bool isVisibleTeacher = true;
		public bool IsVisibleTeacher { get => isVisibleTeacher; set => SetProperty(ref isVisibleTeacher, value); }

		bool isVisibleStudent = true;
		public bool IsVisibleStudent { get => isVisibleStudent; set => SetProperty(ref isVisibleStudent, value); }
		
        public void GetTaskSimulator(int numberTask)
		{
			TaskSimulatorsVM = new TaskSimulatorsViewModel();
			TaskSimulatorsVM.GetTask(numberTask);
		}
		public void GetCreateSimulator()
        {
            PageSwitch.View = new CreateSimulatorView();
        }

        bool userIsTeacher;

		List<SimulatorTask> listSimulatorsTaskTeacher = new List<SimulatorTask> ();

		List<SimulatorTaskView> listTaskTeacherView = new List<SimulatorTaskView> ();
        public List<SimulatorTaskView> ListTaskTeacherView { get => listTaskTeacherView; set => SetProperty(ref listTaskTeacherView, value); }

        List<int> tasks = new List<int> ();
        public List<int> Tasks { get => tasks; set => SetProperty(ref tasks, value); }

        int taskSelected;
        public int TaskSelected { get => taskSelected; set => SetProperty(ref taskSelected, value); }

        List<Simulator> listSimulators= new List<Simulator> ();

		/// <summary>
		/// Проверяет роль пользователя и запускает нужный экран
		/// </summary>
		public SimulatorsViewModel()
        {
            //Получаем лист тренажеров
            listSimulators = ContextDb.Simulators.ToList();
            if (ContextDb.Roles.First(r => r.IdRole == CurrentUser.IdRole).Role1 == "Преподаватель")
			{
				IsVisibleTeacher = true;
				IsVisibleStudent = false;
				userIsTeacher = true;
				GetSimulatorsTaskTeacher();
            }
			else
			{
				IsVisibleTeacher = false;
				IsVisibleStudent = true;
                userIsTeacher = false;
            }
		}

		void GetSimulatorsTaskTeacher()
		{
            //Получаем лист заданий
            listSimulatorsTaskTeacher = ContextDb.SimulatorTasks.OrderBy(it => it.Id).ToList();
            if(listSimulatorsTaskTeacher.Count != 0)
            {
                for (int i = 0; i < listSimulatorsTaskTeacher.Count; i++)
                {
                    string Zadanie = $"{listSimulatorsTaskTeacher[i].Id}. {GetNameSimulatorByID(listSimulatorsTaskTeacher[i].IdSimulator)}";
                    string ZadanieMatrix = "";
                    for (int j = 0; j < listSimulatorsTaskTeacher[i].ZadanieMatrix.GetLength(0); j++)
                    {
                        for (int k = 0; k < listSimulatorsTaskTeacher[i].ZadanieMatrix.GetLength(1); k++)
                        {
                            ZadanieMatrix += $"{listSimulatorsTaskTeacher[i].ZadanieMatrix[j, k]} ";
                        }
                        if (j != listSimulatorsTaskTeacher[i].ZadanieMatrix.GetLength(0) - 1) ZadanieMatrix += "\n";
                    }
                    listTaskTeacherView.Add(new SimulatorTaskView((listSimulatorsTaskTeacher[i].Id, i + 1), Zadanie,
                        ZadanieMatrix, "Ответ: " + listSimulatorsTaskTeacher[i].Answer, listSimulatorsTaskTeacher[i].ZadanieMatrix,
                        GetNameSimulatorByID(listSimulatorsTaskTeacher[i].IdSimulator)));
                }
                tasks = ListTaskTeacherView.Select(it => it.Id.Item1).ToList();
                taskSelected = tasks[0];
            }
        }

        public void UpdateTask()
        {
            if (listSimulatorsTaskTeacher.Count != 0)
            {
                var chtoto = ListTaskTeacherView.First(it => it.Id.Item1 == taskSelected);
                int[,] updatingMatrix = chtoto.Matrix;
                CreateSimulatorVM = new CreateSimulatorViewModel(updatingMatrix, chtoto.Task, chtoto.Id.Item1);
                ContextDb.SimulatorTasks.Remove(listSimulatorsTaskTeacher.First(it => it.Id == chtoto.Id.Item1));
                ContextDb.SaveChanges();
                PageSwitch.View = new CreateSimulatorView();
            }
        }

        string GetNameSimulatorByID(int ID)
        {
            return listSimulators.FirstOrDefault(it => it.Id == ID).Name;
        }

        public void DeleteTask()
        {
            if (listSimulatorsTaskTeacher.Count != 0)
            {
                SimulatorTaskView smtv = listTaskTeacherView.First(it => it.Id.Item2 == taskSelected);
                SimulatorTask removedTask = listSimulatorsTaskTeacher.FirstOrDefault(it => it.Id == smtv.Id.Item1);
                ContextDb.SimulatorTasks.Remove(removedTask);
                ContextDb.SaveChanges();
                SimulatorsVM = new SimulatorsViewModel();
                PageSwitch.View = new SimulatorsView();
            }
        }

	}

    public class SimulatorTaskView
    {
        public (int, int) Id { get; set; }
        public string Zadanie { get; set; }
        public string Task { get; set; }
        public string ZadanieMatrix { get; set; }
        public int[,] Matrix { get; set; }
        public string? Answer { get; set; }

        public SimulatorTaskView((int, int) id, string Zadanie, string ZadanieMatrix, string? Answer, int[,] matrix, string task)
        {
            Id = id;
            this.Zadanie = Zadanie;
            this.ZadanieMatrix = ZadanieMatrix;
            this.Answer = Answer;
            Matrix = matrix;
            Task = task;
        }
    }
}