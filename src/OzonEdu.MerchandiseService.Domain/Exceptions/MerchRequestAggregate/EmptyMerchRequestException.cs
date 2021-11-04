using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class EmptyMerchRequestException : Exception
    {
        public EmptyMerchRequestException(string message) : base(message)
        {
        }

        public EmptyMerchRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}