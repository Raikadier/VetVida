using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public abstract class Persona : NameEntity
    {
        public int Cedula { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
    }
}
