using BHBus.Domain.Entities;
using BHBus.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace BHBus.Infra.Repositories
{
    public class BalanceRepository : RepositoryBase<Balance>, IBalanceRepository
    {
        public double GetLastBalanceForPassengerIDAndDataRegister(Guid passengerID)
        {
            var value = Db.Balances
                .OrderByDescending(d => d.DateRegister)
                .FirstOrDefault(b => b.PassengerID == passengerID)
                .Value;

            return value;
        }

        public IEnumerable<Balance> GetListDebitsForNumberCard(Guid numberCard)
        {
            var debits = Db.Balances
                .Include(i => i.Passenger)
                .AsNoTracking()
                .Where(d => d.NumberCard == numberCard && d.TransactionType == Balance.Debit);

            return debits;
        }

        public bool IsValidBusLine(string busLine)
        {
            var isValid = Db.BusLines
                .Any(b => b.Line == busLine && b.Active);

            return isValid;
        }

        public bool IsValidNumberCard(Guid numberCard)
        {
            var isValid = Db.Cards
                .Any(c => c.NumberCard == numberCard);

            return isValid;
        }

        public Guid GetPassengerID(Guid numberCard)
        {
            var passengerID = Db.Cards
                .FirstOrDefault(p => p.NumberCard == numberCard)
                .PassengerID;

            return passengerID;
        }

        public Guid GetBusLineID(string busLine)
        {
            var busLineID = Db.BusLines
                .FirstOrDefault(b => b.Line == busLine)
                .BusLineID;

            return busLineID;
        }

        public IEnumerable<Balance> GetBalanceForNumberCardAsNoTracking(Guid numberCard)
        {
            return Db.Balances
                .AsNoTracking()
                .Where(b => b.NumberCard == numberCard)
                .ToList();
        }

        public IEnumerable<Balance> GetBalanceForDateAsNoTracking(DateTime start, DateTime finish)
        {
            return Db.Balances
                .AsNoTracking()
                .Where(b => b.DateRegister >= start && b.DateRegister <= finish)
                .ToList();
        }
    }
}
