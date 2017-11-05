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
    public class PassengerAppServiceTest
    {
        private readonly Passenger _passenger;

        private readonly Mock<IPassengerRepository> _passengerRepository;

        public PassengerAppServiceTest()
        {
            var email = "rntvicente@gmail.com";
            _passenger = new Passenger("Renato Vicente", email, "123456");

            _passengerRepository = new Mock<IPassengerRepository>();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void PassengerAppServiceTest_AddPassenger_Passageiro_Registered()
        {
            _passengerRepository.Setup(p => p.ExistsEmail(_passenger.Email)).Returns(true);

            var service = new AppServiceBase(_passengerRepository.Object);

            service.RegisterPassenger(_passenger);

            _passengerRepository.Verify(p => p.Add(_passenger), Times.Never);
        }

        [TestMethod]
        public void PassengerAppServiceTest_AddPassenger_Passageiro_New()
        {
            _passengerRepository.Setup(p => p.ExistsEmail(_passenger.Email)).Returns(false);

            var service = new AppServiceBase(_passengerRepository.Object);

            service.RegisterPassenger(_passenger);

            _passengerRepository.Verify(p => p.Add(_passenger), Times.Once);
        }
    }
}
