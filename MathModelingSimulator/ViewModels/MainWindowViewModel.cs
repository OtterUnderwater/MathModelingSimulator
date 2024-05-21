using MathModelingSimulator.Views;

namespace MathModelingSimulator.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		public MainWindowViewModel()
		{
			PageSwitcher.View = new AuthorizationView();
		}

		public void Click(string name)
		{
			PageSwitcher.View = new AuthorizationView();
		}

	}
}
