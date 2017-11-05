using BHBus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BHBus.Domain.Interfaces
{
    public interface IBalanceRepository : IRepositoryBase<Balance>
    {
        IEnumerable<Balance> GetListDebitsForNumberCard(Guid numberCard);

        double GetLastBalanceForPassengerIDAndDataRegister(Guid passengerID);

        bool IsValidBusLine(string busLine);

        bool IsValidNumberCard(Guid numberCard);

        Guid GetPassengerID(Guid numberCard);

        Guid GetBusLineID(string busLine);

        IEnumerable<Balance> GetBalanceForNumberCardAsNoTracking(Guid numberCard);

        IEnumerable<Balance> GetBalanceForDateAsNoTracking(DateTime start, DateTime finish);
    }
}
