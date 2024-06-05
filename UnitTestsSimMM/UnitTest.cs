using  MathModelingSimulator.Function;
using System.Xml.Linq;

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


        [TestMethod]
        public void GetMinSimplexMetod_Matrix_Expected15()
        {
            int[,] matrix = { { 0, 1, 2, 3, 4, 5, 6, 7 }, { 4, 2, -1, 1, 1, 0, 0, 1 }, { 5, -4, 2, -1, 0, 1, 0, 2 }, { 6, 3, 0, 1, 0, 0, 1, 5 }, { 0, -1, 1, 3, 0, 0, 0, 0 } };
            int expected = -15;

            SimplexMetod obj = new SimplexMetod();
            int result = obj.MainSimplexMetod(matrix, false);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetTravelingSalesmanProblem_Matrix_Expected41()
        {
            int[,] matrix = { { 0, 20,18,12,8}, {5,0,14,7,11},{12,18,0,6,11},{11,17,11,0,12},{5,5,5,5,0}};
            int expected = 41;

            TravelingSalesmanProblem obj = new TravelingSalesmanProblem();
            int result = obj.MainTravelingSalesmanProblem(matrix);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetMaxSimplexMetod_Matrix_Expected11()
        {
            int[,] matrix = { { 0,1,2,3,4,5,6 }, { 3,1,2,1,0,0,4 }, { 4,1,1,0,1,0,3 }, {5,2,1,0,0,1,8 }, {0,-3,-4,0,0,0,0} };
            int expected = 11;

            SimplexMetod obj = new SimplexMetod();
            int result = obj.MainSimplexMetod(matrix, true);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetRegularTelephone_Telephone_ExpectedTrue()
        {
            string Telephone = "+7 985 678 23 43";
            bool expected = true;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularTelephone(Telephone);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularTelephone_Telephone_ExpectedFalse()
        {
            string Telephone = "999 999 99 99";
            bool expected = false;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularTelephone(Telephone);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularPassword_Password123_ExpectedFalse()
        {
            string Password = "123";
            bool expected = false;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularPassword(Password);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularPassword_PasswordAbcd1234_ExpectedTrue()
        {
            string Password = "Abcd1234";
            bool expected = true;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularPassword(Password);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularSurname_Surname_ExpectedTrue()
        {
            string Surname = "Абрамова";
            bool expected = true;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularSurname(Surname);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularSurname_Surname_ExpectedFalse()
        {
            string Surname = "Abramova";
            bool expected = false;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularSurname(Surname);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularName_Name_ExpectedTrue()
        {
            string Name = "Евгения";
            bool expected = true;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularName(Name);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularName_Surname_ExpectedFalse()
        {
            string Name = "Evgenia";
            bool expected = false;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularName(Name);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularEmail_Email_ExpectedTrue()
        {
            string Email = "abcd@mail.ru";
            bool expected = true;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularEmail(Email);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
        [TestMethod]
        public void GetRegularEmail_Email_ExpectedFalse()
        {
            string Email = "1@mail";
            bool expected = false;

            Regular obj = new Regular();
            (bool isTrueField, string message) result = obj.GetRegularEmail(Email);

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(expected, result.isTrueField);
        }
    }
}