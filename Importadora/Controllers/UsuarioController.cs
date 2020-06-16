using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dominio;
using Importadora.ViewModel;
using Repositorios;

namespace Importadora.Controllers
{
    public class UsuarioController : Controller
    {


        public ActionResult Index(string mensaje)
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "admin" && Session["rol"].ToString() != "deposito")
                return RedirectToAction("login", "usuario");

            ViewBag.Mensaje = mensaje;
            return View();
        }
    



        public ActionResult Login(string mensaje)
        {
            ViewBag.Mensaje = mensaje;
            return View();
        }



        [HttpPost]
        public ActionResult Login(FormCollection col)
        {
            //Fachada importadora devuevle usario null si no cumple las condicioens
            Usuario u = FachadaImportadora.Login(col["user"], col["password"]);
            string error = "";
            if (u != null)
            {
                Session["user"] = u;
                Session["rol"] = u.Rol;
                return RedirectToAction("Index","Importacion", new { mensaje = "Login Correcto" });

            }
            else
            {
                error = "Usuario o Password incorrecto";
            }
            return RedirectToAction("Login", new { mensaje = error });
        }






        public ActionResult Salir()
        {
            Session["rol"] = null;
            Session["user"] = null;
            return RedirectToAction("login");
        }

        public ActionResult Create(string mensaje)
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "admin")
                return RedirectToAction("login", "usuario");
            ViewBag.Current = "UsusarioCreate";

            ViewModelUsuario u = new ViewModelUsuario();
            return View(u);
        }

        //ALTAR USUARIOS
        [HttpPost]
        public ActionResult Create(ViewModelUsuario u)
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "admin")
                return RedirectToAction("login", "usuario");

            ViewBag.Current = "UsusarioCreate";

            string passwordOk = Usuario.ComplejidadPassword(u.Clave);
            bool ciOk = Usuario.ValidarCedula(u.Ci);
            if (passwordOk == "ok" && ciOk)
            {
                Usuario unU = new Usuario
                {
                    Ci = u.Ci,
                    Clave = u.Clave,
                    Rol = u.Rol.ToString(),
                    Email = u.Email
                };
                if (FachadaImportadora.AltaUsuario(unU) && ciOk)
                    return RedirectToAction("Index","Importacion", new { mensaje = "Usuario " + u.Ci + " creado correctamente con el rol de: " + u.Rol });

            }
            else
                if (passwordOk != "ok")
            {
                ViewBag.ErrorPass = passwordOk;
            }
            if (!Usuario.CedulaSinCaracter(u.Ci))
            {
                ViewBag.ErrorCi = "La Cedula debe conetener unicamente numeros";
            }
            ViewBag.Error = "No se puedo dar de alta el usuario";
            return View(u);

        }

        //GUARDAR ACHIVOS
        public ActionResult Guardar()
        {
            if (Session["rol"] == null)
                return RedirectToAction("login", "usuario");

            if (Session["rol"].ToString() != "admin" && Session["rol"].ToString() != "deposito")
                return RedirectToAction("login", "usuario");

            if (FachadaImportadora.GuardarArchivos())
            
                return RedirectToAction("Index", "Importacion", new { mensaje = "Se guardaron los archivos correctamente" });
            else
                return RedirectToAction("Index", "Importacion", new { mensaje = "Hubo un problema guardado los archivos" });


        }
    }
}