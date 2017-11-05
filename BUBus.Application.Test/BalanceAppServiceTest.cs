using BHBus.Application;
using BHBus.Domain.Entities;
using BHBus.Domain.Interfaces;
using BHBus.Domain.ValueObject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace BUBus.Application.Test
{
    [TestClass]
    public class BalanceAppServiceTest
    {
        private Passenger _passenger;

        private BusLine _busLine;

        private Balance _balance;

        private Guid NumberCard;

        private Guid PassengerID;

        private Guid BusLineID;

        private Mock<IBalanceRepository> _balanceRepository;

        public BalanceAppServiceTest()
        {
            _balanceRepository = new Mock<IBalanceRepository>();

            _passenger = new Passenger("Renato Vicente", "rntvicente@gmail.com", "123456");

            NumberCard = Guid.Empty;
            Guid.TryParse("CA8F6917-4A37-4CB5-A671-1CBC86F72324", out NumberCard);

            PassengerID = Guid.Empty;
            Guid.TryParse("11612030-F093-48E4-9EE8-D5563259DD69", out PassengerID);

            BusLineID = Guid.Empty;
            Guid.TryParse("5ACBA9DA-705F-4CEA-B4BB-1DD84FC5794B", out BusLineID);

            _busLine = new BusLine("917-M");

            _balance = new Balance(PassengerID);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_BalanceGreaterThanPassage_Required()
        {
            Balance balance = null;

            var service = new BalanceAppService(_balanceRepository.Object);
            service.BalanceGreaterThanPassage(balance);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_BalanceGreaterThanPassage_InsufficientValue()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            Balance balance = service.CreateBalance(PassengerID, BusLineID, NumberCard, 0, Balance.Debit);
            service.BalanceGreaterThanPassage(balance);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_IsValidNumberCard_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);
            service.IsValidNumberCard(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_IsValidNumberCard_NotExissts()
        {
            var service = new BalanceAppService(_balanceRepository.Object);
            service.IsValidNumberCard(Guid.NewGuid());
        }

        [TestMethod]
        public void BalanceAppServiceTest_IsValidNumberCard_IsValid()
        {
            _balanceRepository.Setup(b => b.IsValidNumberCard(NumberCard)).Returns(true);

            var service = new BalanceAppService(_balanceRepository.Object);

            service.IsValidNumberCard(NumberCard);

            _balanceRepository.Verify(b => b.IsValidNumberCard(NumberCard), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_IsValidBusLine_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);
            service.IsValidBusLine("");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_IsValidBusLine_NotExists()
        {
            var service = new BalanceAppService(_balanceRepository.Object);
            service.IsValidBusLine("9191");
        }

        [TestMethod]
        public void BalanceAppServiceTest_IsValidBusLine_IsValid()
        {
            _balanceRepository.Setup(b => b.IsValidBusLine(_busLine.Line)).Returns(true);

            var service = new BalanceAppService(_balanceRepository.Object);

            service.IsValidBusLine(_busLine.Line);

            _balanceRepository.Verify(b => b.IsValidBusLine(_busLine.Line), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetPassengerIDForNumberCard_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetPassengerID(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetPassengerIDForNumberCard_NotExists()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetPassengerID(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetBusLineIDForNumberCard_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetBusLineID("");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetBusLineIDForNumberCard_NotExists()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetBusLineID("XXXX");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetLastBalanceForPassengerIDAndDataRegister_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetLastBalance(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_Debits_NumberCard_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.Debits(Guid.Empty, _busLine.Line);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_Debits_BusLine_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.Debits(NumberCard, string.Empty);
        }

        [TestMethod]
        public void BalanceAppServiceTest_Debits_New()
        {
            double value = 10;

            _balanceRepository.Setup(c => c.IsValidNumberCard(NumberCard)).Returns(true);
            _balanceRepository.Setup(l => l.IsValidBusLine(_busLine.Line)).Returns(true);
            _balanceRepository.Setup(p => p.GetPassengerID(NumberCard)).Returns(PassengerID);
            _balanceRepository.Setup(b => b.GetBusLineID(_busLine.Line)).Returns(BusLineID);
            _balanceRepository.Setup(s => s.GetLastBalanceForPassengerIDAndDataRegister(PassengerID)).Returns(value);

            var balance = new Balance(PassengerID);
            balance.SetBusLineID(BusLineID);
            balance.SetNumberCard(NumberCard);
            balance.SetValue(value, BusLine.PricePassaage, Balance.Debit);

            var service = new BalanceAppService(_balanceRepository.Object);

            service.Add(balance);

            _balanceRepository.Verify(p => p.Add(balance), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_Credits_NumberCard_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.Credits(Guid.Empty, 10.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_Credits_NumberCard_Exists()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.Credits(NumberCard, 10.0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_Credits_Value_NotZero()
        {
            _balanceRepository.Setup(p => p.IsValidNumberCard(NumberCard)).Returns(true);

            var service = new BalanceAppService(_balanceRepository.Object);

            service.Credits(NumberCard, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_Credits_Value_ValueMinimun()
        {
            _balanceRepository.Setup(p => p.IsValidNumberCard(NumberCard)).Returns(true);

            var service = new BalanceAppService(_balanceRepository.Object);

            service.Credits(NumberCard, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetBalance_NumberCard_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetBalanceForNumberCardAsNoTracking(Guid.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetBalance_NumberCard_Exists()
        {
            _balanceRepository.Setup(p => p.IsValidNumberCard(Guid.NewGuid())).Returns(false);

            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetBalanceForNumberCardAsNoTracking(Guid.NewGuid());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetBalance_StartDate_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetBalanceForDateAsNoTracking(DateTime.MinValue, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetBalance_StartDate_SuperiorNow()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetBalanceForDateAsNoTracking(DateTime.Now.AddDays(1), DateTime.Now.AddDays(20));
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetBalance_StartDate_SuperiorFinishidate()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetBalanceForDateAsNoTracking(DateTime.Now.AddDays(10), DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BalanceAppServiceTest_GetBalance_FinishDate_Required()
        {
            var service = new BalanceAppService(_balanceRepository.Object);

            service.GetBalanceForDateAsNoTracking(DateTime.Now, DateTime.MinValue);
        }
    }
}
