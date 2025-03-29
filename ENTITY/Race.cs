using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public abstract class Race
    {
        public Spicies spicies;
        public List<Pet> Pets = new List<Pet>();

        public Race() { }
    }
}
