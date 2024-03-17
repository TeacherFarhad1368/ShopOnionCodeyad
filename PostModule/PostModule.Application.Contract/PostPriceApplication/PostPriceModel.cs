namespace PostModule.Application.Contract.PostPriceApplication
{
    public class PostPriceModel
    {
        public int Id { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public int TehranPrice { get; set; }
        public int StateCenterPrice { get; set; }
        public int CityPrice { get; set; }
        public int InsideStatePrice { get; set; }
        public int StateClosePrice { get; set; }
        public int StateNonClosePrice { get; set; }
    }
}
