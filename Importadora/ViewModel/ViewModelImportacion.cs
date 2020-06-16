using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Importadora.ViewModel
{
    public class ViewModelImportacion
    {
        [Display(Name = "Id de la importacion")]
        public int Id { get; set; }
        [Display(Name = "Cantidad de unidades")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe ingresar una cantidad mayor a 0")]
        public int CantidadUnidades { get; set; }
        [Display(Name = "Id del Producto")]
        public int IdProducto { get; set; }
        [Display(Name = "Nombre del Producto")]
        public string NombrePord { get; set; }
        [Range(0.1, double.MaxValue, ErrorMessage = "Precio debe ser distinto de 0")]
        public decimal Precio { get; set; }
        public string Salio { get; set; }
        [Display(Name = "Fecha de ingreso"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaIngreso { get; set; }
        [Display(Name = "Fecha de salida prevista"), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaSalida { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Costo { get; set; }
        public char Estado { get; set; }



        //Agregar Validacion de fechas.
        //Bloquear el campo id producto en importacion.
    }
}