using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;

namespace DAL
{
    public class RazaRepository : FileRepository<Raza>
    {
        private EspecieRepository especieRepository;

        public RazaRepository(string ruta) : base(ruta)
        {
            especieRepository = new EspecieRepository(Archivos.ARC_ESPECIE);
        }

        public override List<Raza> Consultar()
        {
            try
            {
                List<Raza> lista = new List<Raza>();

                if (File.Exists(ruta))
                {
                    StreamReader sr = new StreamReader(ruta);
                    while (!sr.EndOfStream)
                    {
                        lista.Add(Mappear(sr.ReadLine()));
                    }
                    sr.Close();
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public  List<RazaDto> ConsultarDTO()
        {
            try
            {
                List<RazaDto> lista = new List<RazaDto>();

                if (File.Exists(ruta))
                {
                    StreamReader sr = new StreamReader(ruta);
                    while (!sr.EndOfStream)
                    {
                        lista.Add(Mappear2(sr.ReadLine()));
                    }
                    sr.Close();
                }
                return lista;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private RazaDto Mappear2(string datos)
        {
            string[] campos = datos.Split(';');
            RazaDto raza = new RazaDto();
            raza.Codigo = (campos[0]);
            raza.Nombre_Raza = campos[1];
            var id = int.Parse(campos[2]);
            raza.Especie= especieRepository.Consultar().FirstOrDefault(e => e.Id ==id).Nombre;
           
            return raza;
        }

        public override Raza Mappear(string datos)
        {
            string[] campos = datos.Split(';');
            Raza raza = new Raza();
            raza.Id = int.Parse(campos[0]);
            raza.Nombre = campos[1];

            int especieId = int.Parse(campos[2]);
            Especie especie = especieRepository.Consultar().FirstOrDefault(e => e.Id == especieId);
            if (especie != null)
            {
                raza.AsignarEspecie(especie);
            }

            return raza;
        }

        public List<Raza> ConsultarPorEspecie(int especieId)
        {
            return Consultar().Where(r => r.Especie.Id == especieId).ToList();
        }
    }

}
