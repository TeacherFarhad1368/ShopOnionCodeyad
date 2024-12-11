using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Query.Contract.UserPanel.Store
{
    public interface IStoreUserPanelQuery
    {
        StoreUserPanelPaging GetStoresForUserPanel(int userId,int sellerId,string filter,int pageId,int take);
    }
    public class StoreUserPanelPaging : BasePaging
    {
        public int SellerId { get; set; }
        public string PageTitle { get; set; }
        public string Filter { get; set; }
        public List<StoreUserPanelQueryModel> Stores { get; set; }
    }
    public class StoreUserPanelQueryModel
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public string CreationDate { get; set; }
    }
}
