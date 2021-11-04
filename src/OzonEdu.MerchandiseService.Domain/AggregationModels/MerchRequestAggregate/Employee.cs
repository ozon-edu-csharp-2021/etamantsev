using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    /// <summary>
    ///     Cотрудник.
    /// </summary>
    public class Employee : Entity
    {
        public Employee(Name name)
        {
            Name = name;
        }

        public Name Name { get; }
    }
}