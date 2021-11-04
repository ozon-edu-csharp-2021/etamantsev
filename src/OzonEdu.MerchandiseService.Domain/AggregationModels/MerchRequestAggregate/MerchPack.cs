using OzonEdu.MerchandiseService.Domain.Models;

namespace OzonEdu.MerchandiseService.Domain.AggregationModels.MerchRequestAggregate
{
    public class MerchPack : Enumeration
    {
        public MerchPack(int id, string name) : base(id, name)
        {
        }
        
        /// <summary>
        ///     Выдается сотруднику, вышедшему на работу.
        /// </summary>
        public static MerchPack Welcome = new(1, nameof(Welcome));

        /// <summary>
        ///     Выдается сотруднику, прошедшему испытательный срок (3 месяцы).
        /// </summary>
        public static MerchPack Starter = new(2, nameof(Starter));
        
        /// <summary>
        ///     Выдается сотруднику, участвующему в конференции в качестве слушателя.
        /// </summary>
        public static MerchPack ConferenceListener = new(3, nameof(ConferenceListener));
        
        /// <summary>
        ///     Выдается сотруднику, участвующему в конференции в качестве докладчика.
        /// </summary>
        public static MerchPack ConferenceSpeaker = new(4, nameof(ConferenceSpeaker));
        
        /// <summary>
        ///     Выдается сотруднику, отработавшему в компании больше 5 лет.
        /// </summary>
        public static MerchPack Veteran = new(5, nameof(Veteran));
    }
}