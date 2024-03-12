using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application
{
    public static class ValidationMessages
    {
        public const string RequiredMessage = "این فیلد اجباری است .";
        public const string MaxLengthMessage = "تعداد کاراکتر ها بیش از حد مجاز است .";
        public const string MinLengthMessage = "تعداد کاراکتر ها کنتر از حد مجاز است .";
        public const string SystemErrorMessage = "خطای سیستم !!!! مجددا تلاش کنید .";
        public const string DuplicatedMessage = "این فیلد تکراری است .";
        public const string ImageErrorMessage = "لطفا یک تصویر معتبر وارد کنید .";
        public const string MobileErrorMessage = "لطفا یک شماره موبایل معتبر وارد کنید .";
        public const string PasswordErrorMessage = "لطفا یک کلمه عبور بین 4 و 9 حرف وشامل یک حرف کوچک انگلیسی و یک حرف بزرگ انگلیسی و بدون فاصله وارد کنید .";
        public const string OldPasswordErrorMessage = "کلمه عبور فعلی اشتباه است.";
        public const string PasswordCompare =  "کلمات عبور مغایرت دارند . ";
        public const string PasswordLoginError =  "کلمع عبور اشتباه است . ";
        public const string UserNotFound =  "کاربری یافت نشد . ";
        public const string ChoosePermission = "سطح دسترسی را انتخاب کنید  . ";
        public const string ParentCategoryMessage = "سرگروه را انتخاب کنید  . ";
        public const string ChildCategoryMessage = "زیرگروه را انتخاب کنید  . ";
    }
}
