namespace Shop.Application.Contract.ProductGalleryApplication.Query
{
    public class ProductGalleryAdminQueryModel
    {
        public ProductGalleryAdminQueryModel(int id, string imageName, string imageAlt)
        {
            Id = id;
            ImageName = imageName;
            ImageAlt = imageAlt;
        }

        public int Id { get; private set; }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
    }
}
