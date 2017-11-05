using System;
using System.Collections.Generic;

namespace BHBus.Domain.Entities
{
    [Serializable]
    public class Balance
    {
        public const string Credit = "C";

        public const string Debit = "D";

        public Guid PassengerID { get; private set; }

        public Guid NumberCard { get; private set; }

        public Nullable<Guid> BusLineID { get; private set; }

        public DateTime DateRegister { get; private set; }

        public Double Value { get; private set; }

        public string TransactionType { get; private set; }

        public virtual Passenger Passenger { get; private set; }
        
        protected Balance()
        {

        }

        public Balance(Guid pessengerID)
        {
            SetPassengerID(pessengerID);
            DateRegister = DateTime.Now;
        }

        public void SetTransactionType(string transactionType)
        {
            if (string.IsNullOrWhiteSpace(transactionType))
                throw new Exception("Tipo de transação não pode ser vazio.");

            TransactionType = transactionType;
        }

        public void SetNumberCard(Guid numberCard)
        {
            if (numberCard == Guid.Empty)
                throw new Exception("Erro ao preencher chave Número do Cartão.");

            NumberCard = numberCard;
        }

        public void SetPassengerID(Guid passengerID)
        {
            if (passengerID == Guid.Empty)
                throw new Exception("Erro ao preencher chave PassageiroID.");

            PassengerID = passengerID;
        }

        public void SetValue(double actualValue, double value, string transactionType)
        {
            if (string.IsNullOrWhiteSpace(transactionType))
                throw new Exception("Tipo de transação deve ser informado.");

            if (value <= 0)
            {
                if (transactionType == Balance.Credit)
                    throw new Exception("Valor à creditar inválido.");
                else
                    throw new Exception("Valor à debitar inválido.");
            }

            if (transactionType == Credit)
                actualValue += Value;
            else
                actualValue -= value;

            Value = actualValue;
        }

        public void SetBusLineID(Guid busLineID)
        {
            if (busLineID == Guid.Empty)
                throw new Exception("Erro ao preencher chave PassageiroID.");

            BusLineID = busLineID;
        }
    }
}
