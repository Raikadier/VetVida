using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface IRepository <T>
    {
        bool Save(T entity);
        bool SaveList(List<T> entities);
        List<T> Read();
        T MappingType(string datos);
    }
}
