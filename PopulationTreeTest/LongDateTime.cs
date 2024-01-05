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
    }
}
