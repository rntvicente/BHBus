using BHBus.Application.Interfaces;
using BHBus.Domain.Entities;
using BHBus.Domain.Interfaces;
using System.Collections.Generic;
using System;
using BHBus.Domain.ValueObject;
using System.Linq;

namespace BHBus.Application
{
    public class AppServiceBase : AppServiceBase<Passenger>, IPassengerAppService
    {
        private readonly IPassengerRepository _repository;

        public AppServiceBase(IPassengerRepository repository) 
            : base(repository)
        {
            this._repository = repository;
        }

        public bool ExistsEmail(Email email)
        {
            return _repository.ExistsEmail(email);
        }

        public IEnumerable<Passenger> GetPessengerActive(IEnumerable<Passenger> passengerList)
        {
            return passengerList.Where(p => p.GetPessengerActive(p));
        }

        public Passenger GetPassengerForEmail(string email)
        {
            return null;
        }

        public Passenger RegisterPassenger(Passenger passenger)
        {
            bool isValid = ExistsEmail(passenger.Email);

            if (isValid)
                throw new Exception("E-mail já existente");

            return _repository.Add(passenger);
        }
    }
}
