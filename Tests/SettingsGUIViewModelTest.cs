using Microsoft.VisualStudio.TestTools.UnitTesting;
using Szofttech_WPF.ViewModel;

namespace Tests
{
    [TestClass]
    public class SettingsGUIViewModelTest
    {
        [DataTestMethod]
        [DataRow("25564d")]
        [DataRow("hesoyam")]
        [DataRow("583221")]
        public void AllowOnlyNumerics_OnBadInputShouldRemoveLastCharacter(string parameter)
        {
            //Arrange
            PrivateObject pObject = new PrivateObject(new SettingsGUIViewModel());

            //Act
            string expected = parameter.Remove(parameter.Length - 1);
            string actual = (string)pObject.Invoke("AllowOnlyNumerics", parameter);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("0")]
        [DataRow("25564")]
        [DataRow("65535")]
        public void AllowOnlyNumerics_OnGoodInputShouldNotRemoveLastCharacter(string parameter)
        {
            //Arrange
            PrivateObject pObject = new PrivateObject(new SettingsGUIViewModel());

            //Act
            string expected = parameter;
            string actual = (string)pObject.Invoke("AllowOnlyNumerics", parameter);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
