using CommunityToolkit.Mvvm.ComponentModel;
using MathModelingSimulator.Models;
using MathModelingSimulator.Navigation;
using System.Text;

namespace MathModelingSimulator.ViewModels
{
	/// <summary>
	/// Наследуемся от ObservableObject (NuGet CommunityToolkit.Mvvm)
	/// </summary>
	public class ViewModelBase : ObservableObject
	{
		#region navigation
		// Главный экран. Регистрация, авторизация, меню
		private static PageSwitcher _startPage = new PageSwitcher();
		public static PageSwitcher StartPage { get => _startPage; set => _startPage = value; }

		// Вложенный экран. Элементы, открывающиеся под меню
		private static PageSwitcher _pageSwitch = new PageSwitcher();
		public static PageSwitcher PageSwitch { get => _pageSwitch; set => _pageSwitch = value; }
		#endregion

		private Trio33pContext _contextDb = new Trio33pContext();
		public Trio33pContext ContextDb { get => _contextDb; set => _contextDb = value; }

		//Ключ для хэша пароля БД	
		private readonly byte[] _keyDb = Encoding.UTF8.GetBytes("trioSecretKey");
		public byte[] KeyDb { get => _keyDb; }
	}
}
