using System.Collections.Generic;
using System.Linq;
using MathModelingSimulator.Function;
using System.Xml.Linq;
using MathModelingSimulator.Models;
using MathModelingSimulator.Views;

namespace MathModelingSimulator.ViewModels
{
	public class UserAccountViewModel : MainWindowViewModel
    {
        #region PropertyObjects
        string fullName = "";
		string login = "";
        string phoneNum = "";
		string email = "";
        public string FullName { get => fullName; set => this.SetProperty(ref fullName, value); }
        public string PhoneNum { get => phoneNum; set => this.SetProperty(ref phoneNum, value); }
        public string Login { get => login; set => this.SetProperty(ref login, value); }
        public string Email { get => email; set => this.SetProperty(ref email, value); }

        
        private string messageSurnameName = "";
        private string messageTelephone = "";
        private string messageEmail = "";
        public string MessageSurnameName { get => messageSurnameName; set => this.SetProperty(ref messageSurnameName, value); }
        public string MessageTelephone { get => messageTelephone; set => this.SetProperty(ref messageTelephone, value); }
        public string MessageEmail { get => messageEmail; set => this.SetProperty(ref messageEmail, value); }

        private bool isVisibleMessageSurnameName = false;
        private bool isVisibleMessageTelephone = false;
        private bool isVisibleMessageEmail = false;
        public bool IsVisibleMessageSurnameName { get => isVisibleMessageSurnameName; set => this.SetProperty(ref isVisibleMessageSurnameName, value); }
        public bool IsVisibleMessageTelephone { get => isVisibleMessageTelephone; set => this.SetProperty(ref isVisibleMessageTelephone, value); }
        public bool IsVisibleMessageEmail { get => isVisibleMessageEmail; set => this.SetProperty(ref isVisibleMessageEmail, value); }
        
        #endregion

        public UserAccountViewModel()
        {
            GetUserAccount();
        }
        public void GetUserAccount()
        {
            PhoneNum = CurrentUser.Telephone;
            Login = CurrentUser.Login;
            Email = CurrentUser.Email;
            FullName = $"{CurrentUser.Surname} {CurrentUser.Name} {CurrentUser.Patronymic}";
        }

        public void SaveUserAccount()
        {
            if (IsTrueData())
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

        public void LogOut()
        {
            ClearAuth();
            StartPage.View = new AuthorizationView();
        }
        private bool IsTrueData()
        {
            Regular regular = new Regular();
            (bool isTrueField, string message) result;
            var bufferFullName = FullName.Split(" ").ToList<string>();
            if (
            regular.GetRegularSurnameName(bufferFullName[1],bufferFullName[0]).isTrueField &&
                regular.GetRegularTelephone(PhoneNum).isTrueField &&
                regular.GetRegularEmail(Email).isTrueField
            )
            {
                return true;
            }
            else
            {
                result = regular.GetRegularSurnameName(bufferFullName[1], bufferFullName[0]);
                IsVisibleMessageSurnameName = !result.isTrueField;
                MessageSurnameName = result.message;

                result = regular.GetRegularTelephone(PhoneNum);
                IsVisibleMessageTelephone = !result.isTrueField;
                MessageTelephone = result.message;

                result = regular.GetRegularEmail(Email);
                IsVisibleMessageEmail = !result.isTrueField;
                MessageEmail = result.message;

                return false;
            }
        }
    }
}