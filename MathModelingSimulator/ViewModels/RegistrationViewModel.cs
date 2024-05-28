using MathModelingSimulator.Function;
using MathModelingSimulator.Models;
using MathModelingSimulator.Views;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Tmds.DBus.Protocol;

namespace MathModelingSimulator.ViewModels
{
    public class RegistrationViewModel : MainWindowViewModel
    {
        private string surname = "";
        public string Surname { get => surname; set => surname = value; }

        private string name = "";
        public string Name { get => name; set => name = value; }

        private string patronymic = "";
        public string Patronymic { get => patronymic; set => patronymic = value; }

        private string telephone = "";
        public string Telephone { get => telephone; set => telephone = value; }

        private string email = "";
        public string Email { get => email; set => email = value; }

        private string login = "";
        public string Login { get => login; set => login = value; }
       
        private string password = "";
        public string Password { get => password; set => password = value; }

        private string message = "";
        public string Message { get => message; set => this.SetProperty(ref message, value); }

		public void GoToAuthorization()
        {
			StartPage.View = new AuthorizationView();
		}

		public void CheckRegistration()
		{
			//*TO DO:* if (IsTrueData())
			Security security = new Security();
			User user = new User();
			user.Id = Guid.NewGuid();
			user.Surname = Surname;
			user.Name = Name;
			user.Patronymic = Patronymic;
			user.Email = Email;
			user.Login = Login;
			user.Telephone = Telephone;
			user.Password = security.GetHashPassword(Password);
			user.IdRole = 1;
			ContextDb.Users.Add(user);
			ContextDb.SaveChanges();
			StartPage.View = new AuthorizationView();
		}

		private bool IsTrueData() {
            Regex FI = new Regex("^([А-Я]|[а-я])+$");
            Regex mail = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            Regex password = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            if (FI.IsMatch(Surname) && FI.IsMatch(Name))
            {
                if (mail.IsMatch(Email))
                {
                    User? nullLogin = ContextDb.Users.FirstOrDefault(u => u.Login == Login); /*TO DO: Проверка*/
                    if (nullLogin == null)
                    {
                        if (password.IsMatch(Password))
                        {
                            return true;
                        }
                        else {
							Message = "Некорректный пароль";
                            return false;
                        }
                    }
                    else
                    {
						Message = "Пользователь с данным ником существует";
                        return false;
                    }
                }
                else
                {
					Message = "Некорректный почтовый адрес.";
                    return false;
                }
            }
            else {
				Message = "Некорректный ввод фамилии и имени";
                return false;
            }
        }
    }
}
