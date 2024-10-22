using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stores.Domain.StoreAgg
{
    public class Store : BaseEntityCreate<int>
    {
        public int SellerId { get; private set; }
        public string Description { get; private set; }
        public List<StoreProduct> StoreProducts { get; private set; }
        public Store()
        {
            StoreProducts = new();
        }
        public void EditDescription(string des)
        {
            Description = des;
        }
        public Store(int sellerId, string description)
        {
            SellerId = sellerId;
            Description = description;
        }
    }
}
