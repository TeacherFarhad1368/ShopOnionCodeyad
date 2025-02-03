namespace Shop.Application.Contract.OrderApplication.Command
{
    public class ShopCartViewModel
    {
        public int productId { get; set; }
        public int productSellId { get; set; }
        public int price { get; set; }
        public int priceAfterOff { get; set; }
        public int count { get; set; }
        public string title { get; set; }
        public string shopTitle { get; set; }
        public string unit { get; set; }
        public string slug { get; set; }
        public string imageName { get; set; }
    }
}
