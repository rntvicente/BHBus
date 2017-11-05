using BHBus.Domain.Entities;
using BHBus.Domain.Interfaces;
using BHBus.Domain.ValueObject;
using System;
using System.Linq;

namespace BHBus.Infra.Repositories
{
    public class PassengerRepository : RepositoryBase<Passenger>, IPassengerRepository
    {
        public bool ExistsEmail(Email email)
        {
            return Db.Passengeres.Any(p => p.Email.Address == email.Address);
        }

        public Passenger GetPassengerForEmail(string email)
        {
            return Db.Passengeres.FirstOrDefault(p => p.Email.Address == email);
        }
    }
}
