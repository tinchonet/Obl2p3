using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositorios.Interfaces
{
    interface IRepoImportacion : IRepositorio<Importacion>
    {
        List<Importacion> ImportacionesEnStock();
        List<Importacion> ImportacionesEnStockCliente(Object id);
    }
}
