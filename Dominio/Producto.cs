using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Producto
    {

        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal Peso { get; set; }
        public Cliente Cliente { get; set; }
        //le estoy agregando stock por la consulta de listar productos
        public int Stock { get; set; }


    }
}
