using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class FileRepository<T> : IRepository<T>
    {
        protected string file=string.Empty;
        protected FileRepository(string file)
        {
            this.file = file;
        }
        public virtual bool Save(T entity)
        {
            try
            {
                if(entity == null)
                {
                    throw new ArgumentNullException("El objeto no puede ser nulo");
                }
                if (!File.Exists(file))
                {
                    File.Create(file).Close();
                }
                StreamWriter writer = new StreamWriter(file, true);
                writer.WriteLine(entity.ToString());
                writer.Close();   
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                return false;
                throw new Exception($"No tienes permisos para acceder al archivo: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                return false;
                throw new Exception($"Ruta malformada o con formato inválido: {ex.Message}");
            }
            catch (FileNotFoundException ex)
            {
                return false;
                throw new Exception($"El archivo no existe: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                return false;
                throw new Exception($"La carpeta de destino no existe: {ex.Message}");
            }
            catch (IOException ex)
            {
                return false;
                throw new Exception($"Error de entrada/salida: {ex.Message}");
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception($"Error inesperado: {ex.Message}");
               
            }
        }
        public virtual bool SaveList(List<T> entities)
        {
            try
            {
                StreamWriter writer = new StreamWriter(file, false);
                foreach (var entity in entities)
                {
                    writer.WriteLine(entity.ToString());
                }
                writer.Close();
                return true;
            }
            catch (UnauthorizedAccessException ex)
            {
                return false;
                throw new Exception($"No tienes permisos para acceder al archivo: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                return false;
                throw new Exception($"Ruta malformada o con formato inválido: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                return false;
                throw new Exception($"La carpeta de destino no existe: {ex.Message}");
            }
            catch(FileNotFoundException ex)
            {
                return false;
                throw new Exception($"El archivo no existe: {ex.Message}");
            }
            catch (IOException ex)
            {
                return false;
                throw new Exception($"Error de entrada/salida: {ex.Message}");
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception($"Error inesperado: {ex.Message}");
            }
        }
        public abstract List<T> Read();
        public abstract T MappingType(string datos);
    }
}
