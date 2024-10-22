using Shared.Domain;
using Shared.Domain.Enum;

namespace Stores.Domain.StoreAgg
{
    public class StoreProduct : BaseEntityCreate<int>
    {
        public int StoreId { get; private set; }
        public int ProductSellId { get; private set; }
        public StoreProductType Type { get; private set; }
        public int Count { get; private set; }
        public Store Store { get; private set; }
        public StoreProduct()
        {
            Store = new();
        }

        public StoreProduct(int storeId, int productSellId, StoreProductType type, int count)
        {
            StoreId = storeId;
            ProductSellId = productSellId;
            Type = type;
            Count = count;
        }
    }
}
