using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Veterinario : Persona
    {
        public string Especialidad { get; set; }
        public override string ToString()
        {
            return $"{Id};{Nombre};{Apellido};{Cedula};{Telefono};{Especialidad}";
        }
    }
}
