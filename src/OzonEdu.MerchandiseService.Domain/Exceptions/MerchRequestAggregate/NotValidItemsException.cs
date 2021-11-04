using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class NotValidItemsException : Exception
    {
        public NotValidItemsException(string message) : base(message)
        {
        }

        public NotValidItemsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}