using Shared.Domain;
using Shared.Domain.Enum;

namespace Seos.Domain
{
    public class Seo : BaseEntityCreateUpdate<int>
    {
        public WhereSeo Where { get; private set; }
        public int OwnerId { get; private set; }
        public string MetaTitle { get; private set; }
        public string? MetaDescription { get; private set; }
        public string? MetaKeyWords { get; private set; }
        public bool IndexPage { get; private set; }
        public string? Canonical { get; private set; }
        public string? Schema { get; private set; }
        private Seo()
        {

        }
        public Seo(string metaTitle, string metaDescription, string metaKeyWords,
            bool indexPage, string canonical, string schema, WhereSeo where, int ownerId)
		{
			SetValues(metaTitle, metaDescription, metaKeyWords, indexPage, canonical, schema);
			Where = where;
            OwnerId = ownerId;
        }
        public void Edit(string metaTitle, string metaDescription, string metaKeyWords,
            bool indexPage, string canonical, string schema)
        {
           SetValues(metaTitle, metaDescription, metaKeyWords,indexPage,canonical, schema); 
            UpdateEntity();
        }
        private void SetValues(string metaTitle, string? metaDescription, string? metaKeyWords,
			bool indexPage, string? canonical, string? schema)
        {
			MetaTitle = metaTitle;
			MetaDescription = metaDescription;
			MetaKeyWords = metaKeyWords;
			IndexPage = indexPage;
			Canonical = canonical;
			Schema = schema;
		}
    }
}
