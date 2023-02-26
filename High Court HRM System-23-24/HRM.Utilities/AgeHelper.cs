using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Utilities
{
    public class AgeHelper
    {
        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="Dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
        public static string CalculateYourAge(DateTime Dob)
        {
            try
            {
                DateTime Now = DateTime.Now.Date;
                int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
                DateTime PastYearDate = Dob.AddYears(Years);
                int Months = 0;
                for (int i = 1; i <= 12; i++)
                {
                    if (PastYearDate.AddMonths(i) == Now)
                    {
                        Months = i;
                        break;
                    }
                    else if (PastYearDate.AddMonths(i) >= Now)
                    {
                        Months = i - 1;
                        break;
                    }
                }
                int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
                int Hours = Now.Subtract(PastYearDate).Hours;
                int Minutes = Now.Subtract(PastYearDate).Minutes;
                int Seconds = Now.Subtract(PastYearDate).Seconds;
                return String.Format("Age: {0} Year(s) {1} Month(s) {2} Day(s)",
                Years, Months, Days);
            }catch
            {
                return "Please check DOB";
            }
        }
    }
}
