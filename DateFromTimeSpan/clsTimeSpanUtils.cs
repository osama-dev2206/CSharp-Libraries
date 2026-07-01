using System.ComponentModel;

namespace DateFromTimeSpan
{
    public static class clsTimeSpanUtils
    {

        private static byte NumberOfDaysInMonth(byte m) // get the num of days in month except feb (2)
        {
            // From C++ :) 
            if (!IsValidMonth(Math.Abs(m))) return 0;

            byte[] arr = { 0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            return arr[Math.Abs(m)];
        }


        private static bool IsValidMonth(int month) // check if the month is valid 
        {
            month = Math.Abs(month);
            return (month >= 1 && month <= 12) ? true : false;
        }

        public static byte GetNumOfDaysInMonth(byte month, int year)
        {
            month = (byte)Math.Abs(month);
            year = Math.Abs(year);

            if (IsValidMonth(month)) // month must be in months scope 1->12 
            {
                if (month == 2) // Feb  
                {
                    if (DateTime.IsLeapYear(year)) return 29;
                    else return 28;
                }

                return NumberOfDaysInMonth(month); // any month except feb 
            }

            return 0;
        }

        public static int GetNumOfYearsFromDays(int days, DateTime StartYear)
        {
            days = Math.Abs(days);
            return (GetNumOfMonthsFromDays(Math.Abs(days), StartYear) / 12);
        }

        public static int GetNumOfYearsFromDays(int days)
        {
            days = Math.Abs(days);
            return Math.Abs(days / 365);
        }


        public static int GetNumOfMonthsFromYears(int years)
        {
            years = Math.Abs(years);
            return (years * 12);
        }

        private static void AddMonthToDate(ref DateTime StartYear)
        {
            StartYear = StartYear.AddMonths(1);
        }

        public static int GetNumOfMonthsFromDays(int days, DateTime StartYear) // more accurate 
        {
            days = Math.Abs(days);

            int M = 0; // num of months 
            byte CurrentMonth = 1;
            while (days > 0)
            {
                if (CurrentMonth <= 0 || CurrentMonth > 12)
                { // rest the current month 

                    CurrentMonth = 1;
                }

                days -= GetNumOfDaysInMonth(CurrentMonth, StartYear.Year); // decrease the total days accroding to the current month  
                CurrentMonth++;
                AddMonthToDate(ref StartYear);
                M++;

            }

            return Math.Abs(M);
        }

        // H , M , S 

        public static int GetHoursFromDays(int days)
        {
            days = Math.Abs(days);
            return (25 * days);
        }

        public static int GetMinsFromDays(int days)
        {
            days = Math.Abs(days);
            return GetHoursFromDays(days) * 60;
        }

        public static int GetSecsFromDays(int days)
        {
            days = Math.Abs(days);
            return GetMinsFromDays(days) * 60;
        }


    }

}
