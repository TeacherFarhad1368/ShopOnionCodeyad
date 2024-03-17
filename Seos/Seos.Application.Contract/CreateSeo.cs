using System.ComponentModel.DataAnnotations;
using Shared.Domain.Enum;

namespace Seos.Application.Contract
{
    public class CreateSeo
    {
        public int OwnerId { get; set; }
        public WhereSeo Where { get; set; }
        [Display(Name = "MetaTitle")]
        [Required(ErrorMessage = "MetaTitle اجباری است")]
        [MaxLength(500, ErrorMessage = "MetaTitle باید کمتر از 500 حرف باشد .")]
        public string MetaTitle { get; set; }
        [Display(Name = "MetaDescription")]
        [MaxLength(800, ErrorMessage = "MetaDescription باید کمتر از 800 حرف باشد .")]
        public string? MetaDescription { get; set; }
        [Display(Name = "MetaKeyWords")]
        [MaxLength(500, ErrorMessage = "MetaKeyWords باید کمتر از 500 حرف باشد .")]
        public string? MetaKeyWords { get; set; }
        [Display(Name = "IndexPage شود ؟")]
        public bool IndexPage { get; set; }
        [Display(Name = "Canonical")]
        [MaxLength(500, ErrorMessage = "Canonical باید کمتر از 500 حرف باشد .")]
        public string? Canonical { get; set; }
        [Display(Name = "Schema")]
        public string? Schema { get; set; }
    }
}
