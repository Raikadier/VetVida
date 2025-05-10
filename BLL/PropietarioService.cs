using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class PropietarioService : ILogic<Propietario>
    {
        private readonly MascotaService mascotaService;
        private readonly PropietarioRepository propietarioRepository;
        private List<Propietario> propietarios;
        public PropietarioService()
        {
            propietarioRepository = new PropietarioRepository(Archivos.ARC_PROPIETARIO);
            mascotaService = new MascotaService();
            propietarios = propietarioRepository.Read();
        }

        public ResultadoOperacion Save(Propietario propietario)
        {
            try
            {
                if (propietario == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"El propietario es nulo"
                    };
                }
                if (GetById(propietario.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"El propietario ya existe\nId: {GetById(propietario.Id).Id} | Nombre: {GetById(propietario.Id).Nombre}"
                    };
                }
                if (propietarioRepository.Save(propietario))
                {
                    propietarios.Add(propietario);
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = $"El propietario se guardo correctamente\nId: {propietario.Id} | Nombre: {propietario.Nombre}"
                    };
                }
                else
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Error al guardar el propietario\nId: {propietario.Id} | Nombre: {propietario.Nombre}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al guardar el propietario\nId: {propietario.Id} | Nombre: {propietario.Nombre}\n{ex.Message}"
                };
            }
        }
        public ResultadoOperacion Delete(int id)
        {
            var propietario = GetById(id);
            if (propietario != null)
            {
                string message = $"El propietario se elimino correctamente\nId: {propietario.Id} | Nombre: {propietario.Nombre}";
                mascotaService.DeleteByPropietary(id);
                propietarios.Remove(propietario);
                propietarioRepository.SaveList(propietarios);
                return new ResultadoOperacion
                {
                    Exito = true,
                    Mensaje = message
                };
            }
            return new ResultadoOperacion
            {
                Exito = false,
                Mensaje = $"El propietario no existe"
            };
        }

        public List<Propietario> GetAll()
        {
            return propietarioRepository.Read();
        }

        public Propietario GetById(int id)
        {
            var propietario = BuscadorEntidad.ObtenerPropietarioPorId(propietarios, id);
            return propietario;
        }

        public ResultadoOperacion Update(Propietario propietario)
        {
            if (propietario == null)
            {
                return new ResultadoOperacion()
                {
                    Exito = false,
                    Mensaje = $"El propietario es nulo"
                };
            }
            if (GetById(propietario.Id) != null)
            {
                foreach (var prop in propietarios)
                {
                    if (prop.Id == propietario.Id)
                    {
                        prop.Id = propietario.Id;
                        prop.Nombre = propietario.Nombre;
                        prop.Cedula = propietario.Cedula;
                        prop.Apellido = propietario.Apellido;
                        prop.Telefono = propietario.Telefono;
                    }
                }
                propietarioRepository.SaveList(propietarios);
                return new ResultadoOperacion()
                {
                    Exito = true,
                    Mensaje = $"El propietario se actualizo correctamente"
                };
            }
            return new ResultadoOperacion()
            {
                Exito = false,
                Mensaje = $"El propietario no se encontro"
            };
        }
    }
}
