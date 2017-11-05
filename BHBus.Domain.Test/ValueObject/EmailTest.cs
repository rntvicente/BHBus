using BHBus.Domain.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BHBus.Domain.Test.ValueObject
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmailTest_New_Email_IsEmpty()
        {
            var email = new Email("");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmailTest_New_Email_IsNull()
        {
            var email = new Email(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmailTest_New_Email_IsSpace()
        {
            var email = new Email(" ");
        }

        [TestMethod]
        public void EmailTest_New_Email_IsValid()
        {
            var address = "rntvicente@gmail.com";
            var email = new Email(address);

            Assert.AreEqual(address, email.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmailTest_New_Email_Invalid()
        {
            var address = "rntvicente.gmail.com";
            var email = new Email(address);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EmailTest_New_Email_MaxLength()
        {
            var address = "rntvicente@gmail.com";

            for (int i = address.Length, j = Email.AddressMaxLength+1; i < j; i++)
            {
                address = address + "@gmail.com";
            }

            new Email(address);
        }
    }
}
