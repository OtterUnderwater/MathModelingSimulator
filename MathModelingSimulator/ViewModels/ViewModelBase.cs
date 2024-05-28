using CommunityToolkit.Mvvm.ComponentModel;
using MathModelingSimulator.Models;
using MathModelingSimulator.Navigation;
using System;
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

		private static readonly Trio33pContext _contextDb = new Trio33pContext();
		public static Trio33pContext ContextDb { get => _contextDb; }

		//Ключ для хэша пароля БД	
		private static readonly byte[] _keyDb = Encoding.UTF8.GetBytes("trioSecretKey");
		public static byte[] KeyDb { get => _keyDb; }

		private static User currentUser = new User();
		public static User CurrentUser { get => currentUser; set => currentUser = value; }

		private static Guid userId;
		public static Guid UserId { get => userId; set => userId = value; }

		public static int UserRole { get => currentUser.IdRole; }
    }
}
