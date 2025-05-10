using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class VeterinarioService : LogicBase<Veterinario>
    {
        private readonly FileRepository<Veterinario> repositorio;
        private VeterinarioRepository veterinarioRepository;
        private List<Veterinario> veterinarios;
        public VeterinarioService(FileRepository<Veterinario> repositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
            veterinarioRepository = new VeterinarioRepository("Veterinario.txt");
            veterinarios = repositorio.Read();
        }

        public override ResultadoOperacion Save(Veterinario veterinario)
        {
            try
            {
                if (GetById(veterinario.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"El veterinario ya existe\nId: {GetById(veterinario.Id).Id} | Nombre: {GetById(veterinario.Id).Nombre}"
                    };
                }
                if (repositorio.Save(veterinario))
                {
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
                    Mensaje = string.Empty
                };
            }
        }
        public override ResultadoOperacion Delete(int id)
        {
            var vet = GetById(id);
            if (vet != null)
            {
                string message = $"El veterinario se elimino correctamente\nId: {vet.Id} | Nombre: {vet.Nombre}";
                veterinarios.Remove(vet);
                repositorio.SaveList(veterinarios);
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

        public override List<Veterinario> GetAll()
        {
            return veterinarios;
        }

        public override Veterinario GetById(int id)
        {
            var veterinario=BuscadorEntidad.ObtenerVeterinarioPorId(veterinarios, id);
            return veterinario;
        }

        public override ResultadoOperacion Update(Veterinario veterinario)
        {
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
                repositorio.SaveList(veterinarios);
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
