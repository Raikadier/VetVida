using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class EspecieService : LogicBase<Especie>
    {
        private readonly FileRepository<Especie> repositorio;
        private EspecieRepository especieRepository;
        private List<Especie> especies;
        public EspecieService(FileRepository<Especie> repositorio) : base(repositorio)
        {
            this.repositorio = repositorio;
            especieRepository = new EspecieRepository("Especie.txt");
            especies = repositorio.Read();
        }

        public override ResultadoOperacion Save(Especie especie)
        {
            try
            {
                if (GetById(especie.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Esta especie ya esta registrada\nId: {GetById(especie.Id).Id} | Nombre: {GetById(especie.Id).Nombre}"
                    };
                }
                if (repositorio.Save(especie))
                {
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
                    Mensaje = string.Empty
                };
            }
        }
        public override ResultadoOperacion Delete(int id)
        {
            var especie = GetById(id);
            if (especie != null)
            {
                string message = $"La especie se elimino correctamente\nId: {especie.Id} | Nombre: {especie.Nombre}";
                especies.Remove(especie);
                repositorio.SaveList(especies);
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

        public override List<Especie> GetAll()
        {
            return especies;
        }

        public override Especie GetById(int id)
        {
            var especie = BuscadorEntidad.ObtenerEspeciePorId(especies, id);
            return especie;
        }

        public override ResultadoOperacion Update(Especie especie)
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
                repositorio.SaveList(especies);
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
