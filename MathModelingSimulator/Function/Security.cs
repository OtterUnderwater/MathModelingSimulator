using System;
using System.Security.Cryptography;
using System.Text;
using MathModelingSimulator.ViewModels;

namespace MathModelingSimulator.Function
{
	public class Security : ViewModelBase
	{
		public string GetHashPassword(string password)
		{
			byte[] data = Encoding.UTF8.GetBytes(password);
			HMACSHA256 sha = new HMACSHA256(KeyDb);
			byte[] result = sha.ComputeHash(data);
			return Convert.ToBase64String(result);
		}
	}
}
