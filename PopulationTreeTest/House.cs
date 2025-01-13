using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class House
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public int Floors { get; set; }
        public List<Flat> Flats { get; set; } = new List<Flat>();
        public int BuildYear { get; set; }
        public int DemolishedYear { get; set; }

        public string Epoch { get; set; }

        public House()
        {

        }
    }
}
