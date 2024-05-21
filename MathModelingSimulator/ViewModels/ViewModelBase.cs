using CommunityToolkit.Mvvm.ComponentModel;
using MathModelingSimulator.Navigation;

namespace MathModelingSimulator.ViewModels
{
	/// <summary>
	/// Наследуемся от ObservableObject (NuGet CommunityToolkit.Mvvm)
	/// </summary>
	public class ViewModelBase : ObservableObject
	{
		#region navigation
		private static PageSwitcher _pageSwitcher = new PageSwitcher();
		public static PageSwitcher PageSwitcher { get => _pageSwitcher; set => _pageSwitcher = value; }
		#endregion
	}
}
