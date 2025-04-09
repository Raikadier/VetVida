using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public abstract class FileRepository<T>
    {
        protected string file=string.Empty;
        protected FileRepository(string file)
        {
            this.file = file;
        }
        public virtual void Save(T entity)
        {
            try
            {
                StreamWriter writer = new StreamWriter(file, true);
                writer.WriteLine(entity.ToString());
                writer.Close(); 
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception($"No tienes permisos para acceder al archivo: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                throw new Exception($"Ruta malformada o con formato inválido: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception($"La carpeta de destino no existe: {ex.Message}");
            }
            catch (IOException ex)
            {
                throw new Exception($"Error de entrada/salida: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado: {ex.Message}");
            }
        }
        public virtual void SaveList(List<T> entities)
        {
            try
            {
                StreamWriter writer = new StreamWriter(file, false);
                foreach (var entity in entities)
                {
                    writer.WriteLine(entity.ToString());
                }
                writer.Close();
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new Exception($"No tienes permisos para acceder al archivo: {ex.Message}");
            }
            catch (NotSupportedException ex)
            {
                throw new Exception($"Ruta malformada o con formato inválido: {ex.Message}");
            }
            catch (DirectoryNotFoundException ex)
            {
                throw new Exception($"La carpeta de destino no existe: {ex.Message}");
            }
            catch (IOException ex)
            {
                throw new Exception($"Error de entrada/salida: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inesperado: {ex.Message}");
            }
        }
        public abstract List<T> Read();
        public abstract T MappingType(string datos);
    }
}
