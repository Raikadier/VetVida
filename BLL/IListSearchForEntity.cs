using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    interface IListSearchForEntity <T>
    {
        List<T> SearchForEntity(int id);
    }
}
