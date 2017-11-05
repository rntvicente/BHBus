using BHBus.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BHBus.Domain.Test.Entities
{
    [TestClass]
    public class CardTest
    {
        private Guid PassengerID { get; set; }

        private Guid NumberCard { get; set; }

        public CardTest()
        {
            PassengerID = Guid.NewGuid();
            NumberCard = Guid.NewGuid();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CardTest_New_PassengerID_Required()
        {
            new Card(Guid.Empty, Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CardTest_New_NumberCard_Required()
        {
            new Card(Guid.NewGuid(), Guid.Empty);
        }

        [TestMethod]
        public void CardTest_New_PassengerID()
        {
            var card = new Card(PassengerID, NumberCard);
            Assert.AreEqual(PassengerID, card.PassengerID);
        }

        [TestMethod]
        public void CardTest_New_NumberCard()
        {
            var card = new Card(PassengerID, NumberCard);
            Assert.AreEqual(NumberCard, card.NumberCard);
        }
    }
}
