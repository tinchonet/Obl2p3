using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {

        public int Id { get; set; }
        public string Ci { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }
        public string Email { get; set; }

        //Validaciones
        //La cédula es un numérico con 7 u 8 dígitos.
        public static bool ValidarCedula(string cedula)
        {
            bool esValido = false;
            if (cedula != null)
            {
                if (cedula.Length >= 7 && cedula.Length <= 8)
                {
                    //Verifico que sean solo numeros
                    if (CedulaSinCaracter(cedula))
                    {
                        esValido = true;
                    }
                }

            }
            return esValido;
        }

        public static bool CedulaSinCaracter(string cedula)
        {
            bool ret = false;
            if (cedula != null)
            {

                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasLowerChar = new Regex(@"[a-z]+");
                var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
                if (!hasLowerChar.IsMatch(cedula) && !hasUpperChar.IsMatch(cedula) && !hasSymbols.IsMatch(cedula))
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }
            }
            else
            {
                ret = false;
            }
            return ret;
        }

        public static string ComplejidadPassword(string clave)
        {
            var hasUpperChar = new Regex(@"[A-Z]");
            var hasLowerChar = new Regex(@"[a-z]");
            var hasNumber = new Regex(@"[0-9]");

            string esValido = "ok";
            if (clave != null)
            {
                if (clave.Length < 6)
                {
                    esValido = "La es clave muy corta, debe tener al menos 6 caracteres.";
                }
                else
                {
                    if (!hasNumber.IsMatch(clave))
                    {
                        esValido = "La clave debe contener al menos un digito numerico.";
                    }
                    if (!hasLowerChar.IsMatch(clave) || !hasUpperChar.IsMatch(clave))
                    {
                        esValido = "La clave debe contener al menos una minuscula y una mayuscula";
                    }
                }
            }
            else
            {
                esValido = "La clave no debe estar vacia";
            }
            return esValido;
        }


    }
}
