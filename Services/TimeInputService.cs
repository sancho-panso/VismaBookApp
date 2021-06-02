using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class TimeInputService
    {
        public static DateTime GetTimeFromInput(string inputString)
        {
            DateTime date;
            string[] dateFormats = new[] { "yyyy/MM/dd", "MM/dd/yyyy" };
            CultureInfo provider = new CultureInfo("en-US");

            if (DateTime.TryParseExact(inputString, dateFormats, provider,
            DateTimeStyles.AdjustToUniversal, out date))
            {
                return date;
            }
            else
            {
                return DateTime.Now;
            }

        }
    }
}
