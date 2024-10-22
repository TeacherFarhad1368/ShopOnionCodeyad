namespace Shop.Application.Contract.ProductGalleryApplication.Query
{
    public class ProductGalleryAdminPageQueryModel
    {
       
        public int ProductId { get; set; }
        public string Title { get; set; }
        public List<ProductGalleryAdminQueryModel> Galleries { get; set; }
    }
}
