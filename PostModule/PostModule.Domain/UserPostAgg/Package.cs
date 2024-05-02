using Shared.Domain;

namespace PostModule.Domain.UserPostAgg
{
    public class Package : BaseEntityCreateUpdateActive<int>
    {
        public Package()
        {
            PostOrders = new();
        }
        public Package(string title, string description, int count, int price)
        {
            Title = title;
            Description = description;
            Count = count;
            Price = price;
        }
        public void Edit(string title, string description, int count, int price)
        {
            Title = title;
            Description = description;
            Count = count;
            Price = price;
            UpdateEntity();
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public int Count { get; private set; }
        public int Price { get; private set; }
        public List<PostOrder> PostOrders { get; private set; }
    }
}
