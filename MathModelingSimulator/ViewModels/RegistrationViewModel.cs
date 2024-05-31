using MathModelingSimulator.Function;
using MathModelingSimulator.Models;
using MathModelingSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Tmds.DBus.Protocol;

namespace MathModelingSimulator.ViewModels
{
    public class RegistrationViewModel : MainWindowViewModel
    {
		#region PropertyObjects
		private string surname = "";
        private string name = "";
        private string? patronymic;
        private string telephone = "";
        private string email = "";
        private string login = "";
        private string password = "";
        public string Surname { get => surname; set => surname = value; }
        public string Name { get => name; set => name = value; }
        public string? Patronymic { get => patronymic; set => patronymic = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Email { get => email; set => email = value; }
        public string Login { get => login; set => login = value; }
        public string Password { get => password; set => password = value; }

        private Role selectRole = ContextDb.Roles.First();
        public List<Role> Roles { get => ContextDb.Roles.ToList(); }
		public Role SelectRole { get => selectRole; set => selectRole = value; }

        #region
        private string messageName = "";
        private string messageSurname = "";
        private string messagePatronymic = "";
        private string messageTelephone = "";
        private string messageEmail = "";
        private string messageLogin = "";
        private string messagePassword = "";
        public string MessageName { get => messageName; set => this.SetProperty(ref messageName, value); }
        public string MessageSurname { get => messageSurname; set => this.SetProperty(ref messageSurname, value); }
        public string MessagePatronymic { get => messagePatronymic; set => this.SetProperty(ref messagePatronymic, value); }
        public string MessageTelephone { get => messageTelephone; set => this.SetProperty(ref messageTelephone, value); }
        public string MessageEmail { get => messageEmail; set => this.SetProperty(ref messageEmail, value); }
        public string MessageLogin { get => messageLogin; set => this.SetProperty(ref messageLogin, value); }
        public string MessagePassword { get => messagePassword; set => this.SetProperty(ref messagePassword, value); }
        
        private bool isVisibleMessageName = false;
        private bool isVisibleMessageSurname = false;
        private bool isVisibleMessagePatronymic = false;
        private bool isVisibleMessageTelephone = false;
        private bool isVisibleMessageEmail = false;
        private bool isVisibleMessageLogin = false;
        private bool isVisibleMessagePassword = false;
        public bool IsVisibleMessageName { get => isVisibleMessageName; set => SetProperty(ref isVisibleMessageName, value); }
        public bool IsVisibleMessageSurname { get => isVisibleMessageSurname; set => this.SetProperty(ref isVisibleMessageSurname, value); }
        public bool IsVisibleMessagePatronymic { get => isVisibleMessagePatronymic; set => this.SetProperty(ref isVisibleMessagePatronymic, value); }
        public bool IsVisibleMessageTelephone { get => isVisibleMessageTelephone; set => this.SetProperty(ref isVisibleMessageTelephone, value); }
        public bool IsVisibleMessageEmail { get => isVisibleMessageEmail; set => this.SetProperty(ref isVisibleMessageEmail, value); }
        public bool IsVisibleMessageLogin { get => isVisibleMessageLogin; set => this.SetProperty(ref isVisibleMessageLogin, value); }
        public bool IsVisibleMessagePassword { get => isVisibleMessagePassword; set => this.SetProperty(ref isVisibleMessagePassword, value); }


        #endregion
        #endregion

        public void GoToAuthorization()
		{
			StartPage.View = new AuthorizationView();
		}

		public void CheckRegistration()
		{
            //IsTrueData())
            if (IsTrueData())
            {
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
				user.IdRole = SelectRole.IdRole;
				ContextDb.Users.Add(user);
				ContextDb.SaveChanges();
                ClearRegistration();
                StartPage.View = new AuthorizationView();
			}
		}
        private bool IsTrueData()
        {
            Regular regular = new Regular();
            (bool isTrueField, string message) result;
            if (
                regular.GetRegularSurname(Surname).isTrueField &&
                regular.GetRegularName(Name).isTrueField &&
                regular.GetRegularTelephone(Telephone).isTrueField &&
                regular.GetRegularEmail(Email).isTrueField &&
                regular.GetRegularLogin(login, ContextDb).isTrueField &&
                regular.GetRegularPassword(Password).isTrueField
            )
            {
                return true;
            }
            else {
                result = regular.GetRegularSurname(Surname);
                IsVisibleMessageSurname = !result.isTrueField;
                MessageSurname = result.message;

                result = regular.GetRegularName(Name);
                IsVisibleMessageName = !result.isTrueField;
                MessageName = result.message;

                result = regular.GetRegularTelephone(Telephone);
                IsVisibleMessageTelephone = !result.isTrueField;
                MessageTelephone = result.message;

                result = regular.GetRegularEmail(Email);
                IsVisibleMessageEmail = !result.isTrueField;
                MessageEmail = result.message;

                result = regular.GetRegularLogin(Login, ContextDb);
                IsVisibleMessageLogin = !result.isTrueField;
                MessageLogin = result.message;

                result = regular.GetRegularPassword(Password);
                IsVisibleMessagePassword = !result.isTrueField;
                MessagePassword = result.message;

                return false;
            }
        }
    }
}
