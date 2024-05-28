using System.Collections.Generic;
using System.Linq;
using MathModelingSimulator.Models;

namespace MathModelingSimulator.ViewModels
{
	public class UserAccountViewModel : MainWindowViewModel
    {
        public UserAccountViewModel()
        {
            GetUserAccount();
        }

        string fullName = "";
		string login = "";
        string phoneNum = "";
		string email = "";

        public string FullName { get => fullName; set => this.SetProperty(ref fullName, value); }
        public string PhoneNum { get => phoneNum; set => this.SetProperty(ref phoneNum, value); }
        public string Login { get => login; set => this.SetProperty(ref login, value); }
        public string Email { get => email; set => this.SetProperty(ref email, value); }

        //Во-первых, как это сразу при запуске окна запускать. Сейчас оно при нажатии на кнопку работает только
        public void GetUserAccount()
        {
            PhoneNum = CurrentUser.Telephone;
            Login = CurrentUser.Login;
            Email = CurrentUser.Email;
            FullName = $"{CurrentUser.Surname} {CurrentUser.Name} {CurrentUser.Patronymic}";
        }

        public void SaveUserAccount()
        {
            var bufferFullName = FullName.Split(" ").ToList<string>();
            CurrentUser.Surname = bufferFullName[0];
            CurrentUser.Name = bufferFullName[1];
            CurrentUser.Patronymic = bufferFullName[2];
            CurrentUser.Email = Email;
            CurrentUser.Telephone = PhoneNum;
            CurrentUser.Login = Login;
            ContextDb.SaveChanges();
            GetUserAccount();
        }

    }
}