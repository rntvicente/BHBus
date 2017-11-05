using BHBus.Domain.Entities;
using System;
using System.Collections.Generic;

namespace BHBus.Application.Interfaces
{
    public interface IBalanceAppService : IAppServiceBase<Balance>
    {
        Balance Credits(Guid numberCard, double value);

        Balance Debits(Guid numberCard, string busLine);

        IEnumerable<Balance> GetBalanceForNumberCardAsNoTracking(Guid numberCard);

        IEnumerable<Balance> GetBalanceForDateAsNoTracking(DateTime start, DateTime finish);
    }
}
