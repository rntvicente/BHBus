using BHBus.Domain.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BHBus.Domain.Entities
{
    [Serializable]
    public class Passenger
    {
        public const int NameMaxLegth = 150;

        public const int PasswordMaxLength = 8;

        public Guid PassengerID { get; private set; }

        public string Name { get; private set; }

        public Email Email { get; private set; }

        public string Password { get; private set; }

        public DateTime DateRegister { get; private set; }

        public bool Active { get; private set; }

        public virtual Card Card { get; private set; }

        public virtual IEnumerable<Balance> BalanceList { get; set; }

        protected Passenger()
        {
            
        }

        public Passenger(string nome, string email, string password)
        {
            SetName(nome);
            SetEmail(email);
            SetPassword(password);

            SetPassengerID(Guid.NewGuid());
            SetCard(Guid.NewGuid());
        }

        public bool GetPessengerActive(Passenger passsenger)
        {
            return passsenger.Active;
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new Exception("Nome não pode ser vazio.");

            if (name.Length > NameMaxLegth)
                throw new Exception("Nome deve ter 150 caracteres.");

            Name = name;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("E-mail não pode ser vazio.");

            Email = new Email(email);
        }

        private void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password não pode ser vazio.");

            if (password.Length > PasswordMaxLength)
                throw new Exception("Password deve ter 8 caracteres.");

            Password = password;
        }

        private void SetCard(Guid numberCard)
        {
            if (numberCard == Guid.Empty)
                throw new Exception("Cartão não pode ser nulo.");

            Card = new Card(PassengerID, numberCard);
        }

        private void SetPassengerID(Guid passengerID)
        {
            PassengerID = passengerID;
        }
    }
}
