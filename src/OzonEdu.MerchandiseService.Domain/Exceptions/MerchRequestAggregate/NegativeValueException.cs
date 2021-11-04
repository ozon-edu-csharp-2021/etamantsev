using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class NegativeValueException : Exception
    {
        public NegativeValueException(string message) : base(message)
        {
        }

        public NegativeValueException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}