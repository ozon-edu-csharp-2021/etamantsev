using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class IssuedException : Exception
    {
        public IssuedException(string message) : base(message)
        {
        }

        public IssuedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}