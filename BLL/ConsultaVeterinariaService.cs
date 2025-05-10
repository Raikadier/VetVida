using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class ConsultaVeterinariaService : LogicBase<ConsultaVeterinaria>
    {
        public ConsultaVeterinariaService(FileRepository<ConsultaVeterinaria> repositorio) : base(repositorio)
        {
        }
        public List<ConsultaVeterinaria> ObtenerConsultasPorMascota(Mascota mascota)
        {
            List<ConsultaVeterinaria> consultas = repositorio.Read();
            return consultas.Where(c => c.Mascota.Id == mascota.Id).ToList();
        }
    }
}
