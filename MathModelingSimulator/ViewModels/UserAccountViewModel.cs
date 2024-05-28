using System;
using System.Collections.Generic;
using MathModelingSimulator.Models;
using ReactiveUI;

namespace MathModelingSimulator.ViewModels
{
	public class UserAccountViewModel : ViewModelBase
	{
		string fullName;
		string login;
		string phoneNum;
		string email;

        /*//Сохраняем в памяти айдишник пользователя, который вошел. (на VMBase)
        User? user = ContextDb.Users.FirstOrDefault();*/

        public string FullName { get => fullName; set => fullName = value; }
        public string Login { get => login; set => login = value; }
        public string PhoneNum { get => phoneNum; set => phoneNum = value; }
        public string Email { get => email; set => email = value; }

        public void SaveUserAccount()
        {

        }

        /* user.Id = Guid.NewGuid();
             user.Surname = Surname;
             user.Name = Name;
             user.Patronymic = Patronymic;
             user.Email = Email;
             user.Login = Login;
             user.Password = security.GetHashPassword(Password);
             user.IdRole = 1;
 */
    }
}