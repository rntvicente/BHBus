using System;
using System.Text.RegularExpressions;

namespace BHBus.Domain.ValueObject
{
    public class Email
    {
        public const int AddressMaxLength = 254;

        public string Address { get;  private set; }

        protected Email()
        {

        }

        public Email(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                throw new Exception("E-mail não pode ser em vazio.");

            if (address.Length > AddressMaxLength)
                throw new Exception("E-mail permite no maximo 254 carateres.");

            if (!IsValid(address))
                throw new Exception("E-mail invalido.");

            this.Address = address;
        }

        public static bool IsValid(string address)
        {
            var regexEmail = new Regex(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$");

            return regexEmail.IsMatch(address);
        }
    }
}
