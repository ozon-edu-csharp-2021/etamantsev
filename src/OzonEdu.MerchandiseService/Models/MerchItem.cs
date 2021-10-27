namespace OzonEdu.MerchandiseService.Models
{
    public class MerchItem
    {
        public MerchItem(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }

        public string Name { get; }
    }
}