namespace PostModule.Application.Contract.UserPostApplication.Query
{
    public class PackageAdminQueryModel
    {
        public PackageAdminQueryModel(int id,string title, int count, int price, string creationDate, string updateDate, bool active)
        {
            Title = title;
            Count = count;
            Price = price;
            CreationDate = creationDate;
            UpdateDate = updateDate;
            Active = active;
        }
        public int Id { get; private set; }
        public string Title { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public string CreationDate { get; private set; }
        public string UpdateDate { get; private set; }
        public bool Active { get; private set; }
    }
}
