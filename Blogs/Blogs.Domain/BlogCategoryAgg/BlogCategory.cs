using Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Domain.BlogCategoryAgg
{
    public class BlogCategory : BaseEntity<int>
    {
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public int Parent { get; private set; }
        public DateTime UpdateDate { get; private set; }
        public bool Active { get; private set; }

        public BlogCategory(string title, string slug, string imageName, string imageAlt,
            int parent)
        {
            Title = title;
            Slug = slug;
            ImageName = imageName;
            ImageAlt = imageAlt;
            Parent = parent;
            UpdateDate = DateTime.Now;
            Active = true;
        }

        public void Edit(string title, string slug, string imageName, string imageAlt)
        {
            Title = title;
            Slug = slug;
            ImageName = imageName;
            ImageAlt = imageAlt;
            UpdateDate = DateTime.Now;
        }
        public void ActivationChange()
        {
            if (Active) Active = false;
            else Active = true;
        }
    }
}
