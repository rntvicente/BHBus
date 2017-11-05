using BHBus.Domain.Entities;
using BHBus.Domain.ValueObject;
using System;
using System.Collections.Generic;

namespace BHBus.Application.Interfaces
{
    public interface IPassengerAppService : IAppServiceBase<Passenger>
    {
        IEnumerable<Passenger> GetPessengerActive(IEnumerable<Passenger> passageiros);

        bool ExistsEmail(Email email);

        Passenger GetPassengerForEmail(string email);

        Passenger RegisterPassenger(Passenger passageiro);
    }
}
