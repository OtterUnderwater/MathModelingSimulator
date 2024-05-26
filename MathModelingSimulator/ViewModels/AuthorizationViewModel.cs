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
		//SetProperty ��� ���������� ������ �� ��������
		public string Massage { get => massage; set => this.SetProperty(ref massage, value); }

		public void Click(string name)
		{
			StartPage.View = new RegistrationView();
		}

		//���� �����������/����������� ������� - ��������� ����
		public void CheckAuthorization(string name)
		{
			User? authorization = ContextDb.Users.FirstOrDefault(u => u.Login == Login); /*TO DO: ��������*/
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