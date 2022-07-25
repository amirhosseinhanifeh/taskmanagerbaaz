using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.DateTimeExtensions
{
    public static partial class DateTimeExtensions
    {
        public static string ConvertToPersian(this DateTime date, PersianDateTimeFormat format = PersianDateTimeFormat.FullDateFullTime)
        {
            return new PersianDateTime(date).ToString(format);
        }
        public static string ConvertToDayAgo(this DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);

            double delta = Math.Abs(ts.TotalSeconds);

            return ts.Days.ToString();
        }
        public static string ConvertToTimeAgo(this DateTime date)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var ts = new TimeSpan(DateTime.UtcNow.Ticks - date.Ticks);

            double delta = Math.Abs(ts.TotalSeconds);


            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "یک ثانیه پیش" : ts.Seconds + " ثانیه پیش";

            if (delta < 2 * MINUTE)
                return "یک دقیقه پیش";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " دقیقه پیش";

            if (delta < 90 * MINUTE)
                return "یک ساعت پیش";

            if (delta < 24 * HOUR)
                return ts.Hours + " ساعت پیش";

            if (delta < 48 * HOUR)
                return "دیروز";

            if (delta < 30 * DAY)
                return ts.Days + " روز پیش";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "یک ماه پیش" : months + " ماه پیش";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "پارسال" : years + " سال پیش";
            }
        }
    }
}
