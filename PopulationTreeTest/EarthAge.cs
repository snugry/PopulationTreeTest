using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class EarthAge
    {
        public string Name { get; set; }
        public long StartYear { get; set; }
        public long EndYear { get; set; }

        public int ImmigrationRate { get; set; }

        public float ChildPropability { get; set; }

        public int MaxChildNumber { get; set; }

        public List<Job> PossibleJobs { get; set; }

        public int MaxHouseFloors {  get; set; }

        public EarthAge(long startYear, long endYear, string name, int immigrationRate, float childPropability)
        {
            StartYear = startYear;
            EndYear = endYear;
            Name = name;
            ImmigrationRate = immigrationRate;
            PossibleJobs = new List<Job>();
            ChildPropability = childPropability;
        }

        public bool IsYearInEarthAge(long year)
        {
            return (year >= StartYear && year <= EndYear);
        }
    }
}
