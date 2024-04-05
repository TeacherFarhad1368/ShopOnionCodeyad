namespace Query.Contract.UI
{
    public class SeoUiQueryModel
    {
        public SeoUiQueryModel(string metaTitle, string? metaDescription, string? metaKeyWords, bool indexPage, string? canonical, string? schema)
        {
            MetaTitle = metaTitle;
            MetaDescription = metaDescription;
            MetaKeyWords = metaKeyWords;
            IndexPage = indexPage;
            Canonical = canonical;
            Schema = schema;
        }

        public string MetaTitle { get; private set; }
        public string? MetaDescription { get; private set; }
        public string? MetaKeyWords { get; private set; }
        public bool IndexPage { get; private set; }
        public string? Canonical { get; private set; }
        public string? Schema { get; private set; }
    }
}
