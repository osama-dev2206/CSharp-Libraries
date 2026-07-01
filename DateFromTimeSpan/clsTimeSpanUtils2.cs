using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateFromTimeSpan
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    namespace DateTime_Calculator
    {
        public static partial  class clsTimeSpanUtils
        {

            static public bool CheckDate(string Date, out DateTime Res) // check if the date (string) is valid format or not 
            {
                return DateTime.TryParse(Date, out Res); // will return true if correct date 
            }

            static public bool IsValidBirthDate(DateTime DateOfBirth) // check if the date of birth is smaller than or equl current date 
            {
                if (DateOfBirth == default(DateTime)) return false;
                return (DateOfBirth < DateTime.Now) ? true : false;
            }

            static public TimeSpan GetDiffBetweenNowDateAndCurrentDate(DateTime Date)
            {
                if (IsValidBirthDate(Date)) return (DateTime.Now - Date);
                else return new TimeSpan(0, 0, 0);
            }

            static private void CheckDefaultDate(ref DateTime Date) // if date is default date then set the date to now 
            {
                if (Date == default(DateTime))
                    Date = DateTime.Now;
            }

            static public TimeSpan GetDiffBetweenDate1AndDate2(DateTime Date1, DateTime Date2)
            {

                if (!CheckDate(Date1.Date.ToString(), out _) || !CheckDate(Date2.Date.ToString(), out _)) return new TimeSpan(0, 0, 0);
                else
                {
                    CheckDefaultDate(ref Date1); // if date is the default date then set it to now 
                    CheckDefaultDate(ref Date2); // if date is the default date then set it to now 
                    return (Date1) - (Date2);
                }
            }

            static public string ExtractDateFromDateTimePicker(dynamic dateTimePicker)
            {
                return dateTimePicker.Value.ToString().Remove(dateTimePicker.Value.ToString().IndexOf(" ")).Trim();

            }



        }


    }

}
