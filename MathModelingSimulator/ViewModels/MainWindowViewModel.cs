
namespace MathModelingSimulator.ViewModels
{   
	/// <summary>
	/// Наследуем все классы от ViewModelBase для навигации
	/// </summary>
	public class MainWindowViewModel : ViewModelBase
	{
		#region ViewModel-objects
		static AuthorizationViewModel? authorizationVM = new AuthorizationViewModel();
		public static AuthorizationViewModel? AuthVM { get => authorizationVM; set => authorizationVM = value; }

        static RegistrationViewModel? registrationViewModel = new RegistrationViewModel();
        public static RegistrationViewModel? RegVM { get => registrationViewModel; set => registrationViewModel = value; }

        static MenuNavigationViewModel? menuNavigationVM;
		public static MenuNavigationViewModel? MenuVM { get => menuNavigationVM; set => menuNavigationVM = value; }

        static UserAccountViewModel? userAccountViewModel;
        public static UserAccountViewModel? UserAccountVM { get => userAccountViewModel; set => userAccountViewModel = value; }

		static StatisticsViewModel? statisticsViewModel;
		public static StatisticsViewModel? StatisticsVM { get => statisticsViewModel; set => statisticsViewModel = value; }

		static SimulatorsViewModel? simulatorsViewModel;
		public static SimulatorsViewModel? SimulatorsVM { get => simulatorsViewModel; set => simulatorsViewModel = value; }

        static TaskSimulatorsViewModel? taskSimulatorsViewModel;
        public static TaskSimulatorsViewModel? TaskSimulatorsVM { get => taskSimulatorsViewModel; set => taskSimulatorsViewModel = value; }

		static CreateSimulatorViewModel? createSimulatorViewModel;
		public static CreateSimulatorViewModel? CreateSimulatorVM { get => createSimulatorViewModel; set => createSimulatorViewModel = value; }
		#endregion

		public void ClearAuth()
		{
			authorizationVM = new AuthorizationViewModel();
		}
        public void ClearRegistration()
        {
            registrationViewModel = new RegistrationViewModel();
        }
	}
}
