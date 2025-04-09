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
        => propietarios.FirstOrDefault(p => p.Id == id);

        public static Raza ObtenerRazaPorId(List<Raza> razas, int id)
            => razas.FirstOrDefault(r => r.Id == id);

        public static Especie ObtenerEspeciePorId(List<Especie> especies, int id)
            => especies.FirstOrDefault(e => e.Id == id);
        public static Veterinario ObtenerVeterinarioPorId(List<Veterinario> veterinarios, int id)
            => veterinarios.FirstOrDefault(v => v.Id == id);
        public static Mascota ObtenerMascotaPorId(List<Mascota> mascotas, int id)
            => mascotas.FirstOrDefault(m => m.Id == id);
    }
}
