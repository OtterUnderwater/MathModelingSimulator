using System;
using System.Collections.Generic;
using MathModelingSimulator.Views;
using ReactiveUI;

namespace MathModelingSimulator.ViewModels
{
	public class MenuNavigationViewModel : ViewModelBase
	{
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