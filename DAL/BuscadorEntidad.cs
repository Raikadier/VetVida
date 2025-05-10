using ENTITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class BuscadorEntidad
    {
        public static Propietario ObtenerPropietarioPorId(List<Propietario> propietarios, int id)
        => propietarios.FirstOrDefault<Propietario>(p => p.Id == id);
        public static Raza ObtenerRazaPorId(List<Raza> razas, int id)
            => razas.FirstOrDefault<Raza>(r => r.Id == id);
        public static Especie ObtenerEspeciePorId(List<Especie> especies, int id)
            => especies.FirstOrDefault<Especie>(e => e.Id == id);
        public static Veterinario ObtenerVeterinarioPorId(List<Veterinario> veterinarios, int id)
            => veterinarios.FirstOrDefault<Veterinario>(v => v.Id == id);
        public static Mascota ObtenerMascotaPorId(List<Mascota> mascotas, int id)
            => mascotas.FirstOrDefault<Mascota>(m => m.Id == id);
        public static ConsultaVeterinaria ObtenerConsultaPorId(List<ConsultaVeterinaria> consultas, int id)
            => consultas.FirstOrDefault<ConsultaVeterinaria>(c => c.Id == id);
    }
}
