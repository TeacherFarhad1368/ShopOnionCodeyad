namespace Site.Application.Contract.MenuApplication.Query
{
    public class MenuForUi
    {
        public int Number { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }
        public string? ImageName { get; private set; }
        public string? ImageAlt { get; private set; }
        public List<MenuForUi>? Childs { get; private set; }
    }
}
