using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Especie : NameEntity
    {
        public List<Raza> Raza { get; set; } = new List<Raza>();

        public override string ToString()
        {
            return $"{Id};{Nombre}";
        }
    }
}
