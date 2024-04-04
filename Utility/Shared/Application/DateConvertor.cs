using System.Globalization;

namespace Shared.Application
{
    public static class DateConvertor
    {
        public static string[] MonthNames =
             {"فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"};

        public static string[] DayNames = { "شنبه", "یکشنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه" };
        public static string[] DayNamesG = { "یکشنبه", "دو شنبه", "سه شنبه", "چهار شنبه", "پنج شنبه", "جمعه", "شنبه" };


        public static string ToPersainDate(this DateTime? date)
        {
            try
            {
                if (date != null) return date.Value.ToPersainDate();
            }
            catch (Exception)
            {
                return "";
            }

            return "";
        }

        public static string ToPersainDate(this DateTime date)
        {
            if (date == new DateTime()) return "";
            var pc = new PersianCalendar();
            return $"{pc.GetYear(date)} {MonthNames[pc.GetMonth(date) - 1]} {pc.GetDayOfMonth(date):00}";
        }

        public static string ToDiscountFormat(this DateTime date)
        {
            if (date == new DateTime()) return "";
            return $"{date.Year}/{date.Month}/{date.Day}";
        }

        private static readonly string[] Pn = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        private static readonly string[] En = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public static string ToEnglishNumber(this string strNum)
        {
            var cash = strNum;
            for (var i = 0; i < 10; i++)
                cash = cash.Replace(Pn[i], En[i]);
            return cash;
        }

        public static string ToPersianNumber(this int intNum)
        {
            var chash = intNum.ToString();
            for (var i = 0; i < 10; i++)
                chash = chash.Replace(En[i], Pn[i]);
            return chash;
        }


        public static DateTime ToEnglishDateTime(this string persianDate)
        {
            persianDate = persianDate.ToEnglishNumber();
            var year = Convert.ToInt32(persianDate.Substring(0, 4));
            var month = Convert.ToInt32(persianDate.Substring(5, 2));
            var day = Convert.ToInt32(persianDate.Substring(8, 2));
            return new DateTime(year, month, day, new PersianCalendar());
        }


    }
}
