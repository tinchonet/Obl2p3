using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Interfaces
{
    interface IRepoProducto : IRepositorio<Producto>
    {
        List<Producto> ProductosEnStock();
        List<Producto> ProductosCliente(string rut);
    }
}
