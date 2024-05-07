using Shared;

namespace Query.Contract.UserPanel.PostOrder;

public class PostOrderUserPanelPaging : BasePaging
{
    public List<PostOrderUserPanelQueryModel> Orders { get; set; }
}
