
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{


    public class Importacion

    {

        public int Id { get; set; }
        public int CantidadUnidades { get; set; }
        public Producto Producto { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        public char Estado { get; set; }

        //<0 − If date1 is earlier than date2
        //0 − If date1 is the same as date2
        //>0 − If date1 is later than date2
        //Validaciones
        public bool ValidarPrecio()
        {
            return this.Precio > 0;
        }
        public decimal GananciaPrevista(decimal ganancia, decimal descuento, int antiguedadMininma)
        {
            DateTime FechaAux;
            int resultado = DateTime.Compare(FechaSalida, DateTime.Today);
            if (resultado <= 0 && Estado != 'n')
            {
                FechaAux = DateTime.Today;
            }
            else
            {
                FechaAux = FechaSalida;
            }
            int diasAlmacenado = ((TimeSpan)(FechaAux - FechaIngreso)).Days;
            if (diasAlmacenado == 0)
            {
                diasAlmacenado = 1;
            }
            decimal total = Precio * CantidadUnidades * diasAlmacenado * ganancia / 100;
            if (Producto.Cliente.CalcularAntiguedad(antiguedadMininma))
            {
                total = total - total * descuento / 100;
            }
            return total;
        }

        public bool ValidarFechas()
        {
            bool ret = false;
            if (DateTime.Compare(FechaIngreso, FechaSalida) <= 0)
                ret = true;
            return ret;
        }

    }
}
