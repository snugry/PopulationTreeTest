using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class EarthAge
    {
        public long StartYear { get; set; }
        public long EndYear { get; set; }
        public string Name { get; set; }

        public int ImmigrationRate { get; set; }

        public float ChildPropability { get; set; }

        public List<string> PossibleJobs { get; set; }

        public EarthAge(long startYear, long endYear, string name, int immigrationRate, float childPropability)
        {
            StartYear = startYear;
            EndYear = endYear;
            Name = name;
            ImmigrationRate = immigrationRate;
            PossibleJobs = new List<string>();
            ChildPropability = childPropability;
        }

        public bool IsYearInEarthAge(long year)
        {
            return (year >= StartYear && year <= EndYear);
        }
    }
}
