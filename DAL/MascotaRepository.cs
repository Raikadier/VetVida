using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class MascotaRepository : FileRepository<Mascota>, IGetById<Mascota>
    {
        PropietarioRepository propietarioRepository = new PropietarioRepository("Propietario.txt");
        public MascotaRepository(string file) : base(file) { }
        public override List<Mascota> Read()
        {
            try
            {
                List<Mascota> mascotas = new List<Mascota>();
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    mascotas.Add(MappingType(reader.ReadLine()));
                }
                reader.Close();
                return mascotas;
            }
            catch (Exception)
            {
                return new List<Mascota>();
            }
        }
        public override Mascota MappingType(string datos)
        {
            Mascota mascota = new Mascota();
            mascota.Id = int.Parse(datos.Split(';')[0]);
            mascota.Nombre = datos.Split(';')[1];
            mascota.Raza.Id = int.Parse(datos.Split(';')[2]);
            mascota.Edad = int.Parse(datos.Split(';')[3]);
            List<Propietario> propietarios = propietarioRepository.Read();
            mascota.Propietario=BuscadorEntidad.ObtenerPropietarioPorId(propietarios, int.Parse(datos.Split(';')[4]));
            return mascota;
        }
        public Mascota GetById(int id)
        {
            List<Mascota> Mascotas = Read();
            return Mascotas.FirstOrDefault(p => p.Id == id);
        }
        
    }
}
