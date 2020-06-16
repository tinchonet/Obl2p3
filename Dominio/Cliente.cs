using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dominio
{
    public class Cliente
    {

        //prueba
        public int Id { get; set; }
        public string Rut { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public static decimal Porcentaje { get; set; }


        //Validaciones
        public bool ValidarRut()
        {
            bool esValido = false;
            if (this.Rut.Length == 12)
            {
                esValido = true;
            }
            return esValido;
        }

        public bool CalcularAntiguedad(int antiguedadMinima)
        {
            bool ret = false;

            int resultado = DateTime.Compare(FechaRegistro.AddYears(antiguedadMinima), DateTime.Today);
            if (resultado <= 0)
            {
                ret = true;
            }
            return ret;
        }


    }
}





