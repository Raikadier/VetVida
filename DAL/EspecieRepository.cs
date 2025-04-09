using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class EspecieRepository : FileRepository<Especie>
    {
        public EspecieRepository(string file) : base(file) { }
        public override List<Especie> Read()
        {
            try
            {
                List<Especie> especies = new List<Especie>();
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    especies.Add(MappingType(reader.ReadLine()));
                }
                return especies;
            }
            catch (Exception)
            {
                return new List<Especie>();
            }
        }
        public override Especie MappingType(string datos)
        {
            Especie especie = new Especie();
            especie.Id = int.Parse(datos.Split(';')[0]);
            especie.Nombre = datos.Split(';')[1];
            return especie;
        }
    }
}
