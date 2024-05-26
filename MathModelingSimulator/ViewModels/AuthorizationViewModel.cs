using System.Linq;
using MathModelingSimulator.Views;
using MathModelingSimulator.Models;

namespace MathModelingSimulator.ViewModels
{

	public class AuthorizationViewModel : ViewModelBase
	{
		private string login = "";
		public string Login { get => login; set => login = value; }

		private string massage = "";
		//SetProperty для обновления данных на странице
		public string Massage { get => massage; set => this.SetProperty(ref massage, value); }

		public void Click(string name)
		{
			StartPage.View = new RegistrationView();
		}

		//Если авторизация/регистрация успешна - открываем меню
		public void CheckAuthorization(string name)
		{
			User? authorization = ContextDb.Users.FirstOrDefault(u => u.Login == Login); /*TO DO: Проверка*/
			if (authorization == null)
			{
				Massage = "Такого пользователя нет";
			}
			else
			{
				StartPage.View = new MenuNavigationView();
			}

			/*//Сохранение в БД пример:
			ContextDb.Users.Add(...);
			ContextDb.SaveChanges();*/
		}
	}
}