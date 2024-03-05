using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class EarthAgeHelper
    {
        public List<EarthAge> ages;

        private const int MIN_AGE = -10000;
        private const int MAX_AGE = 4000;

        public EarthAgeHelper()
        {
            ages = new List<EarthAge>();

            InitAges();
        }

        private void InitAges()
        {
            var stoneAge = new EarthAge(MIN_AGE, -2200, "Stone Age", 3, 0.3f);
            stoneAge.PossibleJobs = new List<string> { "Hunter", "Collector" };
            ages.Add(stoneAge);

            var ancientAge = new EarthAge(-2199, 500, "Ancient Age", 5, 0.45f);
            ancientAge.PossibleJobs = new List<string> { "Farmer", "Builder", "Philosopher", "Merchant", "Baker", "Cook", "Dressmaker", "Senator", "Soldier", "Healer", "Slave" };
            ages.Add(ancientAge);

            var middleAge = new EarthAge(501, 1500, "Middle Age", 12, 0.4f);
            middleAge.PossibleJobs = new List<string> { "Farmer", "Builder", "Traveling Entertainer", "Merchant", "Baker", "Noble", "Soldier", "Doctor" };
            ages.Add(middleAge);

            var renaissance = new EarthAge(1501, 1849, "Renaissance", 15, 0.45f);
            renaissance.PossibleJobs = new List<string> { "Farmer", "Builder", "Artist", "Merchant", "Noble", "Baker", "Soldier", "Doctor" };
            ages.Add(renaissance);

            var industrial = new EarthAge(1850, 1932, "Industrial Revolution", 35, 0.55f);
            industrial.PossibleJobs = new List<string> { "Factory Worker", "Worker", "Farmer", "Builder", "Salesman", "Baker", "Soldier", "Doctor", "Artist", "Boss" };
            ages.Add(industrial);

            var ww2 = new EarthAge(1933, 1945, "WW2", 10, 0.2f);
            ww2.PossibleJobs = new List<string> { "Soldier", "Worker", "Farmer", "Builder", "Driver", "Baker", "Soldier", "Doctor", "Sergeant", "General" };
            ages.Add(ww2);

            var rebuild = new EarthAge(1946, 1980, "Rebuilding", 50, 0.55f);
            rebuild.PossibleJobs = new List<string> { "Factory Worker", "Worker", "Farmer", "Builder", "Salesman", "Baker", "Soldier", "Doctor", "Artist", "Boss" };
            ages.Add(rebuild);

            var modern = new EarthAge(1981, 2047, "Modern Times", 40, 0.5f);
            modern.PossibleJobs = new List<string> { "Banker", "Lawyer", "Worker", "Farmer", "Builder", "Salesman", "Baker", "Soldier", "Doctor", "Artist", "Boss" };
            ages.Add(modern);

            var cyber = new EarthAge(2048, MAX_AGE, "Cyberpunk", 30, 0.45f);
            cyber.PossibleJobs = new List<string> { "Factory Worker", "Worker", "Farmer", "Builder", "Salesman", "Baker", "Soldier", "Doctor", "Artist", "Boss" };
            ages.Add(cyber);
        }

        public EarthAge GetEarthAge(long year)
        {
            if(year < MIN_AGE)
            {
                year = MIN_AGE;
            }
            else if (year > MAX_AGE)
            {
                year = MAX_AGE;
            }

            return ages.Find(x => x.IsYearInEarthAge(year));
        }

        public int GetImmigrationRate(long year)
        {
            return GetEarthAge(year).ImmigrationRate;
        }
    }
}
