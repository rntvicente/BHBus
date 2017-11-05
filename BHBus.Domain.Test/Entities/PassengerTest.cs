using BHBus.Domain.Entities;
using BHBus.Domain.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BHBus.Domain.Test.Entities
{
    [TestClass]
    public class PassengerTest
    {
        private string Name { get; set; }

        private string Email { get; set; }

        private string Password { get; set; }

        private double Value { get; set; }

        public PassengerTest()
        {
            Name = "Renato Vicente";
            Email = "rntvicente@gmail.com";
            Password = "123456";
            Value = 100;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PassageiroTest_Name_Required()
        {
            new Passenger("",Email,Password);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PassageiroTest_Email_Required()
        {
            new Passenger(Name, null, Password);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PassageiroTest_Password_Required()
        {
            new Passenger(Name, Email, "");
        }

        [TestMethod]
        public void PassageiroTest_New_Name()
        {
            var passenger = new Passenger(Name, Email, Password);

            Assert.AreEqual(Name, passenger.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PassageiroTest_New_NameMaxLength()
        {
            for (int i = 0, j = Passenger.NameMaxLegth + 1; i < j; i++)
            {
                Name = Name + " Renato Vicente";
            }

            new Passenger(Name, Email, Password);
        }

        [TestMethod]
        public void PassageiroTest_New_Email()
        {
            var passenger = new Passenger(Name, Email, Password);

            Assert.AreEqual(Email, passenger.Email.Address);
        }

        [TestMethod]
        public void PassageiroTest_New_Password()
        {
            var passenger = new Passenger(Name, Email, Password);

            Assert.AreEqual(Password, passenger.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PassageiroTest_New_PasswordMaxLength()
        {
            for (int i = 0, j = Passenger.PasswordMaxLength + 1; i < j; i++)
            {
                Password = Password + "1234567890";
            }

            new Passenger(Name, Email, Password);
        }
    }
}
