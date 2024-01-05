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

        public Person[] Children { get; set; }

        public Community(Person person1, Person person2, Random rand)
        {
            Adults = new Person[] { person1, person2 };

            int nameSwitch = rand.Next(0, 2);
            if(nameSwitch == 0)
            {
                CommunityName = person1.Surname;
            }
            else if(nameSwitch == 1)
            {
                CommunityName = person2.Surname;
            }
            else 
            {
                if (person1.Surname.Contains('-'))
                {
                    CommunityName = person2.Surname;
                }
                else if (person2.Surname.Contains('-'))
                {
                    CommunityName = person1.Surname;
                }
                else
                {
                    CommunityName = person1.Surname + "-" + person2.Surname;
                }
            }
        }

        public List<Person> CreateChildren(Random rand, NameGenerator nameGen, int childRate = 2)
        {
            List<Person> children = new List<Person>();
            int numChilds = rand.Next(0, childRate + 1);

            long startYear = (long)(Adults.Average(x => x.BirthDate.Year) + 20);
            for (int i = 0; i < numChilds; i++)
            {
                Person p = new Person(nameGen, (Gender)(rand.Next(0, 1)), CommunityName);
                p.SetBirthDateRange(startYear, startYear + 30, rand);
                p.SetDeathDateRange(120, rand);
                p.Family = this;

                children.Add(p);
            }

            Children = children.ToArray();
            return children;
        }
    }
}
