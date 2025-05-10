using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
using DAL;
namespace BLL
{
    public class MascotaService : ILogic<Mascota>, IListSearchForEntity<Mascota>
    {
        private readonly MascotaRepository mascotaRepository;
        private readonly ConsultaVeterinariaService consultaVeterinariaService;
        private List<Mascota> mascotas;
        public MascotaService()
        {
            mascotaRepository = new MascotaRepository(Archivos.ARC_MASCOTA);
            mascotas = mascotaRepository.Read();
            consultaVeterinariaService = new ConsultaVeterinariaService();
        }
        public List<Mascota> SearchForEntity(int op,int id)
        {
            if(op == 1)
            {
                return mascotas.Where(m => m.Propietario.Id == id).ToList();
            }
            else if (op == 2)
            {
                return mascotas.Where(m => m.Raza.Id != id).ToList();
            }
            else
            {
                return new List<Mascota>();
            }
        }

        public ResultadoOperacion Delete(int id)
        {
            try
            {
                var mascota = GetById(id);
                if (mascota != null)
                {
                    string message = $"La mascota se elimino correctamente\nId: {mascota.Id} | Nombre: {mascota.Nombre}";
                    consultaVeterinariaService.DeleteByMascota(mascota.Id);
                    mascotas.Remove(mascota);
                    mascotaRepository.SaveList(mascotas);
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = message
                    };
                }
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"La mascota no existe"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al eliminar la mascota\nId: {id}\n{ex.Message}"
                };
            }
        }

        public List<Mascota> GetAll()
        {
            try
            {
                return mascotaRepository.Read();
            }
            catch (Exception)
            {
                return new List<Mascota>();
            }
        }
        public ResultadoOperacion DeleteByPropietary(int id)
        {
            try
            {
                var mascotas = mascotaRepository.Read();
                var filtradas = mascotas.Where(m => m.Propietario.Id != id).ToList();
                mascotaRepository.SaveList(filtradas);
                return new ResultadoOperacion{
                    Exito = true,
                    Mensaje = $"Se eliminaron las mascotas del propietario con id {id}"
                };
            }
            catch
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al eliminar las mascotas del propietario con id {id}"
                };
            }
        }
        public ResultadoOperacion DeleteByRaza(int id)
        {
            try
            {
                var mascotas = mascotaRepository.Read();
                var filtradas = mascotas.Where(m => m.Raza.Id != id).ToList();
                mascotaRepository.SaveList(filtradas);
                return new ResultadoOperacion
                {
                    Exito = true,
                    Mensaje = $"Se eliminaron las mascotas de la raza con id {id}"
                };
            }
            catch
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al eliminar las mascotas de la raza con id {id}"
                };
            }
        }
        public Mascota GetById(int id)
        {
            var mascota = BuscadorEntidad.ObtenerMascotaPorId(mascotas, id);
            return mascota;
        }

        public ResultadoOperacion Save(Mascota mascota)
        {
            try
            {
                if (mascota == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"La mascota es nula"
                    };
                }
                if (GetById(mascota.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"La mascota ya existe\nId: {GetById(mascota.Id).Id} | Nombre: {GetById(mascota.Id).Nombre}"
                    };
                }
                if (mascotaRepository.Save(mascota))
                {
                    mascotas.Add(mascota);
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = $"La mascota se guardo correctamente\nId: {mascota.Id} | Nombre: {mascota.Nombre}"
                    };
                }
                else
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Error al guardar la mascota\nId: {mascota.Id} | Nombre: {mascota.Nombre}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al guardar la mascota\nId: {mascota.Id}\n{ex.Message}"
                };
            }
        }

        public ResultadoOperacion Update(Mascota mascota)
        {
            try
            {
                if (mascota == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"La mascota es nula"
                    };
                }
                if (GetById(mascota.Id) != null)
                {
                    foreach (var masc in mascotas)
                    {
                        if (masc.Id == mascota.Id)
                        {
                            masc.Id = mascota.Id;
                            masc.Nombre = mascota.Nombre;
                            masc.Edad = mascota.Edad;
                            masc.Raza = mascota.Raza;
                            masc.Propietario = mascota.Propietario;
                        }
                    }
                    mascotaRepository.SaveList(mascotas);
                    return new ResultadoOperacion()
                    {
                        Exito = true,
                        Mensaje = $"La mascota se actualizo correctamente"
                    };
                }
                return new ResultadoOperacion()
                {
                    Exito = false,
                    Mensaje = $"La mascota no se encontro"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al actualizar la mascota\nId: {mascota.Id}\n{ex.Message}"
                };
            }
        }
    }
}
