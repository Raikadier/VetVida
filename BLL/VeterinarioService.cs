using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class VeterinarioService : ILogic<Veterinario>
    {
        private readonly VeterinarioRepository veterinarioRepository;
        private List<Veterinario> veterinarios;
        public VeterinarioService()
        {
            veterinarioRepository = new VeterinarioRepository(Archivos.ARC_VETERINARIO);
            veterinarios = veterinarioRepository.Read();
        }

        public ResultadoOperacion Save(Veterinario veterinario)
        {
            try
            {
                if(veterinario == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"El veterinario es nulo"
                    };
                }

                if (GetById(veterinario.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"El veterinario ya existe\nId: {GetById(veterinario.Id).Id} | Nombre: {GetById(veterinario.Id).Nombre}"
                    };
                }
                if (veterinarioRepository.Save(veterinario))
                {
                    veterinarios.Add(veterinario);
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = $"El veterinario se guardo correctamente\nId: {veterinario.Id} | Nombre: {veterinario.Nombre}"
                    };
                }
                else
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Error al guardar el veterinario\nId: {veterinario.Id} | Nombre: {veterinario.Nombre}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al guardar el veterinario\nId: {veterinario.Id} | Nombre: {veterinario.Nombre}\n{ex.Message}"
                };
            }
        }
        public ResultadoOperacion Delete(int id)
        {
            var vet = GetById(id);
            if (vet != null)
            {
                string message = $"El veterinario se elimino correctamente\nId: {vet.Id} | Nombre: {vet.Nombre}";
                veterinarios.Remove(vet);
                veterinarioRepository.SaveList(veterinarios);
                return new ResultadoOperacion
                {
                    Exito = true,
                    Mensaje = message
                };
            }
            return new ResultadoOperacion
            {
                Exito = false,
                Mensaje = $"El veterinario no existe"
            };
        }

        public List<Veterinario> GetAll()
        {
            return veterinarioRepository.Read();
        }

        public Veterinario GetById(int id)
        {
            var veterinario=BuscadorEntidad.ObtenerVeterinarioPorId(veterinarios, id);
            //var x = veterinarios.FirstOrDefault<Veterinario>(v => v.Id == id);
            return veterinario;
        }

        public ResultadoOperacion Update(Veterinario veterinario)
        {
            if (veterinario == null)
            {
                return new ResultadoOperacion()
                {
                    Exito = false,
                    Mensaje = $"El veterinario es nulo"
                };
            }
            if (GetById(veterinario.Id) != null)
            {
                foreach (var vet in veterinarios)
                {
                    if (vet.Id == veterinario.Id)
                    {
                        vet.Id = veterinario.Id;
                        vet.Nombre = veterinario.Nombre;
                        vet.Cedula = veterinario.Cedula;
                        vet.Apellido = veterinario.Apellido;
                        vet.Telefono = veterinario.Telefono;
                        vet.Especialidad = veterinario.Especialidad;

                    }
                }
                veterinarioRepository.SaveList(veterinarios);
                return new ResultadoOperacion()
                {
                    Exito = true,
                    Mensaje = $"El veterinario se actualizo correctamente"
                };
            }
            return new ResultadoOperacion()
            {
                Exito = false,
                Mensaje = $"El veterinario no se encontro"
            };
        }
    }
}
