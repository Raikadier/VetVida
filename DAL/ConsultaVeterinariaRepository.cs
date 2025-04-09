using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class ConsultaVeterinariaRepository : FileRepository<ConsultaVeterinaria>, IGetById<ConsultaVeterinaria>
    {
        public ConsultaVeterinariaRepository(string file) : base(file) { }
        public override List<ConsultaVeterinaria> Read()
        {
            try
            {
                List<ConsultaVeterinaria> consultas = new List<ConsultaVeterinaria>();
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    consultas.Add(MappingType(reader.ReadLine()));
                }
                reader.Close();
                return consultas;
            }
            catch (Exception)
            {
                return new List<ConsultaVeterinaria>();
            }
        }
        public override ConsultaVeterinaria MappingType(string datos)
        {
            ConsultaVeterinaria consulta = new ConsultaVeterinaria();
            consulta.Id = int.Parse(datos.Split(';')[0]);
            consulta.Fecha = DateTime.Parse(datos.Split(';')[1]);
            List<Veterinario> veterinarios = new VeterinarioRepository("Veterinario.txt").Read();
            consulta.Veterinario= BuscadorEntidad.ObtenerVeterinarioPorId(veterinarios,int.Parse(datos.Split(';')[2]));
            List<Mascota> mascotas = new MascotaRepository("Mascota.txt").Read();
            consulta.Mascota = BuscadorEntidad.ObtenerMascotaPorId(mascotas, int.Parse(datos.Split(';')[3]));
            consulta.Diagnostico = datos.Split(';')[4];
            consulta.Tratamiento = datos.Split(';')[5];
            return consulta;
        }
        public ConsultaVeterinaria GetById(int id)
        {
            List<ConsultaVeterinaria> consultas = Read();
            return consultas.FirstOrDefault(p => p.Id == id);
        }
    }
}
