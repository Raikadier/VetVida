using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
namespace DAL
{
    public class VeterinarioRepository : FileRepository<Veterinario>
    {
        public VeterinarioRepository(string file) : base(file)
        {
        }
        public override List<Veterinario> Read(){
            
            try
            {
                List<Veterinario> veterinarios = new List<Veterinario>();
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    veterinarios.Add(MappingType(reader.ReadLine()));
                }
                reader.Close();
                return veterinarios;
            }
            catch (Exception)
            {
                return new List<Veterinario>();
            }
        }
        public override Veterinario MappingType(string datos)
        {
            Veterinario veterinario = new Veterinario();
            veterinario.Id=int.Parse(datos.Split(';')[0]);
            veterinario.Nombre = datos.Split(';')[1];
            veterinario.Apellido = datos.Split(';')[2];
            veterinario.Cedula = int.Parse(datos.Split(';')[3]);
            veterinario.Telefono = int.Parse(datos.Split(';')[4]);
            veterinario.Especialidad = datos.Split(';')[5];
            return veterinario;
        }
    }
}
