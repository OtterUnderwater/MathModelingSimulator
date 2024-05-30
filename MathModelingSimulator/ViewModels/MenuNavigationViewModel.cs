using System;
using System.Collections.Generic;
using MathModelingSimulator.Views;
using ReactiveUI;

namespace MathModelingSimulator.ViewModels
{
	public class MenuNavigationViewModel : MainWindowViewModel
	{
		static string btnName = "����������";
		public MenuNavigationViewModel()
		{
			//1 - �������������
			//2 - �������
			if (UserRole == 1)
			{
				btnName = "���������� ��������";
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
			//�������� �� �������� ��������� ��� ���� ���������� � �������
			StatisticsVM = new StatisticsViewModel();
			PageSwitch.View = new StatisticsView();
		}
	}
}