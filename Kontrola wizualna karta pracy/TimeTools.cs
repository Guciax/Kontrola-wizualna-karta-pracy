using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontrola_wizualna_karta_pracy
{
    class TimeTools
    {
        public static DateTime GetBeginingDateOfShift(DateTime inputDate, bool eightHoursShift)
        {
            if (eightHoursShift)
            {
                if (inputDate.Hour < 6)
                {
                    DateTime yesterday = inputDate.Date.AddDays(-1);
                    return new DateTime(yesterday.Year, yesterday.Month, yesterday.Day, 22, 0, 0);
                }
                else if (inputDate.Hour >= 6 & inputDate.Hour < 14)
                {
                    return new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 6, 0, 0);
                }
                else if (inputDate.Hour >= 14 & inputDate.Hour < 22)
                {
                    return new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 14, 0, 0);
                }
                else
                {
                    return new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 22, 0, 0);
                }
            }
            else
            {
                if (inputDate.Hour < 6)
                {
                    DateTime yesterday = inputDate.Date.AddDays(-1);
                    return new DateTime(yesterday.Year, yesterday.Month, yesterday.Day, 18, 0, 0);
                }
                else if (inputDate.Hour >= 6 & inputDate.Hour < 18)
                {
                    return new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 6, 0, 0);
                }
                else
                {
                    return new DateTime(inputDate.Year, inputDate.Month, inputDate.Day, 18, 0, 0);
                }
            }
        }

        public static dateShiftNo whatDayShiftIsit(DateTime inputDate)
        {
            int hourNow = inputDate.Hour;
            DateTime resultDate = new DateTime();
            int resultShift = 0;

            if (hourNow < 6)
            {
                resultDate = new DateTime(inputDate.Date.AddDays(-1).Year, inputDate.Date.AddDays(-1).Month, inputDate.Date.AddDays(-1).Day, 22, 0, 0);
                resultShift = 3;
            }

            else if (hourNow < 14)
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 6, 0, 0);
                resultShift = 1;
            }

            else if (hourNow < 22)
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 14, 0, 0);
                resultShift = 2;
            }

            else
            {
                resultDate = new DateTime(inputDate.Date.Year, inputDate.Date.Month, inputDate.Date.Day, 22, 0, 0);
                resultShift = 3;
            }

            dateShiftNo result = new dateShiftNo();
            result.fixedDate = resultDate;
            result.shift = resultShift;

            return result;
        }

        public static DateTime ParseExact(string date)
        {
            try
            {
                if (date.Contains("-"))
                    return DateTime.ParseExact(date, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
                if (date.Contains(@"/"))
                    return DateTime.ParseExact(date, "MM/dd/yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
                else
                    return DateTime.ParseExact(date, "dd.MM.yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None);
            }
            catch (Exception e)
            {
                return new DateTime(1900, 1, 1);
            }
        }

        public class dateShiftNo
        {
            public DateTime fixedDate { get; set; }
            public int shift { get; set; }

            public override bool Equals(object obj)
            {
                dateShiftNo dateItem = obj as dateShiftNo;

                return dateItem.fixedDate == this.fixedDate;
            }

            public override int GetHashCode()
            {
                // Which is preferred?

                //return base.GetHashCode();

                return this.fixedDate.GetHashCode();
            }
        }
    }
}
