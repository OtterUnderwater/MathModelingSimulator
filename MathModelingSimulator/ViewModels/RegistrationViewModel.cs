using MathModelingSimulator.Models;
using MathModelingSimulator.Views;
using MsBox.Avalonia.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace MathModelingSimulator.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        private string surname = "";
        public string Surname { get => surname; set => surname = value; }
        private string name = "";
        public string Name { get => name; set => name = value; }
        private string patronymic = "";
        string tmpTelephone;
        public string Patronymic { 
            get => patronymic; 
        }
        private string telephone = "";
        public string Telephone { get => telephone; set => telephone = value; }
        private string email = "";
        public string Email { get => email; set => email = value; }
        private string login = "";
        public string Login { get => login; set => login = value; }
        private string password = "";
        public string Password { get => password; set => password = value; }

        private string massage = "";
        public string Massage { get => massage; set => this.SetProperty(ref massage, value); }

		public void GoToAuthorization()
        {
			StartPage.View = new AuthorizationView();
		}

		public void CheckRegistration()
        {
            if (isTrueData())
            {
                int n = 0;
                /*типа добавляем данные в БД*/
                StartPage.View = new AuthorizationView();
            }
            
            /*//Сохранение в БД пример:
			ContextDb.Users.Add(...);
			ContextDb.SaveChanges();*/
        }
        bool isTrueData() {
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
                            Massage = "Некорректный пароль";
                            return false;
                        }
                    }
                    else
                    {
                        Massage = "Пользователь с данным ником существует";
                        return false;
                    }
                }
                else
                {
                    Massage = "Некорректный почтовый адрес.";
                    return false;
                }
            }
            else {
                Massage = "Некорректный ввод фамилии и имени";
                return false;
            }
        }
    }
}
