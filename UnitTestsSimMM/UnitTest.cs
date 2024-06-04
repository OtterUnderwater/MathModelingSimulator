using  MathModelingSimulator.Function;

namespace UnitTestsSimMM
{
	[TestClass]
	public class UnitTest
	{
		[TestMethod]
		public void GetHashPassword_Password11_Hashreturned()
		{
			string password = "11";
			string expected = "BrbeSdYMT//FOJwkU1AXSjKfOwkhepBWTx+P/IgBqRA=";

			Security obj = new Security();
			string result = obj.GetHashPassword(password);

			Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result);
		}


	}
}