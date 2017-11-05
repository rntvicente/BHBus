using BHBus.Application.Interfaces;
using BHBus.Domain.Entities;
using BHBus.Domain.Interfaces;

namespace BHBus.Application
{
    public class BusLineAppService : AppServiceBase<BusLine>, IBusLineAppService
    {
        private readonly IBusLineRepository _repository;

        public BusLineAppService(IBusLineRepository repository) 
            : base (repository)
        {
            _repository = repository;
        }
    }
}
