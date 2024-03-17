namespace PostModule.Application.Contract.PostApplication
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public int TehranPricePlus { get; set; }
        public int StateCenterPricePlus { get; set; }
        public int CityPricePlus { get; set; }
        public int InsideStatePricePlus { get; set; }
        public int StateClosePricePlus { get; set; }
        public int StateNonClosePricePlus { get; set; }
        public string CreationDate { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public bool InsideCity { get; set; }
        public bool OutsideCity { get; set; }
    }
}
