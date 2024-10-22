using Shared.Application.BaseCommands;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.Contract.ProductApplication.Command
{
    public class CreateProduct : Text_ShortDescription_Title_Slug_Image_ImageAlt
    {
        [Display(Name = "وزن")]
        public int Weight { get; set; }
        public List<int> Categoryids { get; set; }
    }
}
