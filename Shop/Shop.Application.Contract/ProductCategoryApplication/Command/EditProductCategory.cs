using Shared.Application.BaseCommands;

namespace Shop.Application.Contract.ProductCategoryApplication.Command
{
    public class EditProductCategory : Title_Slug_Image_ImageAlt
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
    }
}
