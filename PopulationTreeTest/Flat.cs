using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationTreeTest
{
    public class Flat
    {
        public int Id { get; set; }
        public int FlatNumber {  get; set; }

        public int Floor { get; set; }
        public House House { get; private set; }

        public Community? OwnerCommunity { get; set; }
        public PersonData? Owner { get; private set; }

        public Flat(House house,PersonData owner, int floor, int flatNumber)
        {
            House = house;
            this.Owner = owner;
            this.OwnerCommunity = owner.Family;
            Floor = floor;
            FlatNumber = flatNumber;
        }

        public void UpdateOwner ( PersonData? newOwner)
        {
            Owner = newOwner;
            if(newOwner == null)
            {
                OwnerCommunity = null;
            }
            else
            {
                OwnerCommunity = newOwner.Family;
            }
        }
    }
}
