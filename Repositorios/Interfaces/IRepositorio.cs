using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios
{
    interface IRepositorio<T>
    {
        bool Alta(T obj);
        bool Baja(object id);
        bool Modificacion(T obj);
        List<T> TraerTodo();
        T BuscarPorId(object id);

    }
}