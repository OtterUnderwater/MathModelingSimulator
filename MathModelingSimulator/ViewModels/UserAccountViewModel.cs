using System;
using System.Collections.Generic;
using System.Linq;
using MathModelingSimulator.Models;
using ReactiveUI;
using Tmds.DBus.Protocol;

namespace MathModelingSimulator.ViewModels
{
	public class UserAccountViewModel : ViewModelBase
	{
		string fullName;
		string login;
		string phoneNum;
		string email;

        public string FullName { get => fullName; set => this.SetProperty(ref fullName, value); }
        public string PhoneNum { get => phoneNum; set => this.SetProperty(ref phoneNum, value); }
        public string Login { get => login; set => this.SetProperty(ref login, value); }
        public string Email { get => email; set => this.SetProperty(ref email, value); }

        //��-������, ��� ��� ����� ��� ������� ���� ���������. ������ ��� ��� ������� �� ������ �������� ������
        public void GetUserAccount()
        {
            //�� ������ ��� ��� ��������� � ������ � VM ����������� � ����� �������� �����
            //User user = ContextDb.Users.First(it => it.Id == UserId);
            User user = ContextDb.Users.First(it => it.Id == new Guid("5682cc19-09c5-41b1-8dc9-a03792bb6d9a"));
            PhoneNum = user.Telephone;
            Login = user.Login;
            Email = user.Email;
            FullName = $"{user.Name} {user.Surname} {user.Patronymic}";
        }

        public void SaveUserAccount()
        {

        }

    }
}