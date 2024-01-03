using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class Person
    {
        public string Prename { get; private set; }
        public string Surname { get; private set; }

        public DateTime BirthDate { get; private set; }
        public DateTime DeathDate { get; private set; }
        public int Age { get; private set; }

        public Person[] Parents { get; private set; }
        public Person[] Children { get; private set; }
        public Person Partner { get; private set; }

        public string Job { get; set; }

        public Person(NameGenerator nameGenerator, bool female) {
            if (female)
            {
                Prename = nameGenerator.GeneratePrenameW();
            }
            else
            {
                Prename = nameGenerator.GeneratePrenameM();
            }
            
            Surname = nameGenerator.GenerateSurname();
        }

        public Person(NameGenerator nameGenerator, bool female, string surname)
        {
            if (female)
            {
                Prename = nameGenerator.GeneratePrenameW();
            }
            else
            {
                Prename = nameGenerator.GeneratePrenameM();
            }
            Surname = surname;
        }
    }
}
