using CommunityToolkit.Mvvm.ComponentModel;
using MathModelingSimulator.Models;
using MathModelingSimulator.Views;
using MathModelingSimulator.Navigation;
using System.Diagnostics;
using System.Text;
using Avalonia.Controls;

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

		#region database
		private static readonly Trio33pContext _contextDb = new Trio33pContext();
		public static Trio33pContext ContextDb { get => _contextDb; }

		//Ключ для хэша пароля БД	
		private static readonly byte[] _keyDb = Encoding.UTF8.GetBytes("trioSecretKey");
		public static byte[] KeyDb { get => _keyDb; }
		#endregion

		#region trace
		public static TextWriterTraceListener logFileListener = new TextWriterTraceListener("logFile.txt");

		public ViewModelBase()
		{
			Trace.AutoFlush = true;
		}
		#endregion

		private static User currentUser = new User();
		public static User CurrentUser { get => currentUser; set => currentUser = value; }
		public static int UserRole { get => currentUser.IdRole; }
	}
}
