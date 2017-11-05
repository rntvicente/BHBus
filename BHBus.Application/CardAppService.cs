using System;
using System.Collections.Generic;
using BHBus.Application.Interfaces;
using BHBus.Domain.Entities;
using BHBus.Domain.Interfaces;

namespace BHBus.Application
{
    public class CardAppService : AppServiceBase<Card>, ICardAppService
    {
        private ICardRepository _repository;

        public CardAppService(ICardRepository repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
