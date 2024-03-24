namespace PostModule.Application.Contract.PostQuery
{
    public class PostAdminQueryModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreationDate { get; set; }
        public bool Active { get; set; }
        public bool InsideCity { get; set; }
        public bool OutsideCity { get; set; }
    }
}
