using System.Linq;
using MathModelingSimulator.Views;
using MathModelingSimulator.Models;
using MathModelingSimulator.Function;
using System.Security.Cryptography;
using System.Text;
using System;

namespace MathModelingSimulator.ViewModels
{

	public class AuthorizationViewModel : ViewModelBase
	{
		private string login = "";
		public string Login { get => login; set => login = value; }

        private string password = "";
		public string Password { get => password; set => password = value; }

        private string message = "";
		public string Message { get => message; set => this.SetProperty(ref message, value); }

		public void GoToRegistration()
		{
			StartPage.View = new RegistrationView();
		}

		//≈сли авторизаци€/регистраци€ успешна - открываем меню
		public void CheckAuthorization()
		{
			Security security = new Security();
			string passwordHash = security.GetHashPassword(password);
			User? authorization = ContextDb.Users.FirstOrDefault(u => u.Login == Login && u.Password == passwordHash);
			if (authorization == null)
			{
				Message = "“акого пользовател€ нет";
			}
			else
			{
				StartPage.View = new MenuNavigationView();
			}
		}
	}
}