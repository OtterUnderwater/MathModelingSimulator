using System;
using System.Collections.Generic;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using ReactiveUI;
using SkiaSharp;
using System.Linq;
using MathModelingSimulator.Views;

namespace MathModelingSimulator.ViewModels
{
	public class SimulatorsViewModel : MainWindowViewModel
	{
		bool isVisibleTeacher = true;
		public bool IsVisibleTeacher { get => isVisibleTeacher; set => SetProperty(ref isVisibleTeacher, value); }

		bool isVisibleStudent = true;
		public bool IsVisibleStudent { get => isVisibleStudent; set => SetProperty(ref isVisibleStudent, value); }

		public void GetTask()
        {
            PageSwitch.View = new TaskSimulatorsView();
        }

        /// <summary>
        /// Проверяет роль пользователя и запускает нужный экран
        /// </summary>
        public SimulatorsViewModel()
		{
			if (ContextDb.Roles.First(r => r.IdRole == CurrentUser.IdRole).Role1 == "Преподаватель")
			{
				IsVisibleTeacher = true;
				IsVisibleStudent = false;
			}
			else
			{
				IsVisibleTeacher = false;
				IsVisibleStudent = true;
			}
		}
	}
}