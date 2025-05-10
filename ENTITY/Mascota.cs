using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Mascota : NameEntity
    {
        public Raza Raza { get; set; }
        public int Edad { get; set; }
        public Propietario Propietario { get; set; }
        public List<ConsultaVeterinaria> Consultas { get; set; } = new List<ConsultaVeterinaria>();
        public override string ToString()
        {
            return $"{Id};{Nombre};{Raza.Id};{Edad};{Propietario.Id}";
        }
    }
}
