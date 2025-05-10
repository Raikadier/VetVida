using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class EspecieService : ILogic<Especie>
    {
        private readonly EspecieRepository especieRepository;
        private readonly RazaService razaService;
        private List<Especie> especies;
        public EspecieService()
        {
            especieRepository = new EspecieRepository(Archivos.ARC_ESPECIE);
            razaService = new RazaService();
            especies = especieRepository.Read();
        }

        public ResultadoOperacion Save(Especie especie)
        {
            try
            {
                if (especie == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"La especie es nula"
                    };
                }
                if (GetById(especie.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Esta especie ya esta registrada\nId: {GetById(especie.Id).Id} | Nombre: {GetById(especie.Id).Nombre}"
                    };
                }
                if (especieRepository.Save(especie))
                {
                    especies.Add(especie);
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = $"La especie se guardo correctamente\nId: {especie.Id} | Nombre: {especie.Nombre}"
                    };
                }
                else
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Error al guardar la especie \nId: {especie.Id} | Nombre: {especie.Nombre}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"XD Error al guardar la especie \nId: {especie.Id}\n{ex.Message}"
                };
            }
        }
        public ResultadoOperacion Delete(int id)
        {
            var especie = GetById(id);
            if (especie != null)
            {
                string message = $"La especie se elimino correctamente\nId: {especie.Id} | Nombre: {especie.Nombre}";
                razaService.DeleteByEspecie(especie);
                especies.Remove(especie);
                especieRepository.SaveList(especies);
                return new ResultadoOperacion
                {
                    Exito = true,
                    Mensaje = message
                };
            }
            return new ResultadoOperacion
            {
                Exito = false,
                Mensaje = $"La especie no existe"
            };
        }

        public List<Especie> GetAll()
        {
            return especieRepository.Read();
        }

        public Especie GetById(int id)
        {
            var especie = BuscadorEntidad.ObtenerEspeciePorId(especies, id);
            return especie;
        }

        public ResultadoOperacion Update(Especie especie)
        {
            if (GetById(especie.Id) != null)
            {
                foreach (var e in especies)
                {
                    if (e.Id == especie.Id)
                    {
                        e.Id = especie.Id;
                        e.Nombre = especie.Nombre;
                    }
                }
                especieRepository.SaveList(especies);
                return new ResultadoOperacion()
                {
                    Exito = true,
                    Mensaje = $"La especie se actualizo correctamente"
                };
            }
            return new ResultadoOperacion()
            {
                Exito = false,
                Mensaje = $"La especie no se encontro"
            };
        }
    }
}
