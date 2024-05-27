using System.Linq;
using MathModelingSimulator.Views;
using MathModelingSimulator.Models;

namespace MathModelingSimulator.ViewModels
{

	public class AuthorizationViewModel : ViewModelBase
	{
		private string login = "";
		public string Login { get => login; set => login = value; }

        private string password = "";
		public string Password { get => password; set => password = value; }

        private string massage = "";
		public string Massage { get => massage; set => this.SetProperty(ref massage, value); }

		public void GoToRegistration(string name)
		{
			StartPage.View = new RegistrationView();
		}

		//���� �����������/����������� ������� - ��������� ����
		public void CheckAuthorization(string name)
		{
			User? authorization = ContextDb.Users.FirstOrDefault(u => u.Login == Login && u.Password == Password); /*TO DO: ��������*/
			if (authorization == null)
			{
				Massage = "������ ������������ ���";
			}
			else
			{
				StartPage.View = new MenuNavigationView();
			}

			/*//���������� � �� ������:
			ContextDb.Users.Add(...);
			ContextDb.SaveChanges();*/
		}
	}
}