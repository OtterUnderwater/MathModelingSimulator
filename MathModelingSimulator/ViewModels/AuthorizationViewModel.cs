using System.Linq;
using MathModelingSimulator.Views;
using MathModelingSimulator.Models;
using MathModelingSimulator.Function;

namespace MathModelingSimulator.ViewModels
{

	public class AuthorizationViewModel : MainWindowViewModel
	{
		#region PropertyObjects
		private string login = "";
		public string Login { get => login; set => login = value; }

        private string password = "";
		public string Password { get => password; set => password = value; }

        private string message = "";
		public string Message { get => message; set => this.SetProperty(ref message, value); }
		#endregion

		public void GoToRegistration()
		{
			StartPage.View = new RegistrationView();
		}

		//���� �����������/����������� ������� - ��������� ����
		public void CheckAuthorization()
		{
			Security security = new Security();
			string passwordHash = security.GetHashPassword(password);
			User? authorization = ContextDb.Users.FirstOrDefault(u => u.Login == Login && u.Password == passwordHash);
			if (authorization == null)
			{
				Message = "��� ������������ ��� ������ �� ����������.";
			}
			else
			{
                CurrentUser = authorization;            
                MenuVM = new MenuNavigationViewModel();
				StartPage.View = new MenuNavigationView();           
                PageSwitch.View = new SimulatorsView();
            }
		}
	}
}