using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Importadora.ViewModel
{
    public class ViewModelUsuario
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo cedula es obligatorio")]
        [Display(Name = "Cedula de identidad")]
        [MinLength(7, ErrorMessage = "La cedula de usuario debe tener al menos 7 digitos")]
        public String Ci { get; set; }
        [DataType(DataType.Password)]
        public string Clave { get; set; }
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public Rol Rol { get; set; }


    }
    public enum Rol
    {
        [Description("Admin")]
        admin,
        [Description("Deposito")]
        deposito
    }

    




}

