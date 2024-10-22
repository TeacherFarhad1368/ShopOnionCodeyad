using Shared.Application.BaseCommands;

namespace Shop.Application.Contract.ProductGalleryApplication.Command
{
    public class CreateProductGallery : Image_ImageAlt
    {
        public int ProductId { get; set; }
    }
}
