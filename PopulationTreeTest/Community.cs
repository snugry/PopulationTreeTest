﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class Community
    {
        public PersonData[] Adults { get; set; }

        public string CommunityName { get; set; }

        public List<PersonData> Children { get; set; }

        public bool Calculated { get; set; } = false;

        public Community(PersonData person1, PersonData person2, Random rand)
        {
            Adults = new PersonData[] { person1, person2 };
            Children = new List<PersonData>();

            SetCommunityName(rand);
        }

        public Community(long year, Random rand, EarthAgeHelper earthAgeHelp, NameGenerator nameGen)
        {
            Adults = new PersonData[] { new PersonData(nameGen, year, rand, earthAgeHelp),
                new PersonData(nameGen, year, rand, earthAgeHelp) };

            SetCommunityName(rand);
            Children = new List<PersonData>();

            CreateChildren(rand, nameGen, rand.Next((int)year - 15, (int)year + 2), earthAgeHelp);

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
            Adults[0].Surname = CommunityName;
            Adults[1].Surname = CommunityName;
        }

        public List<PersonData> CreateChildren(Random rand, NameGenerator nameGen, long year, EarthAgeHelper earthAge)
        {
            Calculated = true;
            EarthAge age = earthAge.GetEarthAge(year);
            List<PersonData> children = new List<PersonData>();
            if(rand.NextDouble() <= age.ChildPropability)
            {
                return children;
            }
            int numChilds = rand.Next(0, 6);

            for (int i = 0; i < numChilds; i++)
            {
                PersonData p = new PersonData(nameGen, (Gender)(rand.Next(0, 1)), CommunityName);
                p.SetBirthDateRange(year, year + 60 - Adults[0].GetAge(year), rand);
                p.SetDeathDateRange(100, rand);
                p.SetJob(earthAge, rand);
                p.Family = this;
                p.Parents = Adults;

                children.Add(p);
            }

            Children.AddRange(children);
            foreach (PersonData parent in Adults)
            {
                parent.Children = Children;
            }
            return children;
        }

        public bool CommunityActive(long year)
        {
            return Adults.All(x => x.GetAge(year) > 15 && x.GetAge(year) < 60);
        }
    }
}
