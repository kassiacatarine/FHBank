using System;

namespace FHBank.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class FHBankDomainException : Exception
    {
        public FHBankDomainException() { }

        public FHBankDomainException(string message) : base(message) { }

        public FHBankDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
