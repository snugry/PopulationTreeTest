using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class Community
    {
        public Person[] Adults { get; set; }

        public string CommunityName { get; set; }

        public List<Person> Children { get; set; }

        public bool Calculated { get; set; } = false;

        public Community(Person person1, Person person2, Random rand)
        {
            Adults = new Person[] { person1, person2 };
            Children = new List<Person>();

            SetCommunityName(rand);
        }

        public Community(long year, Random rand, EarthAgeHelper earthAgeHelp, NameGenerator nameGen)
        {
            Adults = [ new Person(nameGen, year, rand, earthAgeHelp),
                new Person(nameGen, year, rand, earthAgeHelp) ];

            SetCommunityName(rand);
            Children = new List<Person>();
            for (int i = 0; i< rand.Next(0,2); i++)
            {
                CreateChildren(rand, nameGen, rand.Next((int)year - 15, (int)year + 2), earthAgeHelp);
            }
        }

        private void SetCommunityName(Random rand)
        {
            int nameSwitch = rand.Next(0, 3);
            if (nameSwitch == 0)
            {
                CommunityName = Adults[0].Surname;
            }
            else if (nameSwitch == 1)
            {
                CommunityName = Adults[1].Surname;
            }
            else
            {
                if (Adults[0].Surname.Contains('-'))
                {
                    CommunityName = Adults[1].Surname;
                }
                else if (Adults[1].Surname.Contains('-'))
                {
                    CommunityName = Adults[0].Surname;
                }
                else
                {
                    CommunityName = Adults[0].Surname + "-" + Adults[1].Surname;
                }
            }
        }

        public List<Person> CreateChildren(Random rand, NameGenerator nameGen, long year, EarthAgeHelper earthAge)
        {
            EarthAge age = earthAge.GetEarthAge(year);
            List<Person> children = new List<Person>();
            if(rand.NextDouble() > age.ChildPropability)
            {
                return children;
            }
            int numChilds = rand.Next(0, 3);

            for (int i = 0; i < numChilds; i++)
            {
                Person p = new Person(nameGen, (Gender)(rand.Next(0, 1)), CommunityName);
                p.SetBirthDate(year, rand);
                p.SetDeathDateRange(100, rand);
                p.SetJob(earthAge, rand);
                p.Family = this;

                children.Add(p);
            }

            Children.AddRange(children);
            return children;
        }

        public bool CommunityActive(long year)
        {
            List<Person> allMembers = new List<Person>();
            allMembers.AddRange(Adults);
            allMembers.AddRange(Children);

            Calculated = Adults[0].GetAge(year) < 50 && Adults[1].GetAge(year) < 50 && 
                allMembers.Any(x => x.BirthDate.Year < year && x.DeathDate.Year > year);
            return Calculated;
        }
    }
}
