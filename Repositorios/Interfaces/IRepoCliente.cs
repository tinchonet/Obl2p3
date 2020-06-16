using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Repositorios.Interfaces
{
    interface IRepoCliente : IRepositorio<Cliente>
    {
        decimal ObtenerPorcentajeDescuento();
        decimal ObtenerPorcentajeGanancia();
        int ObtenerAntiguedadMinima();
    }
}
