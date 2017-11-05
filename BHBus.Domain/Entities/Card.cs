using System;
using System.Collections;
using System.Collections.Generic;

namespace BHBus.Domain.Entities
{
    [Serializable]
    public class Card
    {
        public Guid PassengerID { get; set; }

        public Guid NumberCard { get; private set; }
        
        public DateTime DateRegister { get; private set; }

        public bool Active { get; private set; }

        public virtual Passenger Passenger { get; private set; }

        protected Card() { }

        public Card(Guid passengerID, Guid numberCard)
        {
            SetPassengerID(passengerID);
            SetNumberCard(numberCard);
        }

        private void SetPassengerID(Guid passengerID)
        {
            if (passengerID == Guid.Empty)
                throw new Exception("Erro ao preencher chave PassageiroID.");

            PassengerID = passengerID;
        }

        public void SetNumberCard(Guid numberCard)
        {
            if (numberCard == Guid.Empty)
                throw new Exception("Erro ao preendher Número do Cartão");

            NumberCard = numberCard;
        }
    }
}
