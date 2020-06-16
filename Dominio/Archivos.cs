using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Archivos
    {

        public static bool GuardarArchivos()
        {

            String rutaAlEjecutable = AppDomain.CurrentDomain.BaseDirectory;
            String carpeta = rutaAlEjecutable + "Archivos";
            bool exists = Directory.Exists(carpeta);
            if (!exists)
            {
                Directory.CreateDirectory(carpeta);
            }
            string clientes = ArmarStringClientes("#");
            string productos = ArmarStringProductos("#");
            string importacion = ArmarStringImportacion("#");
            string usuarios = ArmarStringUsuario("#");
            string variablesEstaticas = ArmarStringStatic("#");


            try
            {
                FileStream rutaClientes = new FileStream(carpeta + "\\Clientes.txt", FileMode.Create);
                FileStream rutaProducto = new FileStream(carpeta + "\\Productos.txt", FileMode.Create);
                FileStream rutaImportacion = new FileStream(carpeta + "\\Importacion.txt", FileMode.Create);
                FileStream rutaUsuario = new FileStream(carpeta + "\\Usuarios.txt", FileMode.Create);
                FileStream rutaStatic = new FileStream(carpeta + "\\Static.txt", FileMode.Create);

                using (StreamWriter swClientes = new StreamWriter(rutaClientes))
                {
                    swClientes.WriteLine(clientes);
                    swClientes.Close();
                }
                using (StreamWriter swProductos = new StreamWriter(rutaProducto))
                {
                    swProductos.WriteLine(productos);
                    swProductos.Close();
                }
                using (StreamWriter swImportacion = new StreamWriter(rutaImportacion))
                {
                    swImportacion.WriteLine(importacion);
                    swImportacion.Close();
                }
                using (StreamWriter swUsuarios = new StreamWriter(rutaUsuario))
                {
                    swUsuarios.WriteLine(usuarios);
                    swUsuarios.Close();
                }
                using (StreamWriter swStatic = new StreamWriter(rutaStatic))
                {
                    swStatic.WriteLine(variablesEstaticas);
                    swStatic.Close();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Metodos para armar filas de las distintas tablas
        private static string ArmarFilaCliente(IDataRecord fila, string delimitador)
        {
            string linea = "";
            if (fila != null && delimitador != null)
            {
                linea += fila["idCliente"].ToString() + delimitador +
                   fila["rut"].ToString() + delimitador +
                   fila["nomCliente"].ToString() + delimitador +
                   fila["fechaRegistro"].ToString();
            }
            return linea;
        }
        private static string ArmarFilaProducto(IDataRecord fila, string delimitador)
        {
            string linea = "";
            if (fila != null && delimitador != null)
            {
                linea += fila["idProducto"].ToString() + delimitador +
                   fila["nombre"].ToString() + delimitador +
                   fila["peso"].ToString() + delimitador +
                   fila["rut"].ToString();
                ;
            }
            return linea;
        }
        private static string ArmarFilaImportacion(IDataRecord fila, string delimitador)
        {
            string linea = "";
            if (fila != null && delimitador != null)
            {
                linea += fila["idImportacion"].ToString() + delimitador +
                   fila["idProducto"].ToString() + delimitador +
                   fila["FechaIngreso"].ToString() + delimitador +
                   fila["FechaSalida"].ToString() + delimitador +
                   fila["cantUni"].ToString() + delimitador +
                   fila["precioUni"].ToString() + delimitador +
                   fila["estado"].ToString();
                ;
            }
            return linea;
        }
        private static string ArmarFilaUsuario(IDataRecord fila, string delimitador)
        {
            string linea = "";
            if (fila != null && delimitador != null)
            {
                linea += fila["idUsuario"].ToString() + delimitador +
                   fila["ci"].ToString() + delimitador +
                   fila["clave"].ToString() + delimitador +
                   fila["rol"].ToString() + delimitador +
                   fila["email"].ToString();
                ;
            }
            return linea;
        }
        private static string ArmarFilaStatic(IDataRecord fila, string delimitador)
        {
            string linea = "";
            if (fila != null && delimitador != null)
            {
                linea += fila["Lock"].ToString() + delimitador +
                   fila["descuento"].ToString() + delimitador +
                   fila["antiguedadMin"].ToString() + delimitador +
                   fila["porcGanancia"].ToString();
                ;
            }
            return linea;
        }
        //Metodos para armar archivos de texto.
        private static string ArmarStringClientes(string deliminador)
        {
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;

            SqlConnection con = new SqlConnection(strCon);


            string cliente = "SELECT * FROM Cliente";
            SqlCommand com = new SqlCommand(cliente, con);

            StringBuilder contenidoArchivo = new StringBuilder();
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                //Leer el reader y cargar los objetos en la lista de retorno

                while (reader.Read())
                {
                    string linea = ArmarFilaCliente(reader, deliminador);
                    if (!string.IsNullOrEmpty(linea))
                    {
                        contenidoArchivo.AppendLine(linea);
                    }
                }
                return contenidoArchivo.ToString();

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;

            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private static string ArmarStringProductos(string deliminador)
        {
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;

            SqlConnection con = new SqlConnection(strCon);


            string producto = "SELECT * FROM Producto";
            SqlCommand com = new SqlCommand(producto, con);

            StringBuilder contenidoArchivo = new StringBuilder();
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                //Leer el reader y cargar los objetos en la lista de retorno

                while (reader.Read())
                {

                    string linea = ArmarFilaProducto(reader, deliminador);
                    if (!string.IsNullOrEmpty(linea))
                    {
                        contenidoArchivo.AppendLine(linea);
                    }
                }
                return contenidoArchivo.ToString();

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;

            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private static string ArmarStringImportacion(string deliminador)
        {
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;

            SqlConnection con = new SqlConnection(strCon);


            string importacion = "SELECT * FROM Importacion";
            SqlCommand com = new SqlCommand(importacion, con);

            StringBuilder contenidoArchivo = new StringBuilder();
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                //Leer el reader y cargar los objetos en la lista de retorno

                while (reader.Read())
                {

                    string linea = ArmarFilaImportacion(reader, deliminador);
                    if (!string.IsNullOrEmpty(linea))
                    {
                        contenidoArchivo.AppendLine(linea);
                    }
                }
                return contenidoArchivo.ToString();

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;

            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private static string ArmarStringUsuario(string deliminador)
        {
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;

            SqlConnection con = new SqlConnection(strCon);


            string usuario = "SELECT * FROM Usuario";
            SqlCommand com = new SqlCommand(usuario, con);

            StringBuilder contenidoArchivo = new StringBuilder();
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                //Leer el reader y cargar los objetos en la lista de retorno

                while (reader.Read())
                {

                    string linea = ArmarFilaUsuario(reader, deliminador);
                    if (!string.IsNullOrEmpty(linea))
                    {
                        contenidoArchivo.AppendLine(linea);
                    }
                }
                return contenidoArchivo.ToString();

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;

            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
        private static string ArmarStringStatic(string deliminador)
        {
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;

            SqlConnection con = new SqlConnection(strCon);


            string StaticCliente = "SELECT * FROM StaticCliente";
            SqlCommand com = new SqlCommand(StaticCliente, con);

            StringBuilder contenidoArchivo = new StringBuilder();
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                //Leer el reader y cargar los objetos en la lista de retorno

                while (reader.Read())
                {

                    string linea = ArmarFilaStatic(reader, deliminador);
                    if (!string.IsNullOrEmpty(linea))
                    {
                        contenidoArchivo.AppendLine(linea);
                    }
                }
                return contenidoArchivo.ToString();

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.Assert(false, ex.Message);
                return null;

            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
        }
    }
}
