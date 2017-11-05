using BHBus.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BHBus.Domain.Test.Entities
{
    [TestClass]
    public class BalanceTest
    {
        private Guid PassengerID { get; set; }
        
        private Double Value { get; set; }

        private string TransactionType { get; set; }

        private Guid BusLineID { get; set; }

        public BalanceTest()
        {
            PassengerID = Guid.NewGuid();
            BusLineID = Guid.NewGuid();
            Value = 100;
            TransactionType = Balance.Credit;
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceTest_New_PassageiroID_Required()
        {
            new Balance(Guid.Empty);
        }

        [TestMethod]
        public void BalanceTest_New_PassageiroID()
        {
            var balancce = new Balance(PassengerID);

            Assert.AreEqual(PassengerID, balancce.PassengerID);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceTest_SetBusLineID_Required()
        {
            var balance = new Balance(PassengerID);

            balance.SetBusLineID(Guid.Empty);
        }

        [TestMethod]
        public void BalanceTest_SetBusLineID_New()
        {
            var balance = new Balance(PassengerID);

            balance.SetBusLineID(BusLineID);

            Assert.AreEqual(BusLineID, balance.BusLineID);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceTest_SetValue_Value_Required()
        {
            var balance = new Balance(PassengerID);

            balance.SetValue(10, 0, Balance.Debit);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceTest_SetValue_TransactionType_Required()
        {
            var balance = new Balance(PassengerID);

            balance.SetValue(10, BusLine.PricePassaage, string.Empty);
        }

        [TestMethod]
        public void BalanceTest_SetBalance_New()
        {
            var balance = new Balance(PassengerID);

            balance.SetValue(10, 38.0, Balance.Credit);

            Assert.IsNotNull(balance.Value);
        }
    }
}
