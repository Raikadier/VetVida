using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface ILogic<T>
    {
        ResultadoOperacion Save(T entidad);
        ResultadoOperacion Update(T entidad);
        ResultadoOperacion Delete(int id);
        T GetById(int id);
        List<T> GetAll();
    }
}
