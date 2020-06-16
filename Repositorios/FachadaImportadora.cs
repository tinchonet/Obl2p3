using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using Repositorios;

namespace Repositorios
{
    public class FachadaImportadora
    {
        #region usuario

        public static Usuario BuscarUsuarioPorCi(string ci)
        {
            RepoUsuario repoUsuario = new RepoUsuario();

            return repoUsuario.BuscarPorId(ci);
        }

        public static Usuario Login(string ci, string password)
        {
            RepoUsuario repoUsuario = new RepoUsuario();
            Usuario u = repoUsuario.BuscarPorId(ci);
            if (u != null && u.Clave == password)
            {

                return repoUsuario.BuscarPorId(ci);
            }
            else
            {
                return null;
            }
        }

        public static bool AltaUsuario(Usuario u)
        {
            bool ret = false;

            RepoUsuario repo = new RepoUsuario();
            if (Usuario.ComplejidadPassword(u.Clave) == "ok" && BuscarUsuarioPorCi(u.Ci) == null)
            {
                ret = repo.Alta(u);

            }
            return ret;
        }

        #endregion

        #region producto
        public static Producto BuscarProductoPorId(int id)
        {
            RepoProducto repoProducto = new RepoProducto();

            return repoProducto.BuscarPorId(id);
        }

        public static List<Producto> ProductosCliente(string id)
        {
            RepoProducto repoProducto = new RepoProducto();
            List<Producto> prods = new List<Producto>();
            prods = repoProducto.ProductosCliente(id);
            return prods;
        }

        #endregion
        #region cliente

        public static Cliente BuscarClientePorId(string rut)
        {
            RepoCliente repoCliente = new RepoCliente();
            return repoCliente.BuscarPorId(rut);
        }

        public static List<Cliente> ListarClientes()

        {
            List<Cliente> listaClientes = new List<Cliente>();
            RepoCliente repoCliente = new RepoCliente();
            listaClientes = repoCliente.TraerTodo();
            return listaClientes;
        }

        #endregion
        #region importacion

        public static bool AltaImportacion(Importacion i)
        {
            bool ret = false;
            RepoImportacion repo = new RepoImportacion();
            if (i.ValidarPrecio())
            {
                ret = repo.Alta(i);
            }
            return ret;
        }

        public static List<Importacion> ListarImportaciones()
        {

            RepoImportacion r = new RepoImportacion();
            List<Importacion> importaciones = r.TraerTodo();
            return importaciones;
        }

        public static List<Importacion> ListarImportacionesStock()
        {

            RepoImportacion r = new RepoImportacion();
            List<Importacion> importaciones = r.ImportacionesEnStock();
            return importaciones;
        }

        public static List<Importacion> ListarImportacionesStockCliente(Object id)
        {
            RepoImportacion r = new RepoImportacion();
            List<Importacion> importaciones = r.ImportacionesEnStockCliente(id);
            return importaciones;
        }

        public static decimal CalcularGananciaPorCliente(Object id)
        {
            RepoCliente repoCliente = new RepoCliente();
            RepoImportacion r = new RepoImportacion();
            decimal ganancia = repoCliente.ObtenerPorcentajeGanancia();
            decimal descuento = repoCliente.ObtenerPorcentajeDescuento();
            int antiguedad = repoCliente.ObtenerAntiguedadMinima();

            decimal total = 0;
            foreach (Importacion i in r.ImportacionesEnStockCliente(id))
            {
                total += i.GananciaPrevista(ganancia, descuento, antiguedad);
            }
            return total;
        }

        public static decimal CalcularGananciaPorImportacion(Importacion i)
        {
            RepoCliente repoCliente = new RepoCliente();
            RepoImportacion r = new RepoImportacion();
            decimal ganancia = repoCliente.ObtenerPorcentajeGanancia();
            decimal descuento = repoCliente.ObtenerPorcentajeDescuento();
            int antiguedad = repoCliente.ObtenerAntiguedadMinima();

            decimal total = 0;
            if (i != null)
            {
                total = i.GananciaPrevista(ganancia, descuento, antiguedad);
            }

            return total;
        }

        #endregion
        public static bool GuardarArchivos()
        {
            bool ret = Archivos.GuardarArchivos();
            return ret;
        }


    }
}
