using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public abstract class Spicies
    {
        public List<Race> Races = new List<Race>();
        protected Spicies() { }
    }
}
