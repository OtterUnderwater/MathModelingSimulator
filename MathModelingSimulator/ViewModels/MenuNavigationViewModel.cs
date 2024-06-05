using Avalonia.Animation.Easings;
using Avalonia.Threading;
using MathModelingSimulator.Views;
using System.Threading;
using System.Threading.Tasks;

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
			SimulatorsVM = new SimulatorsViewModel();
			PageSwitch.View = new SimulatorsView();
			UpdateStatisticsAsync();
		}

		async Task UpdateStatisticsAsync()
		{
			await Task.Run(() =>
			{
				Dispatcher.UIThread.InvokeAsync(UpdateStatistics);
			});
		}
		async Task UpdateStatistics()
		{
			StatisticsVM = new StatisticsViewModel();
		}

		public void GetPageAccount()
		{
			UserAccountVM = new UserAccountViewModel();
			PageSwitch.View = new UserAccountView();
		}

		public void GetPageSimulators()
		{
			SimulatorsVM = new SimulatorsViewModel();
			PageSwitch.View = new SimulatorsView();
		}

		public void GetPageStatistics()
		{
			PageSwitch.View = new StatisticsView();
		}
	}
}