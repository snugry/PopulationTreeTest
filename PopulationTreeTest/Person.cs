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

            BirthDate = new LongDateTime(year);
        }

        public void SetDeathDateRange(long maxAge, Random rand)
        {
            long year = rand.NextInt64(BirthDate.Year, BirthDate.Year + maxAge + 1);

            DeathDate = new LongDateTime(year);
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
    }
}
