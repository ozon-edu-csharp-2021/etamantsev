namespace OzonEdu.MerchandiseService.HttpModels.RequestModel
{
    public class NewMerchSupplyModel
    {
        /// <summary>
        /// Идентификатор товара
        /// </summary>
        public long SkuId { get; set; }

        /// <summary>
        /// Количество товаров.
        /// </summary>
        public long Quantity { get; set; }
    }
}