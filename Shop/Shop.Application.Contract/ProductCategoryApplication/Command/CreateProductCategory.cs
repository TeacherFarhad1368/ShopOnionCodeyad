using Shared.Application.BaseCommands;

namespace Shop.Application.Contract.ProductCategoryApplication.Command
{
    public class CreateProductCategory : Title_Slug_Image_ImageAlt
    {
        public int Parent { get; set; }
    }
}
