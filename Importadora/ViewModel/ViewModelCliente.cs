using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Importadora.ViewModel
{
    public class ViewModelCliente
    {
        public int Id { get; set; }
        public string Rut { get; set; }
        [Display(Name = "Nombre de cliente ")]
        public string Nombre { get; set; }
        [Display(Name = "Fecha de Registro"), DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistro { get; set; }
       
    }
}