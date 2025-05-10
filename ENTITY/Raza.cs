using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITY
{
    public class Raza : NameEntity
    {
        public Especie especie { get; set; }
        public List<Mascota> Mascotas { get; set; }
        public override string ToString()
        {
            return $"{Id};{Nombre};{especie.Id}";
        }
    }
}
