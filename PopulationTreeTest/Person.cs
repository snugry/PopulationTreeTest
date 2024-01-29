﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public enum Gender
    {
        male,
        female
    }

    public class Person
    {
        public string Prename { get; private set; }
        public string Surname { get; private set; }

        public LongDateTime BirthDate { get; private set; }
        public LongDateTime DeathDate { get; private set; }

        public Community Family { get; set; }
        public Person Partner { get; set; }

        public string Job { get; set; }

        public int ChaosLevel { get; private set; }

        public bool Died { get; private set; } = false;

        public Gender Gender { get; private set; }

        public Person(NameGenerator nameGenerator, Gender gender) {
            if (gender == Gender.female)
            {
                Prename = nameGenerator.GeneratePrenameW();
            }
            else
            {
                Prename = nameGenerator.GeneratePrenameM();
            }
            Gender = gender;
            
            Surname = nameGenerator.GenerateSurname();
        }

        public Person(NameGenerator nameGenerator, Gender gender, string surname)
        {
            if (gender == Gender.female)
            {
                Prename = nameGenerator.GeneratePrenameW();
            }
            else
            {
                Prename = nameGenerator.GeneratePrenameM();
            }
            Gender = gender;
            Surname = surname;
        }

        public void SetBirthDateRange(long yearFrom, long yearTo, Random rand)
        {
            long year = rand.NextInt64(yearFrom, yearTo);
            SetBirthDate(year, rand);
        }

        public void SetBirthDate(long year, Random rand)
        {
            int month = rand.Next(1, 13);
            int day = rand.Next(1, LongDateTime.GetDaysInMonth(month, year) + 1);

            BirthDate = new LongDateTime(year, month, day);
        }

        public void SetDeathDateRange(long maxAge, Random rand)
        {
            int ageSwitch = rand.Next(0,3);

            int yearRange = 20;
            if(maxAge < 10)
            {
                yearRange = (int)maxAge;
            }
            long year;
            if(ageSwitch == 0)
            {
                year = rand.NextInt64(BirthDate.Year, BirthDate.Year + maxAge + 1);
            }
            else
            {            
                year = rand.NextInt64(BirthDate.Year + maxAge - yearRange, BirthDate.Year + maxAge + 1);
            }
            int month = rand.Next(1, 13);
            int day = rand.Next(1, LongDateTime.GetDaysInMonth(month, year) + 1);

            DeathDate = new LongDateTime(year, month, day);
        }

        public int GetAge(long year)
        {
            int age = (int)(year - BirthDate.Year);
            if(year > DeathDate.Year)
            {
                Died = true;
            }
            return age;
        }

        public void SetJob(EarthAgeHelper earthAgeHelper, Random rand)
        {
            var possibleJobs = earthAgeHelper.GetEarthAge(this.BirthDate.Year).PossibleJobs;
            this.Job = possibleJobs[rand.Next(0, possibleJobs.Count)];
        }
    }
}
