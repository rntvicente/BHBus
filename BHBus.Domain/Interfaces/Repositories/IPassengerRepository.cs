using BHBus.Domain.Entities;
using BHBus.Domain.ValueObject;

namespace BHBus.Domain.Interfaces
{
    public interface IPassengerRepository : IRepositoryBase<Passenger>
    {
        bool ExistsEmail(Email email);

        Passenger GetPassengerForEmail(string email);
    }
}
