namespace Shop.Application.Contract.ProductCategoryApplication.Query
{
    public class ProductCategoryAdminQueryModel
    {
        public ProductCategoryAdminQueryModel(int id, string title, string imageName, string creationDate, string updateDate, bool active)
        {
            Id = id;
            Title = title;
            ImageName = imageName;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            Active = active;
        }

        public int Id { get; private set; }
        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public string CreationDate { get; private set; }
        public string UpdateDate { get; private set; }
        public bool Active { get; private set; }
    }
}
