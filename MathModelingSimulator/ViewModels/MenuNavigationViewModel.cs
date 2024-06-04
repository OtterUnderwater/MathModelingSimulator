using MathModelingSimulator.Views;

namespace MathModelingSimulator.ViewModels
{
	public class MenuNavigationViewModel : MainWindowViewModel
	{
		#region PropertyObjects
		static string btnName = "Статистика";
		public static string BtnName { get => btnName; set => btnName = value; }
		#endregion

		public MenuNavigationViewModel()
		{
			//1 - Преподаватель
			//2 - Студент
			if (UserRole == 1)
			{
				btnName = "Статистика учеников";
            }
            UserAccountVM = new UserAccountViewModel();
            StatisticsVM = new StatisticsViewModel();
            SimulatorsVM = new SimulatorsViewModel();
			CreateSimulatorVM = new CreateSimulatorViewModel();
			TaskSimulatorsVM = new TaskSimulatorsViewModel();
            PageSwitch.View = new SimulatorsView();
        }

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