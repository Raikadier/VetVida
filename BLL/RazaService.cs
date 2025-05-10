using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class RazaService : LogicBase<Raza> , IListSearchForEntity<Raza>
    {
        private readonly FileRepository<Raza> repositorio;
        private RazaRepository razaRepository;
        private List<Raza> razas;
        public RazaService(FileRepository<Raza> repositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
            razaRepository = new RazaRepository("Raza.txt");
            razas = repositorio.Read();
        }
        public List<Raza> SearchForEntity(int idEspecie)
        {
            List<Raza> razas = repositorio.Read();
            return razas.Where(r => r.especie.Id == idEspecie).ToList();
        }

        public override ResultadoOperacion Save(Raza raza)
        {
            try
            {
                if (GetById(raza.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Esta raza ya esta registrada\nId: {GetById(raza.Id).Id} | Nombre: {GetById(raza.Id).Nombre}"
                    };
                }
                if (repositorio.Save(raza))
                {
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
            var raza = GetById(id);
            if (raza != null)
            {
                string message = $"La raza se elimino correctamente\nId: {raza.Id} | Nombre: {raza.Nombre}";
                razas.Remove(raza);
                repositorio.SaveList(razas);
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

        public override List<Raza> GetAll()
        {
            return razas;
        }

        public override Raza GetById(int id)
        {
            var raza = BuscadorEntidad.ObtenerRazaPorId(razas, id);
            return raza;
        }

        public override ResultadoOperacion Update(Raza raza)
        {
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
                repositorio.SaveList(razas);
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
    }
}
