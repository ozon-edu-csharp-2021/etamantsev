using System;

namespace OzonEdu.MerchandiseService.Domain.Exceptions.MerchRequestAggregate
{
    public class NotValidRequestStatusException : Exception
    {
        public NotValidRequestStatusException(string message) : base(message)
        {
            
        }
        
        public NotValidRequestStatusException(string message, Exception innerException) : base(message, innerException)
        {
            
        }
    }
}