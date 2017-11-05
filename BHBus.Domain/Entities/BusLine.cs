using System;
using System.Collections.Generic;

namespace BHBus.Domain.Entities
{
    [Serializable]
    public class BusLine
    {
        public const int BusLineMaxLength = 9;

        public const double PricePassaage = 3.8;

        public Guid BusLineID { get; private set; }

        public string Line { get; private set; }

        public DateTime DateRegister { get; private set; }

        public bool Active { get; private set; }

        public bool GetBusLineActive(BusLine linha)
        {
            return (linha.DateRegister.Year <= 5);
        }

        protected BusLine()
        {
          
        }

        public BusLine(string line)
        {
            SetLine(line);
        }

        public void SetLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new Exception("Numero da Linha não pode ser vazio.");

            if (line.Length > BusLineMaxLength)
                throw new Exception("Numero da Linha são no maximo 9 caracteres.");

            Line = line;
        }
    }
}
