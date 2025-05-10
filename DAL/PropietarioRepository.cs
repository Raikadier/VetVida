using ENTITY;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PropietarioRepository : FileRepository<Propietario>, IGetById<Propietario>
    {
        public PropietarioRepository(string file) : base(file) { }
        public override List<Propietario> Read()
        {
            try
            {
                List<Propietario> propietarios = new List<Propietario>();
                StreamReader reader = new StreamReader(file);
                while (!reader.EndOfStream)
                {
                    propietarios.Add(MappingType(reader.ReadLine()));
                }
                reader.Close();
                return propietarios;
            }
            catch (Exception)
            {
                return new List<Propietario>();
            }
        }
        public override Propietario MappingType(string datos)
        {
            Propietario propietario = new Propietario();
            propietario.Id = int.Parse(datos.Split(';')[0]);
            propietario.Nombre = datos.Split(';')[1];
            propietario.Apellido = datos.Split(';')[2];
            propietario.Cedula = int.Parse(datos.Split(';')[3]);
            propietario.Telefono = int.Parse(datos.Split(';')[4]);
            return propietario;
        }
        public Propietario GetById(int id)
        {
            List<Propietario> propietarios = Read();
            return propietarios.FirstOrDefault(p => p.Id == id);
        }
    }
}
