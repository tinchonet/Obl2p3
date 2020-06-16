using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Importadora.ServicioProducto;
using Importadora.ViewModel;
using Repositorios;
using Dominio;

namespace Importadora.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente


        // GET: Cliente/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}




        // GET: Cliente
        public ActionResult ListarClientes()
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "admin" && Session["rol"].ToString() != "deposito")
                return RedirectToAction("login", "usuario");

            List<ViewModelCliente> vmClientes = new List<ViewModelCliente>();

            foreach (Cliente c in FachadaImportadora.ListarClientes())
            {
                ViewModelCliente cliente = new ViewModelCliente
                {
                    Id = c.Id,
                    Rut = c.Rut,
                    Nombre = c.Nombre,
                    FechaRegistro = c.FechaRegistro,

                };
                vmClientes.Add(cliente);
            }
            return View(vmClientes);
        }


    }
}


