namespace Site.Application.Contract.SliderApplication.Query
{
    public class SliderForAdmin
    {
        public int Id { get; set; }
        public string ImageAlt { get; set; }
        public string ImageName { get; set; }
        public bool Active { get; set; }
        public string CreationDate { get; set; }
    }
}
