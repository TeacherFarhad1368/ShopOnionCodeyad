using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain.BlogAgg
{
    public class Blog : BaseEntityCreateUpdateActive<int>
    {
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string ShortDescription { get; private set; }
        public string Text { get; private set; }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public int CategoryId { get; private set; }
        public int SubCategoryId { get; private set; }
        public int VisitCount { get; private set; }
        public long UserId { get; private set; }
        public string Writer { get; private set; }

        public Blog(string title, string slug, string shortDescription,string text,
            string imageName, string imageAlt, int categoryId,
            int subCategoryId, long userId, string writer)
		{
			SetValus(title, slug, shortDescription, text, imageName, imageAlt, categoryId, subCategoryId, writer);
			VisitCount = 0;
			UserId = userId;
		}
        public void Edit(string title, string slug, string shortDescription,string text,
            string imageName, string imageAlt, int categoryId,
           int subCategoryId, string writer)
        {
            SetValus(title, slug, shortDescription, text, imageName, imageAlt, categoryId, subCategoryId, writer);
            UpdateEntity();
        }
        public void VisitPlus()
        {
            VisitCount = VisitCount + 1;
        }
        private void SetValus(string title, string slug, string shortDescription, string text,
			string imageName, string imageAlt, int categoryId,
		   int subCategoryId, string writer)
        {
			Title = title;
			Slug = slug;
			ShortDescription = shortDescription;
			Text = text;
			ImageName = imageName;
			ImageAlt = imageAlt;
			CategoryId = categoryId;
			SubCategoryId = subCategoryId;
			Writer = writer;
		}
    }
}
