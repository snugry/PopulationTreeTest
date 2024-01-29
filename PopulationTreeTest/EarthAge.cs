using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class EarthAge
    {
        public long StartYear { get; private set; }
        public long EndYear { get; private set; }
        public string Name { get; private set; }

        public int ImmigrationRate { get; private set; }

        public float ChildPropability { get; private set; }

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
