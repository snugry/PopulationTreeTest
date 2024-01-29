using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class LongDateTime
    {
        public long Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public LongDateTime(long year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public LongDateTime(long year) 
        { 
            Year = year;
            Month = 1;
            Day = 1;
        }

        public void AddYears(long years)
        {
            Year = Year + years;
        }

        public override string ToString()
        {
            return $"{Day}.{Month}.{Year}";
        }

        public static int GetDaysInMonth(int month, long year)
        {
            if(month == 1 ||  month == 3 || month == 5 || month == 7 || month == 8 || month == 10 || month == 12)
            {
                return 31;
            }
            else if(month == 4 || month == 6 || month == 9 || month == 11)
            {
                return 30;
            }
            else
            {
                if(year % 4 > 0)
                {
                    return 28;
                }
                else
                {
                    return 29;
                }
            }
        }
    }
}
