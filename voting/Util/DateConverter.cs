using System.Globalization;

namespace voting.Util
{
    public class DateConverter
    {
        public static DateTime ConvertPersianToMiladi(string persianDate)
        {
            // تبدیل اعداد فارسی به لاتین
            string westernDate = ConvertFarsiToWesternDigits(persianDate);

            // جدا کردن قسمت‌های تاریخ
            var parts = westernDate.Split('/');
            if (parts.Length != 3)
                throw new FormatException("فرمت تاریخ نامعتبر است. فرمت صحیح: yyyy/mm/dd");

            int year = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int day = int.Parse(parts[2]);

            // ایجاد تقویم شمسی
            PersianCalendar pc = new PersianCalendar();
            return pc.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        // تابع تبدیل اعداد فارسی به لاتین
        private static string ConvertFarsiToWesternDigits(string input)
        {
            Dictionary<char, char> farsiToWestern = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9'
            };

            char[] converted = new char[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                converted[i] = farsiToWestern.ContainsKey(input[i])
                    ? farsiToWestern[input[i]]
                    : input[i];
            }

            return new string(converted);
        }
        public static string ConvertToPersianDate(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();

            int year = pc.GetYear(dateTime);
            int month = pc.GetMonth(dateTime);
            int day = pc.GetDayOfMonth(dateTime);

            return $"{year}/{month}/{day}";
        }
    }
}
