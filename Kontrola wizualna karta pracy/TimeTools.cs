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
    }
}
