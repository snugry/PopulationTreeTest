using PopulationTreeTest.FileIO;
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
            JsonReader _json = new JsonReader();
            ages = _json.ReadJson(@"json\earthAges.json");
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
