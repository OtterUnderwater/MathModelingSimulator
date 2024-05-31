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

		public void GetCreateSimulator()
        {
            PageSwitch.View = new CreateSimulatorView();
        }

        bool userIsTeacher;

		List<SimulatorTask> listSimulatorsTaskTeacher = new List<SimulatorTask> ();

		List<SimulatorTaskView> listSimulatorsTaskTeacherView = new List<SimulatorTaskView> ();
        public List<SimulatorTaskView> ListSTaskTeacherView { get => listSimulatorsTaskTeacherView; set => SetProperty(ref listSimulatorsTaskTeacherView, value); }

        List<int> indTasks = new List<int> ();
        public List<int> IndTasks { get => indTasks; set => SetProperty(ref indTasks, value); }

        int taskSelected;
        public int IndTasksSelected { get => taskSelected; set => SetProperty(ref taskSelected, value); }

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
            listSimulatorsTaskTeacher = ContextDb.SimulatorTasks.ToList();
            for (int i = 0; i < listSimulatorsTaskTeacher.Count; i++)
            {
                string Zadanie = $"{i + 1}. {GetNameSimulatorByID(listSimulatorsTaskTeacher[i].IdSimulator)}";
                string ZadanieMatrix = "";
                for (int j = 0; j < listSimulatorsTaskTeacher[i].ZadanieMatrix.GetLength(0); j++) {
                    for (int k = 0; k < listSimulatorsTaskTeacher[i].ZadanieMatrix.GetLength(1); k++){
                        ZadanieMatrix += $"{listSimulatorsTaskTeacher[i].ZadanieMatrix[j, k]} ";
                    }
                    if(j != listSimulatorsTaskTeacher[i].ZadanieMatrix.GetLength(0) - 1) ZadanieMatrix += "\n";
                }
                listSimulatorsTaskTeacherView.Add(new SimulatorTaskView((listSimulatorsTaskTeacher[i].Id, i + 1), Zadanie,
                    ZadanieMatrix, "Ответ: " + listSimulatorsTaskTeacher[i].Answer));
            }
            IndTasks = ListSTaskTeacherView.Select(it => it.Id.Item2).ToList();
            IndTasksSelected = IndTasks[0];
        }

        string GetNameSimulatorByID(int ID)
        {
            return listSimulators.FirstOrDefault(it => it.Id == ID).Name;
        }

        public void DeleteTask()
        {
            SimulatorTaskView smtv = listSimulatorsTaskTeacherView.First(it => it.Id.Item2 == taskSelected);
            SimulatorTask removedTask = listSimulatorsTaskTeacher.FirstOrDefault(it => it.Id == smtv.Id.Item1);
            ContextDb.SimulatorTasks.Remove(removedTask);
            ContextDb.SaveChanges();
            listSimulatorsTaskTeacherView.Remove(smtv);
            List<int> indTasks = ListSTaskTeacherView.Select(it => it.Id.Item2).ToList();
            IndTasks = indTasks;
            listSimulatorsTaskTeacher.Remove(removedTask);
            IndTasksSelected = IndTasks[0];

        }

	}

    public class SimulatorTaskView
    {
        public (int, int) Id { get; set; }
        public string Zadanie { get; set; }
        public string ZadanieMatrix { get; set; }
        public string? Answer { get; set; }

        public SimulatorTaskView((int, int) id, string Zadanie, string ZadanieMatrix, string? Answer)
        {
            Id = id;
            this.Zadanie = Zadanie;
            this.ZadanieMatrix = ZadanieMatrix;
            this.Answer = Answer;
        }
    }
}