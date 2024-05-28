using MathModelingSimulator.Views;
using Microsoft.EntityFrameworkCore;
using System;

namespace MathModelingSimulator.ViewModels
{   
	/// <summary>
	/// Наследуем все классы от ViewModelBase для навигации
	/// </summary>
	public class MainWindowViewModel : ViewModelBase
	{
		#region ViewModel-objects
		static AuthorizationViewModel authorizationVM = new AuthorizationViewModel();
		public static AuthorizationViewModel AuthVM { get => authorizationVM; set => authorizationVM = value; }

        static RegistrationViewModel registrationViewModel = new RegistrationViewModel();
        public static RegistrationViewModel RegVM { get => registrationViewModel; set => registrationViewModel = value; }

        static MenuNavigationViewModel menuNavigationVM = new MenuNavigationViewModel();
		public static MenuNavigationViewModel MenuVM { get => menuNavigationVM; set => menuNavigationVM = value; }

        static UserAccountViewModel userAccountViewModel;
        public static UserAccountViewModel UserAccountVM { get => userAccountViewModel; set => userAccountViewModel = value; }

        static SimulatorsViewModel simulatorsViewModel = new SimulatorsViewModel();
        public static SimulatorsViewModel SimulatorsVM { get => simulatorsViewModel; set => simulatorsViewModel = value; }

        #endregion


        public MainWindowViewModel()
		{
			StartPage.View = new AuthorizationView();
			PageSwitch.View = new SimulatorsView();
		}
	}
}
