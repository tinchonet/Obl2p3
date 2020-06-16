using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Importadora.ViewModel
{
    public class ViewModelProducto
    {
        public int Id { get; set; }
        [Display(Name = "Nombre de producto ")]
        public string Nombre { get; set; }
        public decimal Peso { get; set; }
        public ViewModelCliente Cliente { get; set; }
        public int Stock { get; set; }

    }
}