using MathModelingSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tmds.DBus.Protocol;

namespace MathModelingSimulator.Function
{
    public class Regular
    {
        #region regular expressions
            static Regex password = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
            static Regex FI = new Regex("^([А-Я]|[а-я])+$");
            static Regex mail = new Regex(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
            static Regex telephone = new Regex(@"^\+7|\b8\d{10}\b");
        #endregion
        public (bool isTrueField, string message) GetRegularTelephone(string Telephone)
        {
            if (telephone.IsMatch(Telephone))
            {
                return (true, "");
            }
            else
            {
                return (false, "Телефон должен соотвествовать маске +7ХХХХХХХХХХ ИЛИ 8ХХХХХХХХХХ");
            }
        }
        public (bool isTrueField, string message) GetRegularPassword(string Password) {
            if (password.IsMatch(Password))
            {
                return (true, "");
            }
            else
            {
                return (false, "Пароль должен содержать не менее 8 символов, 1 букву a-z и 1 букву A-Z");
            }
        }
        public (bool isTrueField, string message) GetRegularSurname(string Surname)
        {
            if (FI.IsMatch(Surname))
            {
                return (true, "");
            }
            else
            {
                return (false, "Фамилия должна быть написана символами русского алфавита");
               
            }
        }
        public (bool isTrueField, string message) GetRegularSurnameName(string Name,string Surname)
        {
            
            if (FI.IsMatch(Name) && FI.IsMatch(Surname))
            {
                return (true, "");
            }
            else
            {
                return (false, "Имя и фамилия должна быть написана символами русского алфавита");
            }
        }
        public (bool isTrueField, string message) GetRegularName(string Name)
        {
            if (FI.IsMatch(Name))
            {
                return (true, "");
            }
            else
            {
                return (false, "Имя должно быть написано символами русского алфавита");
            }
        }
        public (bool isTrueField, string message) GetRegularEmail(string Email)
        {
            if (mail.IsMatch(Email))
            {
                return (true, "");
            }
            else
            {
                return (false, "Некорректный почтовый адрес.");
            }
        }
        public (bool isTrueField, string message) GetRegularLogin(string Login, Trio33pContext Context)
        {
            User? nullLogin = Context.Users.FirstOrDefault(u => u.Login == Login);
            if (nullLogin == null)
            {
                return (true, "");
            }
            else {
                return (false, "Пользователь с данным ником уже существует.");
               
            }
        }
    }
}
