using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class PropietarioService : LogicBase<Propietario>
    {
        private readonly FileRepository<Propietario> repositorio;
        private PropietarioRepository propietarioRepository;
        private List<Propietario> propietarios;
        public PropietarioService(FileRepository<Propietario> repositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
            propietarioRepository = new PropietarioRepository("Propietario.txt");
            propietarios = repositorio.Read();
        }

        public override ResultadoOperacion Save(Propietario propietario)
        {
            try
            {
                if (GetById(propietario.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"El propietario ya existe\nId: {GetById(propietario.Id).Id} | Nombre: {GetById(propietario.Id).Nombre}"
                    };
                }
                if (repositorio.Save(propietario))
                {
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = $"El veterinario se guardo correctamente\nId: {propietario.Id} | Nombre: {propietario.Nombre}"
                    };
                }
                else
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Error al guardar el veterinario\nId: {propietario.Id} | Nombre: {propietario.Nombre}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = string.Empty
                };
            }
        }
        public override ResultadoOperacion Delete(int id)
        {
            var propietario = GetById(id);
            if (propietario != null)
            {
                string message = $"El propietario se elimino correctamente\nId: {propietario.Id} | Nombre: {propietario.Nombre}";
                propietarios.Remove(propietario);
                repositorio.SaveList(propietarios);
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

        public override List<Propietario> GetAll()
        {
            return propietarios;
        }

        public override Propietario GetById(int id)
        {
            var propietario = BuscadorEntidad.ObtenerPropietarioPorId(propietarios, id);
            return propietario;
        }

        public override ResultadoOperacion Update(Propietario propietario)
        {
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
                repositorio.SaveList(propietarios);
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
