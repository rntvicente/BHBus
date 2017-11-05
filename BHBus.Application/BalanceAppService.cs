using BHBus.Application.Interfaces;
using BHBus.Domain.Entities;
using BHBus.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace BHBus.Application
{
    public class BalanceAppService : AppServiceBase<Balance>, IBalanceAppService
    {
        private IBalanceRepository _BalanceRepository;

        public BalanceAppService(IBalanceRepository repository)
            : base(repository)
        {
            _BalanceRepository = repository;
        }

        public Balance Debits(Guid numberCard, string busLine)
        {
            IsValidNumberCard(numberCard);

            IsValidBusLine(busLine);

            Guid passengerID = GetPassengerID(numberCard);

            Guid numberLineID = GetBusLineID(busLine);

            double actualValue = GetLastBalance(passengerID);

            var debits = CreateBalance(passengerID, numberLineID, numberCard, actualValue, Balance.Debit);

            BalanceGreaterThanPassage(debits);

            _BalanceRepository.Add(debits);

            return debits;
        }

        public Balance Credits(Guid numberCard, double value)
        {
            IsValidNumberCard(numberCard);

            if (value < BusLine.PricePassaage)
                throw new Exception("Valor inválido");

            Guid passengerID = GetPassengerID(numberCard);

            var credits = CreateBalance(passengerID, Guid.Empty, numberCard, value, Balance.Credit);

            _BalanceRepository.Add(credits);

            return credits;
        }

        public IEnumerable<Balance> GetBalanceForNumberCardAsNoTracking(Guid numberCard)
        {
            IsValidNumberCard(numberCard);

            return _BalanceRepository.GetBalanceForNumberCardAsNoTracking(numberCard);
        }

        public IEnumerable<Balance> GetBalanceForDateAsNoTracking(DateTime start, DateTime finish)
        {
            IsValidDate(start);
            IsValidDate(finish);

            if (start > DateTime.Now.Date)
                throw new Exception("Data inicial maior que hoje.");

            if (finish < start)
                throw new Exception("Data inicial maior que final.");

            return _BalanceRepository.GetBalanceForDateAsNoTracking(start, finish);
        }

        #region : Methods Auxiliar

        public void BalanceGreaterThanPassage(Balance balance)
        {
            if (balance == null)
                throw new Exception("Erro ao carregar balance.");

            bool isValid = (balance.Value > BusLine.PricePassaage) ? true : false;

            if (!isValid)
                throw new Exception("Saldo insuficiente.");
        }

        public void IsValidDate(DateTime date)
        {
            if (date == DateTime.MinValue)
                throw new Exception("data inválida.");
        }

        public void IsValidBusLine(string busLine)
        {
            if (string.IsNullOrWhiteSpace(busLine))
                throw new Exception("Linha de ônibus não pode ser vazio.");

            bool isValid = _BalanceRepository.IsValidBusLine(busLine);

            if (!isValid)
                throw new Exception("Linha de ônibus inválido");
        }

        public void IsValidNumberCard(Guid numberCard)
        {
            if (numberCard == Guid.Empty)
                throw new Exception("Cartão vazio.");

            bool isValid = _BalanceRepository.IsValidNumberCard(numberCard);

            if (!isValid)
                throw new Exception("Cartão inválido");
        }

        public Guid GetPassengerID(Guid numberCard)
        {
            if (numberCard == Guid.Empty)
                throw new Exception("Número do Cartão não pode ser vazio.");

            Guid passengerID = _BalanceRepository.GetPassengerID(numberCard);

            if (passengerID == Guid.Empty)
                throw new Exception("Passageiro não cadastrdo.");

            return passengerID;
        }

        public Guid GetBusLineID(string busline)
        {
            if (string.IsNullOrWhiteSpace(busline))
                throw new Exception("Número da linha não pode ser vazio.");

            Guid busLineID = _BalanceRepository.GetBusLineID(busline);

            if (busLineID == Guid.Empty)
                throw new Exception("Ônibus não encontrado.");

            return busLineID;
        }

        public double GetLastBalance(Guid passengerID)
        {
            if (passengerID == Guid.Empty)
                throw new Exception("Erro ao carregar valor.");

            double value = _BalanceRepository
                .GetLastBalanceForPassengerIDAndDataRegister(passengerID);

            return value;
        }

        public Balance CreateBalance(Guid passengerID, Guid numberLineID, Guid numberCard, double value, string transactionType)
        {
            var balance = new Balance(passengerID);

            if (transactionType == Balance.Debit)
                balance.SetBusLineID(numberLineID);

            balance.SetNumberCard(numberCard);
            balance.SetValue(value, BusLine.PricePassaage, transactionType);
            balance.SetTransactionType(transactionType);

            return balance;
        }

        #endregion
    }
}
