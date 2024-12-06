using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class Timeline
    {
        private List<PersonData> _allPersons;
        public List<PersonData> AllPersons {  get { return _allPersons; } }

        private List<Community> _allCommunities;
        public List<Community> AllCommunities { get { return _allCommunities; } }

        private Random _rand;

        private NameGenerator _nameGenerator;

        private EarthAgeHelper _earthAgeHelper;

        private long _startYear;

        private const int _INTERVAL = 1;

        private const int _SEED = 27;

        private const int _YEAR_RANGE = 10;

        private const int _MAX_AGE = 100;

        public Timeline(int startPersons, long startYear = 0)
        {
            _earthAgeHelper = new EarthAgeHelper();
            _allPersons = new List<PersonData>();
            _allCommunities = new List<Community>();
            _startYear = startYear;

            _rand = new Random(_SEED);
            _nameGenerator = new NameGenerator(_rand);

            for (int i = 0; i < startPersons; i++)
            {
                PersonData p = new PersonData(_nameGenerator, (Gender)(_rand.Next(0, 2)));
                p.SetBirthDateRange(startYear, _YEAR_RANGE, _rand);
                p.SetDeathDateRange(_MAX_AGE, _rand);
                p.SetJob(_earthAgeHelper, _rand);

                _allPersons.Add(p);
                Console.WriteLine($"{p.Prename} {p.Surname} - Birthdate:{p.BirthDate}, Death:{p.DeathDate}, Job: {p.Job}");
            }
        }

        public void CalculateTimeline(long startYear, long endYear, bool movingIn = true)
        {
            List<PersonData> availableP = _allPersons;
            List<Community> availableC = _allCommunities;
            for (long i = startYear; i <= endYear; i += _INTERVAL)
            {
                if(movingIn)
                    availableP.AddRange(PeopleMovingIn(i));

                availableP = availableP.FindAll(x => !x.Died && x.Partner == null);
                List<PersonData> availablePersonsTemp = availableP.FindAll(x => x.GetAge(i) > 15 && x.GetAge(i) < 50 && !x.Died);
                availablePersonsTemp = availablePersonsTemp.OrderBy(x => _rand.Next()).ToList();

                for(int j = 0; j < availablePersonsTemp.Count / _rand.Next(1,5); j += 2)
                {
                    if(j+1 < availablePersonsTemp.Count)
                    {
                        availableC.AddRange(CreateFamilyIfPossible(availablePersonsTemp[j], availablePersonsTemp[j+1]));
                    }
                }

                List<Community> availableCommTemp = new List<Community>();
                foreach (Community comm in availableC.Where(x => !x.Calculated))
                {
                    if (comm.CommunityActive(i))
                    {
                        availableCommTemp.Add(comm);

                        var children = comm.CreateChildren(_rand, _nameGenerator, i, _earthAgeHelper);

                        _allPersons.AddRange(children);
                        availableP.AddRange(children);
                    }
                }
                availableC = availableCommTemp;
            }
        }

        private List<PersonData> PeopleMovingIn(long year)
        {
            int movingNum = _rand.Next(0, _earthAgeHelper.GetImmigrationRate(year));

            List<PersonData> movedPersons = new List<PersonData>();

            for (int i = 0; i <= movingNum; i++)
            {
                if (_rand.Next(0, 2) == 0)
                {
                    //Person moving in
                    PersonData p = new PersonData(_nameGenerator, year, _rand, _earthAgeHelper);

                    _allPersons.Add(p);
                    movedPersons.Add(p);
                }
                else
                {
                    //Family Moving in
                    Community c = new Community(year, _rand, _earthAgeHelper, _nameGenerator);
                    _allPersons.AddRange(c.Children);
                    _allPersons.AddRange(c.Adults);
                    _allCommunities.Add(c);
                    movedPersons.AddRange(c.Children);
                    movedPersons.AddRange(c.Adults);
                }
            }
            return movedPersons;
        }

        private List<Community> CreateFamilyIfPossible(PersonData p1, PersonData p2)
        {
            if ((p1.Family == null || p2.Family == null || p1.Family != p2.Family) && (p1.Gender != p2.Gender))
            {
                p1.Partner = p2;
                p2.Partner = p1;
                Community comm = new Community(p1, p2, _rand);
                p1.Family = comm;
                p2.Family = comm;

                _allCommunities.Add(comm);
                return new List<Community> { comm};
            }
            return new List<Community>();
        }

        public List<PersonData> GetPersonsFromYear(long year)
        {
            List<PersonData> availableP = _allPersons.FindAll(x => x.BirthDate.Year < year && x.DeathDate.Year > year);

            return availableP;
        }

        public List<Community> GetCommunitiesFromYear(long year)
        {
            List<Community> availableC = _allCommunities.FindAll(x => x.CommunityActive(year));

            return availableC;
        }

        public void RemovePersonAndAncestors(PersonData person, long deathYear = -1)
        {
            if(person.Family != null && person.Family.Adults.Contains(person))
            {
                foreach(PersonData adult in person.Family.Adults)
                {
                    adult.Partner = null;
                }

                //Remove childrean that were not born in death year
                foreach(PersonData child in person.Family.Children.Where(p => p.BirthDate.Year > deathYear))
                {
                    RemovePersonAndAncestors(child, -1);
                }
                _allCommunities.Remove(person.Family);
            }
            if (deathYear == -1)
            {
                _allPersons.Remove(person);
            }
            else {
                person.DeathDate = new LongDateTime(deathYear);
            }
        }
    }
}
