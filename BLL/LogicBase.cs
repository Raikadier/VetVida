using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY;
namespace BLL
{
    public abstract class LogicBase<T> : ILogic<T>
    {
        protected readonly FileRepository<T> repositorio;
        public LogicBase(FileRepository<T> repositorio)
        {
            this.repositorio = repositorio;
        }

        public abstract ResultadoOperacion Delete(int id);


        public abstract List<T> GetAll();

        public abstract T GetById(int id);

        public virtual ResultadoOperacion Save(T entidad)
        {
            try
            {
                repositorio.Save(entidad);
                return new ResultadoOperacion
                {
                    Exito = true,
                    Mensaje = string.Empty
                };
            }
            catch(Exception ex)
            {
                return new ResultadoOperacion
                {
                    Exito = false,
                    Mensaje = string.Empty
                };
            }
        }

        public abstract ResultadoOperacion Update(T entidad);
    }
}
