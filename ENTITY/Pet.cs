using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Pet
    {
        public Race Race { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Appointment> Appointments = new List<Appointment>();
        public PetOwner Owner { get; set; }

        public Pet() { }

        public Pet(Race race, int id, string name)
        {
            Race = race;
            Id = id;
            Name = name;
        }
    }
}
