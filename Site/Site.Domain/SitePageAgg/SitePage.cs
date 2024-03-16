using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain.SitePageAgg
{
	public class SitePage : BaseEntityCreateUpdateActive<int> 
	{
		public SitePage(string title, string slug, string description)
		{
			SetValues(title, slug, description);
		}
		public void Edit(string title, string slug, string description)
		{
			SetValues(title, slug, description);
		}
		private void SetValues (string title, string slug, string description)
		{
			Title = title;
			Slug = slug;
			Description = description;
		}
		public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
    }
}
