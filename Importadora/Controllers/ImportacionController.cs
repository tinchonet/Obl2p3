using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Importadora.ViewModel;
using Repositorios;
using Importadora.ServicioImportacion;

namespace Importadora.Controllers
{
    public class ImportacionController : Controller
    {
        // GET: Importacion INDEX
        public ActionResult Index(string mensaje)
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "deposito" && Session["rol"].ToString() != "admin")
                return RedirectToAction("login", "usuario");
            ViewBag.Mensaje = mensaje;
            List<Importacion> importaciones = null;
            List<ViewModelImportacion> vmImportaciones = new List<ViewModelImportacion>();

            importaciones = FachadaImportadora.ListarImportaciones();
            foreach (Importacion i in importaciones)
            {
                ViewModelImportacion vmI = new ViewModelImportacion
                {
                    Id = i.Id,
                    CantidadUnidades = i.CantidadUnidades,
                    FechaIngreso = i.FechaIngreso,
                    FechaSalida = i.FechaSalida,
                    IdProducto = i.Producto.Id,
                    Precio = i.Precio,
                    NombrePord = i.Producto.Nombre,
                    Costo = FachadaImportadora.CalcularGananciaPorImportacion(i),
                    Estado = i.Estado
                };
                vmImportaciones.Add(vmI);
            }


            return View(vmImportaciones);

        }





        //PREVISION DE GANANCIA
        public ActionResult PrevGanancia(Object id)

        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "admin")
                return RedirectToAction("login", "usuario");

            List<Importacion> importaciones = null;
            List<ViewModelImportacion> vmImportaciones = new List<ViewModelImportacion>();

            importaciones = FachadaImportadora.ListarImportacionesStockCliente(id);
            foreach (Importacion i in importaciones)
            {
                ViewModelImportacion vmI = new ViewModelImportacion
                {
                    Id = i.Id,
                    CantidadUnidades = i.CantidadUnidades,
                    FechaIngreso = i.FechaIngreso,
                    FechaSalida = i.FechaSalida,
                    IdProducto = i.Producto.Id,
                    Precio = i.Precio,
                    NombrePord = i.Producto.Nombre,
                    Costo = FachadaImportadora.CalcularGananciaPorImportacion(i)
                };
                vmImportaciones.Add(vmI);
            }


            string total = FachadaImportadora.CalcularGananciaPorCliente(id).ToString("C", CultureInfo.GetCultureInfo("es-UY"));
            ViewBag.MostrarGanancia = total;

            return View(vmImportaciones);
        }

        // GET: Importacion/Details/5
        public ActionResult Details(int id)
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "admin")
                return RedirectToAction("login", "usuario");


            return View();
        }

        // GET: Importacion/Create
        public ActionResult Create(int id)
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "deposito")
                return RedirectToAction("login", "usuario");
            ViewBag.Current = "ImportacionCreate";

            ViewModelImportacion importacion = new ViewModelImportacion
            {
                FechaIngreso = DateTime.Today,
                FechaSalida = DateTime.Today,
                IdProducto = id
            };

            return View(importacion);
        }


        //CREAR IMPORTACION
        [HttpPost] //Click a un booton
        public ActionResult Create(ViewModelImportacion importacion)
        {
            try
            {
                if (Session["rol"] == null)
                    return RedirectToAction("login", "usuario");

                if (Session["rol"].ToString() != "deposito")
                    return RedirectToAction("login", "usuario");
                ViewBag.Current = "ImportacionCreate";


                if (ModelState.IsValid)
                {
                    
                    ServicioImportacionClient proxy = new ServicioImportacionClient();
                  
                    DTOImportacion i = new DTOImportacion
                    {
                        CantidadUnidades = importacion.CantidadUnidades,
                        FechaIngreso = importacion.FechaIngreso,
                        FechaSalida = importacion.FechaSalida,
                        Precio = importacion.Precio,
                        IdProducto = importacion.IdProducto

                    };
                    if (proxy.AltaImportacion(i))
                    {
               
                        return RedirectToAction("Index", new { mensaje = "Se creo correctamente la importacion" });
                    }
                    else
                    {
                        ViewBag.Error = "Verifique los datos";
                        return View(importacion);
                    }


                }
                else
                {
                    return View(importacion);
                }


            }
            catch
            {
                return View();
            }


        }

    }
}
