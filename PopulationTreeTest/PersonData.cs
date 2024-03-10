using System;
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

    public class PersonData
    {
        public string Prename { get; private set; }
        public string Surname { get; set; }

        public LongDateTime BirthDate { get; private set; }
        public LongDateTime DeathDate { get; private set; }

        public Community Family { get; set; }
        public PersonData Partner { get; set; }

        public string Job { get; set; }

        public int ChaosLevel { get; private set; }

        public bool Died { get; private set; } = false;

        public Gender Gender { get; private set; }

        public PersonData(NameGenerator nameGenerator, Gender gender) {
            Prename = nameGenerator.GeneratePrename(gender);
            Gender = gender;
            Surname = nameGenerator.GenerateSurname();
        }

        public PersonData(NameGenerator nameGenerator, Gender gender, string surname)
        {
            Prename = nameGenerator.GeneratePrename(gender);
            Gender = gender;
            Surname = surname;
        }

        public PersonData(NameGenerator nameGenerator, long year, Random rand, EarthAgeHelper earthAgeHelper)
        {
            Gender = (Gender)(rand.Next(0, 2));
            Prename = nameGenerator.GeneratePrename(Gender);
            Surname = nameGenerator.GenerateSurname();

            SetBirthDateRange(year - 30, year - 15, rand);
            SetDeathDateRange(15 + 100, rand);
            SetJob(earthAgeHelper, rand);
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
            var tempJob = possibleJobs[rand.Next(0, possibleJobs.Count)];
            while((float)rand.NextDouble() > tempJob.Propability)
            {
                tempJob = possibleJobs[rand.Next(0, possibleJobs.Count)];
            }
            this.Job = tempJob.Name;
        }
    }
}
