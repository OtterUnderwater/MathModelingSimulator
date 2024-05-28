using System;
using System.Collections.Generic;
using MathModelingSimulator.Views;
using ReactiveUI;

namespace MathModelingSimulator.ViewModels
{
	public class MenuNavigationViewModel : MainWindowViewModel
    {
		static string btnName = "Статистика";

		public MenuNavigationViewModel()
		{
			//1 - Преподаватель
			//2 - Студент
			if(UserRole == 1)
			{
				btnName = "Статистика учеников";
			}
		}

        public static string BtnName { get => btnName; set => btnName = value; }

        public void GetPageAccount()
		{
			PageSwitch.View = new UserAccountView();
        }

		public void GetPageSimulators()
		{
			PageSwitch.View = new SimulatorsView();
		}

		public void GetPageStatistics()
		{
			PageSwitch.View = new StatisticsView();
		}
	}
}