namespace Site.Application.Contract.MenuApplication.Query
{
    public class MenuForUi
    {
        public int Number { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string? ImageName { get; set; }
        public string? ImageAlt { get; set; }
        public List<MenuForUi>? Childs { get; set; }
    }
}
