using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Repositorios.Interfaces;

namespace Repositorios
{
    public class RepoImportacion : IRepoImportacion

    {

        public bool Alta(Importacion importacion)
        {
            bool ret = false;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "INSERT Importacion (idProducto,fechaIngreso,fechaSalida,cantUni,precioUni, estado)  VALUES  (@idProducto, @fechaIngreso, @fechaSalida, @cantUni, @precioUni, @estado);";
            SqlCommand com = new SqlCommand(sql, con);


            com.Parameters.AddWithValue("@idProducto", importacion.Producto.Id);
            com.Parameters.AddWithValue("@fechaIngreso", importacion.FechaIngreso);
            com.Parameters.AddWithValue("@fechaSalida", importacion.FechaSalida);
            com.Parameters.AddWithValue("@cantUni", importacion.CantidadUnidades);
            com.Parameters.AddWithValue("@precioUni", importacion.Precio);
            com.Parameters.AddWithValue("@estado", 'n');

            
            try
            {
                con.Open();
                int afectadas = com.ExecuteNonQuery();
                con.Close();

                ret = afectadas == 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return ret;
        }


        public Importacion BuscarPorId(object id)
        {
            Importacion importacion = null;
            int idBuscado = (int)id;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "SELECT * " +
                         "FROM Cliente, Producto , Importacion " +
                         "WHERE Producto.rut = Cliente.rut AND Producto.idProducto = Importacion.idProducto AND Importacion.idImportacion = @id;";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@id", idBuscado);
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    Cliente unC = new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Rut = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        FechaRegistro = reader.GetDateTime(3)
                    };
                    Producto unP = new Producto
                    {
                        Id = reader.GetInt32(4),
                        Nombre = reader.GetString(5),
                        Peso = reader.GetDecimal(6),
                        Cliente = unC
                    };
                    Importacion i = new Importacion
                    {
                        Id = reader.GetInt32(8),
                        Producto = unP,
                        FechaIngreso = reader.GetDateTime(10),
                        FechaSalida = reader.GetDateTime(11),
                        CantidadUnidades = (int)reader.GetDecimal(12),
                        Precio = reader.GetDecimal(13),
                        Estado = reader.GetString(14)[0]
                    };
                }
                con.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return importacion;
            
        }
        public List<Importacion> TraerTodo()
        {
            List<Importacion> importaciones = new List<Importacion>();
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);


            string sql = "SELECT * " +
                         "FROM Cliente, Producto , Importacion " +
                         "WHERE Producto.rut = Cliente.rut AND Producto.idProducto = Importacion.idProducto;";
            SqlCommand com = new SqlCommand(sql, con);


            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {

                    Cliente unC = new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Rut = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        FechaRegistro = reader.GetDateTime(3)
                    };
                    Producto unP = new Producto
                    {
                        Id = reader.GetInt32(4),
                        Nombre = reader.GetString(5),
                        Peso = reader.GetDecimal(6),
                        Cliente = unC
                    };
                    Importacion i = new Importacion
                    {
                        Id = reader.GetInt32(8),
                        Producto = unP,
                        FechaIngreso = reader.GetDateTime(10),
                        FechaSalida = reader.GetDateTime(11),
                        CantidadUnidades = (int)reader.GetDecimal(12),
                        Precio = reader.GetDecimal(13),
                        Estado = reader.GetString(14)[0]
                    };
                    importaciones.Add(i);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return importaciones;
        }

        public bool Modificacion(Importacion importacion)
        {
            bool ret = false;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "UPDATE Importacion " +
                         "SET idProducto=@idProducto, fechaIngreso=@fechaIngreso, fechaSalida=@fechaSalida, cantUni=@cantUni, precioUni=@precioUni, estado=@estado;" +
                         "WHERE idImportacion=@id";

            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@id", importacion.Id);
            com.Parameters.AddWithValue("@idProducto", importacion.Producto.Id);
            com.Parameters.AddWithValue("@fechaIngreso", importacion.FechaIngreso);
            com.Parameters.AddWithValue("@fechaSalida", importacion.FechaSalida);
            com.Parameters.AddWithValue("@cantUni", importacion.CantidadUnidades);
            com.Parameters.AddWithValue("@precioUni", importacion.Precio);
            com.Parameters.AddWithValue("@estado", 'n');

            try
            {
                con.Open();
                int afectadas = com.ExecuteNonQuery();
                con.Close();

                ret = afectadas == 1;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return ret;
        }

        public bool Baja(object id)
        {
            throw new NotImplementedException();
        }

        public List<Importacion> ImportacionesEnStock()
        {
            List<Importacion> importaciones = new List<Importacion>();
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);


            string sql = "SELECT * " +
                         "FROM Cliente, Producto , Importacion " +
                         "WHERE Producto.rut = Cliente.rut AND Producto.idProducto = Importacion.idProducto AND Importacion.estado = @estado;";

            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@estado", 'n');

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Cliente unC = new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Rut = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        FechaRegistro = reader.GetDateTime(3)
                    };
                    Producto unP = new Producto
                    {
                        Id = reader.GetInt32(4),
                        Nombre = reader.GetString(5),
                        Peso = reader.GetDecimal(6),
                        Cliente = unC
                    };
                    Importacion i = new Importacion
                    {
                        Id = reader.GetInt32(8),
                        Producto = unP, 
                        FechaIngreso = reader.GetDateTime(10),
                        FechaSalida = reader.GetDateTime(11),
                        CantidadUnidades = (int)reader.GetDecimal(12),
                        Precio = reader.GetDecimal(13),
                        Estado = reader.GetString(14)[0]
                    };
                    importaciones.Add(i);
                  
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return importaciones;
        }
        public List<Importacion> ImportacionesEnStockCliente(Object id)
        {
            string idBuscado = (string)id; 
            List<Importacion> importaciones = new List<Importacion>();
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);


            string sql = "SELECT * " +
                         "FROM Cliente, Producto , Importacion " +
                         "WHERE Producto.rut = Cliente.rut AND Producto.idProducto = Importacion.idProducto AND Importacion.estado = @estado AND Cliente.rut = @rut;";

            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@estado", 'n');
            com.Parameters.AddWithValue("@rut", idBuscado);


            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Cliente unC = new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Rut = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        FechaRegistro = reader.GetDateTime(3)
                    };
                    Producto unP = new Producto
                    {
                        Id = reader.GetInt32(4),
                        Nombre = reader.GetString(5),
                        Peso = reader.GetDecimal(6),
                        Cliente = unC
                    };
                    Importacion i = new Importacion
                    {
                        Id = reader.GetInt32(8),
                        Producto = unP,
                        FechaIngreso = reader.GetDateTime(10),
                        FechaSalida = reader.GetDateTime(11),
                        CantidadUnidades = (int)reader.GetDecimal(12),
                        Precio = reader.GetDecimal(13),
                        Estado = reader.GetString(14)[0]
                    };
                    importaciones.Add(i);

                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return importaciones;
        }



    }

}