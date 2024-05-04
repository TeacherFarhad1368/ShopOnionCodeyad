using Shared.Domain;

namespace PostModule.Domain.UserPostAgg
{
    public class Package : BaseEntityCreateUpdateActive<int>
    {
        public Package()
        {
            PostOrders = new();
        }
        public Package(string title, string description, int count, int price,string imageName,string imageAlt)
        {
            Title = title;
            Description = description;
            Count = count;
            Price = price;
            ImageName = imageName;
            ImageAlt = imageAlt;
        }
        public void Edit(string title, string description, int count, int price, string imageName, string imageAlt)
        {
            Title = title;
            Description = description;
            Count = count;
            Price = price;
            ImageName = imageName;
            ImageAlt = imageAlt;
            UpdateEntity();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public List<PostOrder> PostOrders { get; private set; }
    }
}
