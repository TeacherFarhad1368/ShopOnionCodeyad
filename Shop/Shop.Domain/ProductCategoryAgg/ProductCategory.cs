using Shared.Domain;
using Shop.Domain.ProductCategoryRelationAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ProductCategoryAgg
{
    public class ProductCategory : BaseEntityCreateUpdateActive<int>
    {
        public ProductCategory(string title, string slug, string imageName, string imageAlt,
            int parent)
        {
            SetValues(title, slug, imageName, imageAlt);
            Parent = parent;
        }

        public void Edit(string title, string slug, string imageName, string imageAlt)
        {
            SetValues(title, slug, imageName, imageAlt);
        }
        private void SetValues(string title, string slug, string imageName, string imageAlt)
        {
            Title = title;
            Slug = slug;
            ImageName = imageName;
            ImageAlt = imageAlt;
        }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public int Parent { get; private set; }
        public List<ProductCategoryRelation> ProductCategoryRelations { get; private set; }
        public ProductCategory()
        {
            ProductCategoryRelations = new();
        }
    }
}
