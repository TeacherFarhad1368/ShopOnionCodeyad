using Shared.Application;
using System.ComponentModel.DataAnnotations;

namespace PostModule.Application.Contract.PostSettingApplication.Command
{
    public class UbsertPostSetting
    {
        [Display(Name = "عنوان صفحه پکیج های فروش Api پست")]
        [MaxLength(255,ErrorMessage =ValidationMessages.MaxLengthMessage)]
        public string? PackageTitle { get; set; }
        [Display(Name = "توضیحات صفحه پکیج های فروش Api پست")]
        public string? PackageDescription { get; set; }

        [Display(Name = "توضیحات استفاده از Api پست در پنل کاربر")]
        public string? ApiDescription { get;  set; }
    }
}
