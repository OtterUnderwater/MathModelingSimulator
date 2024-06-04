using Avalonia.Controls;
using MathModelingSimulator.ViewModels;
using MathModelingSimulator.Views;

namespace MathModelingSimulator.Navigation
{
	/// <summary>
	/// Используется для смены страницы/активности
	/// </summary>
	public class PageSwitcher : ViewModelBase
	{
		private UserControl? view = new AuthorizationView();

		/// <summary>
		/// Текущая отображаемая страница
		/// </summary>
		public UserControl View
		{
			get => view;
			set
			{
                view = value;
                OnPropertyChanged(nameof(View));
            }
        }
	}
}
