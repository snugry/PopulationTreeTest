﻿using System;
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

        private List<House> _allHouses;

        private List<Flat> _allFlats;

        private Dictionary<int, Dictionary<int, House>> _yearHouseMap;
        public List<House> AllHouses { get {  return _allHouses; } }

        private Dictionary<int, Disaster> _disasters;

        private Random _rand;

        private NameGenerator _nameGenerator;

        private EarthAgeHelper _earthAgeHelper;

        private long _startYear;

        private const int _INTERVAL = 1;

        private const int _SEED = 27;

        private const int _YEAR_RANGE = 10;

        private const int _MIN_MARRIAGE_AGE = 15;
        private const int _MAX_MARRIAGE_AGE = 50;

        private const int _MAX_AGE = 100;

        public Timeline(int startPersons, int startYear = 0)
        {
            _earthAgeHelper = new EarthAgeHelper();
            _allPersons = new List<PersonData>();
            _allCommunities = new List<Community>();
            _allHouses = new List<House>();
            _allFlats = new List<Flat>();
            _yearHouseMap = new Dictionary<int, Dictionary<int, House>>();

            _startYear = startYear;
            _disasters = new Dictionary<int, Disaster>();

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

        public void CalculateTimeline(int startYear, int endYear, bool movingIn = true)
        {
            List<PersonData> availableP = _allPersons;
            List<Community> availableC = _allCommunities;
            for (int i = startYear; i <= endYear; i += _INTERVAL)
            {
                if (movingIn)
                {
                    availableP.AddRange(PeopleMovingIn(i));
                }
                //Unfortunately we have to check died before and after Disaster Checks
                availableP = availableP.FindAll(p => !p.Died);
                CheckDisaster(availableP, i);

                availableP = availableP.FindAll(p => !p.Died && p.Partner == null);
                List<PersonData> availableActivePersons = FilterActivePersons(availableP, i);

                availableC.AddRange(CreateFamiliesForYear(availableActivePersons, i));

                availableC = ProcessCommunitiesForYear(availableC, availableP, i);

                _yearHouseMap[i] = new Dictionary<int, House>();
                if(i > startYear)
                {
                    _yearHouseMap[i] = _yearHouseMap[i - 1].ToDictionary(e => e.Key, e => e.Value);

                }

                CheckFlatHeritance(i);

                _allFlats.AddRange(AddHousesAndFlatsForYear(availableC, _earthAgeHelper.GetEarthAge(i), i));
            }
        }

        private List<PersonData> FilterActivePersons(List<PersonData> persons, int year)
        {
            return persons.FindAll(p => p.GetAge(year) > _MIN_MARRIAGE_AGE && p.GetAge(year) < _MAX_MARRIAGE_AGE);
        }

        private List<Community> CreateFamiliesForYear(List<PersonData> persons, int year)
        {
            var shuffledPersons = persons.OrderBy(x => _rand.Next()).ToList();

            var newCommunities = new List<Community>();
            for (int j = 0; j < shuffledPersons.Count / _rand.Next(1, 4); j += 2)
            {
                if (j + 1 < shuffledPersons.Count)
                {
                    var newComm = Community.CreateFamilyIfPossible(shuffledPersons[j], shuffledPersons[j + 1], _rand);
                    if (newComm != null)
                    {
                        _allCommunities.Add(newComm);
                        newCommunities.Add(newComm);
                    }
                }
            }
            return newCommunities;
        }

        private List<Community> ProcessCommunitiesForYear(List<Community> communities, List<PersonData> persons, int year)
        {
            List<Community> activeCommunities = new List<Community>();
            foreach (Community comm in communities.Where(x => x.CommunityActive(year) && !x.Calculated))
            {
                activeCommunities.Add(comm);

                var children = comm.CreateChildren(_rand, _nameGenerator, year, _earthAgeHelper);

                _allPersons.AddRange(children);
                persons.AddRange(children);
            }
            return activeCommunities;
        }

        private void CheckFlatHeritance(int year)
        {
            foreach (var flat in _allFlats.Where(f => f.House.DemolishedYear > year && f.House.BuildYear <= year))
            {
                if (flat.Owner != null && flat.Owner.Died)
                {
                    if (flat.Owner.Children != null && flat.Owner.Children.Count > 0)
                    {
                        PersonData newOwner = flat.Owner.Children[_rand.Next(0, flat.Owner.Children.Count)];
                        flat.UpdateOwner(newOwner);
                        newOwner.Family.Home = flat;
                    }
                    else
                    {
                        flat.UpdateOwner(null);
                    }
                }
            }
        }

        private List<Flat> AddHousesAndFlatsForYear(List<Community> communities, EarthAge age, int year)
        {
            List<Flat> returnList = new List<Flat>();
            var communitiesWithoutHouse = new Stack<Community>(communities.Where(c => c.Home == null));
            if(communitiesWithoutHouse.Count() == 0)
            {
                return returnList;
            }

            int flatNum = 0;
            List<Community> commsToRemoveFromList = new List<Community>();
            foreach(var flat in _allFlats.Where(f => f.House.DemolishedYear > year && f.House.BuildYear <= year && f.Owner == null))
            {
                if(communitiesWithoutHouse.Count > flatNum)
                {
                    Community comm = communitiesWithoutHouse.Pop();
                    flat.UpdateOwner(comm.Adults[_rand.Next(0, 2)]);
                    comm.Home = flat;
                    flatNum++;
                }
            }

            var newHouse = new House { BuildYear = year, Epoch = age.Name, Floors = _rand.Next(1, age.MaxHouseFloors) };
            newHouse.DemolishedYear = year + _rand.Next(100, 2000);
            _allHouses.Add(newHouse);
            int flatCount = age.Name.Equals("StoneAge") ? 1 : newHouse.Floors * 2;
            int flatsFilled = 0;
            int floor = 1;
            while (communitiesWithoutHouse.Count > 0)
            {
                if(flatsFilled == flatCount)
                {
                    newHouse = new House { BuildYear = year, Epoch = age.Name, Floors = _rand.Next(1, age.MaxHouseFloors) };
                    newHouse.DemolishedYear = year + _rand.Next(100, 2000);
                    _allHouses.Add(newHouse);
                    flatCount = age.Name.Equals("StoneAge") ? 1 : newHouse.Floors * 2;
                    flatsFilled = 0;
                    floor = 1;
                }

                Community comm = communitiesWithoutHouse.Pop();
                var newFlat = new Flat(newHouse, comm.Adults[_rand.Next(0, 2)], floor, flatsFilled);
                newHouse.Flats.Add(newFlat);
                flatsFilled++;
                comm.Home = newFlat;
                returnList.Add(newFlat);

                if(flatsFilled%2 > 0)
                {
                    floor++;
                }

            }
            return returnList;

        }

        public void AddDisaster(int year, int peopleDied, string name)
        {
            _disasters.Add(year, new Disaster { year=year, peopleDied=peopleDied, name=name });
        }

        private void CheckDisaster(List<PersonData> availablePersons, int year)
        {
            if (_disasters.ContainsKey(year))
            {
                var availablePersonsTemp = availablePersons.OrderBy(x => _rand.Next()).ToList();

                int count = _disasters[year].peopleDied;
                for(int i= 0; i < count; i++)
                {
                    if(i<availablePersonsTemp.Count)
                    {
                        availablePersonsTemp[i].DeathDate = new LongDateTime(year);
                        availablePersonsTemp[i].DeathReason = _disasters[year].name;
                    }
                }
            }
        }

        private List<PersonData> PeopleMovingIn(int year)
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

        public void RemovePersonAndAncestors(PersonData person, int deathYear = -1)
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
