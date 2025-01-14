﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PopulationTreeTest.FileIO;

namespace PopulationTreeTest
{
    public class NameGenerator
    {
        private string[] _prenamesW;
        private string[] _prenamesM;
        private string[] _surnames;

        private Random _random;

        private const string PRENAME_W_FILE = "prenames_w.csv";

        private const string PRENAME_M_FILE = "prenames_m.csv";

        private const string SURNAME_FILE = "surnames.csv";

        public NameGenerator(Random? random)
        {
            string folder = @"csv";
            CsvReader csvReader = new CsvReader(Path.Combine(folder,PRENAME_W_FILE));
            _prenamesW = csvReader.ReadCsvValues(1, true);
            csvReader = new CsvReader(Path.Combine(folder, PRENAME_M_FILE));

            _prenamesM = csvReader.ReadCsvValues(1, true);

            csvReader = new CsvReader(Path.Combine(folder, SURNAME_FILE));
            _surnames = csvReader.ReadCsvValues(0, true);

            if(random == null)
            {
                _random = new Random();
            }
            else
            {
                _random = random;
            }
            
        }

        public string GeneratePrename()
        {
            Gender gender = (Gender)(_random.Next(0, 2));
            return GeneratePrename(gender);
        }

        public string GeneratePrename(Gender gender)
        {
            if(gender == Gender.female)
            {
                return GeneratePrenameW();
            }
            return GeneratePrenameM();
        }

        private string GeneratePrenameW()
        {
            int index = _random.Next(0, _prenamesW.Length);
            return _prenamesW[index];
        }

        private string GeneratePrenameM()
        {
            int index = _random.Next(0, _prenamesM.Length);
            return _prenamesM[index];
        }

        public string GenerateSurname()
        {
            int index = _random.Next(0, _surnames.Length);
            return _surnames[index];
        }
    }
}
