using MathModelingSimulator.Views;
using Microsoft.EntityFrameworkCore;

namespace MathModelingSimulator.ViewModels
{   
	/// <summary>
	/// Наследуем все классы от ViewModelBase для навигации
	/// </summary>
	public class MainWindowViewModel : ViewModelBase
	{
		#region ViewModel-objects
		AuthorizationViewModel authorizationVM = new AuthorizationViewModel();
		public AuthorizationViewModel AuthVM { get => authorizationVM; set => authorizationVM = value; }

        RegistrationViewModel registrationViewModel = new RegistrationViewModel();
        public RegistrationViewModel RegVM { get => registrationViewModel; set => registrationViewModel = value; }

        MenuNavigationViewModel menuNavigationVM = new MenuNavigationViewModel();
		public MenuNavigationViewModel MenuVM { get => menuNavigationVM; set => menuNavigationVM = value; }
        
        #endregion

        public MainWindowViewModel()
		{
			StartPage.View = new AuthorizationView();
			PageSwitch.View = new SimulatorsView();
		}
	}
}
