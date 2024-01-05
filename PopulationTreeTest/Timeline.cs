using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class Timeline
    {
        private List<Person> _allPersons;

        private Random _rand;

        private NameGenerator _nameGenerator;

        private long _startYear;

        private const int _INTERVAL = 10;

        private const int _SEED = 666;

        private const int _YEAR_RANGE = 30;

        private const int _MAX_AGE = 65;

        private const int _CHILD_RATE = 3;

        public Timeline(int startPersons, long startYear = 0)
        {
            _allPersons = new List<Person>();
            _startYear = startYear;

            _rand = new Random(_SEED);
            _nameGenerator = new NameGenerator(_rand);

            for (int i = 0; i < startPersons; i++)
            {
                Person p = new Person(_nameGenerator, (Gender)(_rand.Next(0, 1)));
                p.SetBirthDateRange(startYear, _YEAR_RANGE, _rand);
                p.SetDeathDateRange(_MAX_AGE, _rand);

                _allPersons.Add(p);
                Console.WriteLine($"{p.Prename} {p.Surname}- Birthdate:{p.BirthDate}, Death:{p.DeathDate}");
            }
        }

        public void CalculateTimeline(long startYear, long endYear)
        {
            for(long i = startYear; i <= endYear; i += _INTERVAL)
            {
                List<Person> availableP = _allPersons.FindAll(x => x.GetAge(i) > 15 && x.GetAge(i) < 45 && !x.Died && x.Partner == null);
                availableP = availableP.OrderBy(x => Random.Shared.Next()).ToList();

                for(int j = 0; j < availableP.Count; j += 2)
                {
                    if(j+1 < availableP.Count)
                    {
                        Person p1 = availableP[j];
                        Person p2 = availableP[j+1];

                        if(p1.Partner == null && p2.Partner == null && ((p1.Family == null && p1.Family == null) || p1.Family != p2.Family))
                        {
                            p1.Partner = p2;
                            p2.Partner = p1;
                            Community comm = new Community(p1, p2, _rand);
                            p1.Family = comm;
                            p2.Family = comm;

                            _allPersons.AddRange( comm.CreateChildren(_rand, _nameGenerator, _CHILD_RATE));
                        }
                    }
                }
            }
        }

        public List<Person> GetPersonsFromYear(long year)
        {
            List<Person> availableP = _allPersons.FindAll(x => x.BirthDate.Year < year && x.DeathDate.Year > year);

            return availableP;
        }
    }
}
