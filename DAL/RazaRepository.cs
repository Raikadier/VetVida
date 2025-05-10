using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RazaRepository : FileRepository<Raza>
    {
        public RazaRepository(string file) : base(file) { }
        public override List<Raza> Read()
        {
            try
            {
                List<Raza> razas = new List<Raza>();
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    razas.Add(MappingType(reader.ReadLine()));
                }
                return razas;
            }
            catch (Exception)
            {
                return new List<Raza>();
            }
        }
        public override Raza MappingType(string datos)
        {
            Raza raza = new Raza();
            raza.Id= int.Parse(datos.Split(';')[0]);
            raza.Nombre = datos.Split(';')[1];
            List<Especie> especies = new EspecieRepository("Especie.txt").Read();
            raza.especie = BuscadorEntidad.ObtenerEspeciePorId(especies, int.Parse(datos.Split(';')[2]));
            return raza;
        }

    }
}
