using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class RazaService : ILogic<Raza> , IListSearchForEntity<Raza>
    {
        private readonly RazaRepository razaRepository;
        private readonly MascotaService mascotaService;
        private List<Raza> razas;
        public RazaService()
        {
            razaRepository = new RazaRepository(Archivos.ARC_RAZA);
            mascotaService = new MascotaService();
            razas = razaRepository.Read();
        }
        public List<Raza> SearchForEntity(int op,int idEspecie)
        {
            return razas.Where(r => r.especie.Id == idEspecie).ToList();
        }

        public ResultadoOperacion Save(Raza raza)
        {
            try
            {
                if (raza == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"La raza es nula"
                    };
                }
                if (GetById(raza.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Esta raza ya esta registrada\nId: {GetById(raza.Id).Id} | Nombre: {GetById(raza.Id).Nombre}"
                    };
                }
                if (razaRepository.Save(raza))
                {
                    razas.Add(raza);
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = $"La raza se guardo correctamente\nId: {raza.Id} | Nombre: {raza.Nombre}"
                    };
                }
                else
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Error al guardar la raza \nId: {raza.Id} | Nombre: {raza.Nombre}"
                    };
                }
            }
            catch (Exception)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al guardar la raza \nId: {raza.Id} | Nombre: {raza.Nombre}"
                };
            }
        }
        public ResultadoOperacion Delete(int id)
        {
            var raza = GetById(id);
            if (raza != null)
            {
                mascotaService.DeleteByRaza(raza.Id);
                string message = $"La raza se elimino correctamente\nId: {raza.Id} | Nombre: {raza.Nombre}";
                razas.Remove(raza);
                razaRepository.SaveList(razas);
                return new ResultadoOperacion
                {
                    Exito = true,
                    Mensaje = message
                };
            }
            return new ResultadoOperacion
            {
                Exito = false,
                Mensaje = $"La raza no existe"
            };
        }

        public List<Raza> GetAll()
        {
            return razaRepository.Read();
        }

        public Raza GetById(int id)
        {
            var raza = BuscadorEntidad.ObtenerRazaPorId(razas, id);
            return raza;
        }

        public ResultadoOperacion Update(Raza raza)
        {
            if (raza == null)
            {
                return new ResultadoOperacion()
                {
                    Exito = false,
                    Mensaje = $"La raza es nula"
                };
            }
            if (GetById(raza.Id) != null)
            {
                foreach (var r in razas)
                {
                    if (r.Id == raza.Id)
                    {
                        r.Id = raza.Id;
                        r.Nombre = raza.Nombre;
                        r.especie=raza.especie;
                    }
                }
                razaRepository.SaveList(razas);
                return new ResultadoOperacion()
                {
                    Exito = true,
                    Mensaje = $"La raza se actualizo correctamente"
                };
            }
            return new ResultadoOperacion()
            {
                Exito = false,
                Mensaje = $"La raza no se encontro"
            };
        }
        public ResultadoOperacion DeleteByEspecie(Especie especie)
        {
            razas = razaRepository.Read();
            var razasToDelete = razas.Where(r => r.especie.Id == especie.Id).ToList();
            foreach (var raza in razasToDelete)
            {
                mascotaService.DeleteByRaza(raza.Id);
            }
            razas.RemoveAll(r => r.especie.Id == especie.Id);
            razaRepository.SaveList(razas);
            return new ResultadoOperacion()
            {
                Exito = true,
                Mensaje = string.Empty
            };
        }
    }
}
