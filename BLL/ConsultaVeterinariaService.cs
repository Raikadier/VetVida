using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class ConsultaVeterinariaService : ILogic<ConsultaVeterinaria>, IListSearchForEntity<ConsultaVeterinaria>
    {
        private readonly ConsultaVeterinariaRepository consultaVeterinariaRepository;
        private List<ConsultaVeterinaria> consultasVeterinarias;
        public ConsultaVeterinariaService()
        {
            consultaVeterinariaRepository = new ConsultaVeterinariaRepository(Archivos.ARC_CONSULTAVETERINARIO);
            consultasVeterinarias = consultaVeterinariaRepository.Read();
        }

        public ResultadoOperacion Delete(int id)
        {
            try
            {
                var consulta = GetById(id);
                if (consulta != null)
                {
                    string message = $"La consulta se elimino correctamente\nId: {consulta.Id}";
                    consultasVeterinarias.Remove(consulta);
                    consultaVeterinariaRepository.SaveList(consultasVeterinarias);
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = message
                    };
                }
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"La consulta no existe"
                };
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al eliminar la consulta veterinaria \nId: {id}\n{ex.Message}"
                };
            }
        }

        public List<ConsultaVeterinaria> GetAll()
        {
            try
            {
                return consultaVeterinariaRepository.Read();
            }
            catch (Exception)
            {
                return new List<ConsultaVeterinaria>();
            }
        }

        public ConsultaVeterinaria GetById(int id)
        {
            var consulta = BuscadorEntidad.ObtenerConsultaPorId(consultasVeterinarias, id);
            return consulta;
        }

        public ResultadoOperacion Save(ConsultaVeterinaria consulta)
        {
            try
            {
                if (consulta == null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"La consulta es nula"
                    };
                }
                if (GetById(consulta.Id) != null)
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Esta consulta ya esta registrada\nId: {GetById(consulta.Id).Id} | Fecha: {GetById(consulta.Id).Fecha}"
                    };
                }
                if (consultaVeterinariaRepository.Save(consulta))
                {
                    consultasVeterinarias.Add(consulta);
                    return new ResultadoOperacion
                    {
                        Exito = true,
                        Mensaje = $"La consulta se guardo correctamente\nId: {consulta.Id} | Fecha: {consulta.Fecha}"
                    };
                }
                else
                {
                    return new ResultadoOperacion
                    {
                        Exito = false,
                        Mensaje = $"Error al guardar la consulta \nId: {consulta.Id}"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = $"Error al guardar la consulta \nId: {consulta.Id}\n{ex.Message}"
                };
            }
        }

        public List<ConsultaVeterinaria> SearchForEntity(int op, int id)
        {
            if (op == 1)
            {
                return consultasVeterinarias.Where(c => c.Mascota.Id == id).ToList();
            }
            else if (op == 2)
            {
                return consultasVeterinarias.Where(c => c.Veterinario.Id == id).ToList();
            }
            else
            {
                return new List<ConsultaVeterinaria>();
            }
        }
        public ResultadoOperacion DeleteByMascota(int id)
        {
            var consulta = GetAll().Where<ConsultaVeterinaria>(c=>c.Mascota.Id!= id).ToList();
            if (consulta.Count != 0)
            {
                consultaVeterinariaRepository.SaveList(consulta);
                return new ResultadoOperacion()
                {
                    Exito = true,
                    Mensaje = $"La consulta se elimino correctamente"
                };
            }
            else
            {
                return new ResultadoOperacion()
                {
                    Exito = false,
                    Mensaje = $"La consulta no se encontro"
                };
            }
        }

        public ResultadoOperacion Update(ConsultaVeterinaria consulta)
        {
            if (GetById(consulta.Id) != null)
            {
                foreach (var c in consultasVeterinarias)
                {
                    if (c.Id == consulta.Id)
                    {
                        c.Id = consulta.Id;
                        c.Fecha = consulta.Fecha;
                        c.Mascota = consulta.Mascota;
                        c.Veterinario = consulta.Veterinario;
                        c.Diagnostico = consulta.Diagnostico;
                        c.Tratamiento = consulta.Tratamiento;
                    }
                }
                consultaVeterinariaRepository.SaveList(consultasVeterinarias);
                return new ResultadoOperacion()
                {
                    Exito = true,
                    Mensaje = $"La consulta se actualizo correctamente"
                };
            }
            return new ResultadoOperacion()
            {
                Exito = false,
                Mensaje = $"La consulta no se encontro"
            };
        }
    }
}
