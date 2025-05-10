using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using DAL;
namespace BLL
{
    public class MascotaService : LogicBase<Mascota>
    {
        public MascotaService(FileRepository<Mascota> repositorio) : base(repositorio)
        {
        }
        public List<Mascota> BuscarPorPropietario(int idPropietario)
        {
            List<Mascota> mascotas = repositorio.Read();
            return mascotas.Where(m => m.Propietario.Id == idPropietario).ToList();
        }
    }
}
