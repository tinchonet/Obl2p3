using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Importadora.ServicioProducto;
using Importadora.ViewModel;
using Dominio;
using Repositorios;
using System.Net;

namespace Importadora.Controllers
{
    public class ProductoController : Controller
    {

        // GET: Producto LISTAR PRODUCTOS EN STOCK
        public ActionResult ListarProdEnStock()
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "admin" && Session["rol"].ToString() != "deposito")
                return RedirectToAction("login", "usuario");


            List<DTOProducto> dtoProductos = null;
            List<ViewModelProducto> vmProductos = new List<ViewModelProducto>();
            ServicioProductosClient proxy = new ServicioProductosClient();
            proxy.Open();
            dtoProductos = proxy.ProductosEnStock().ToList();
            ViewBag.ListarProductos = dtoProductos;
            foreach (DTOProducto p in dtoProductos)
            {
                ViewModelProducto prod = new ViewModelProducto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Stock = p.Stock
                };
                vmProductos.Add(prod);
            }
            return View(vmProductos);
        }

        // GET: Producto/Details/5   DETALLES DE PRODUCTOS EN STOCK
        public ActionResult Details(int id)
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "deposito" && Session["rol"].ToString() != "admin")
                return RedirectToAction("login", "usuario");

            ViewModelProducto vmProd = null;
            Producto p = FachadaImportadora.BuscarProductoPorId(id);
            ViewModelCliente c = new ViewModelCliente
            {
                Id = p.Cliente.Id,
                Nombre = p.Cliente.Nombre,
                Rut = p.Cliente.Rut,
                FechaRegistro = p.Cliente.FechaRegistro
            };
            vmProd = new ViewModelProducto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Peso = p.Peso,
                Cliente = c
            };
            if (vmProd == null)
            {
                return HttpNotFound();
            }
            return View(vmProd);
        }


        //PRODUCTOS DE UN CLIENTE
        public ActionResult ProductosCliente(string id)
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "deposito")
                return RedirectToAction("login", "usuario");

            List<ViewModelProducto> vmProds = new List<ViewModelProducto>();

            foreach (Producto p in FachadaImportadora.ProductosCliente(id))
            {
                ViewModelCliente c = new ViewModelCliente
                {
                    Id = p.Cliente.Id,
                    Nombre = p.Cliente.Nombre,
                    Rut = p.Cliente.Rut,
                    FechaRegistro = p.Cliente.FechaRegistro
                };
                ViewModelProducto prod = new ViewModelProducto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Peso = p.Peso,
                    Cliente = c
                };
                vmProds.Add(prod);
            }
            return View(vmProds);
        }




    }
}
