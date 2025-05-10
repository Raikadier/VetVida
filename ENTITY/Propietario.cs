using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Propietario : Persona
    {
        public List<Mascota> Mascotas { get; set; } = new List<Mascota>();
        public override string ToString()
        {
            return $"{Id};{Nombre};{Apellido};{Cedula};{Telefono}";
        }
    }
}
