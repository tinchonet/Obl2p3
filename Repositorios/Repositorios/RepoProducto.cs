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
    public class RepoProducto : IRepoProducto
    {
        public bool Alta(Producto obj)
        {
            bool ret = false;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "INSERT Producto (nombre,peso,rut)  VALUES   (@nombre, @peso, @rut);";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@nombre", obj.Nombre);
            com.Parameters.AddWithValue("@peso", obj.Peso);
            com.Parameters.AddWithValue("@rut", obj.Cliente.Rut);


            try
            {
                con.Open();
                int afectadas = com.ExecuteNonQuery();
                con.Close();
                ret = afectadas == 1;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return ret;
        }

        public bool Baja(object id)
        {
            bool ret = false;
            int idBuscado = (int)id;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);
            string sql = "delete from Producto where idProducto=@id;";
            SqlCommand com = new SqlCommand(sql, con);
            com.Parameters.AddWithValue("@id", idBuscado);
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

        public Producto BuscarPorId(object id)
        {
            Producto producto = null;
            Cliente cliente = null;
            int idBuscado = (int)id;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "SELECT * " +
                         "FROM Producto, Cliente " +
                         "WHERE idProducto=@id AND Producto.rut = Cliente.rut;";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@id", idBuscado);
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                     cliente = new Cliente
                    {
                        Id = reader.GetInt32(4),
                        Rut = reader.GetString(5),
                        Nombre = reader.GetString(6),
                        FechaRegistro = reader.GetDateTime(7)
                    };
                    producto = new Producto
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Peso = reader.GetDecimal(2),
                        Cliente = cliente
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

            return producto;
        }


        public bool Modificacion(Producto obj)
        {
            bool ret = false;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "UPDATE Producto " +
                         "SET nombre=@nombre, peso=@peso, rut=@rut;" +
                         "WHERE idImportacion=@id";

            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@nombre", obj.Nombre);
            com.Parameters.AddWithValue("@peso", obj.Peso);
            com.Parameters.AddWithValue("@rut", obj.Cliente.Rut);

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

        public List<Producto> TraerTodo()
        {
            List<Producto> productos = new List<Producto>();
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);


            string sql = "SELECT * " +
                         "FROM Producto, Cliente" +
                         "WHERE Producto.rut = Cliente.rut ";
            SqlCommand com = new SqlCommand(sql, con);


            try
            {
                con.Open();
                RepoCliente repoCliente = new RepoCliente();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Cliente unC = new Cliente
                    {
                        Id = reader.GetInt32(4),
                        Rut = reader.GetString(5),
                        Nombre = reader.GetString(6),
                        FechaRegistro = reader.GetDateTime(7)
                    };
                    Producto unP = new Producto
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Peso = reader.GetDecimal(2),
                        Cliente = unC
                    };
                    productos.Add(unP);
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
            return productos;

        }
        public List<Producto> ProductosCliente(string rut)
        {
            List<Producto> productos = new List<Producto>();
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);
            string sql = "SELECT * " +
                         "FROM Producto, Cliente " +
                         "WHERE  Producto.rut = Cliente.rut AND Cliente.rut = @rut;";


           
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@rut", rut);

            try
            {
                con.Open();
                RepoCliente repoCliente = new RepoCliente();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Id = reader.GetInt32(4),
                        Rut = reader.GetString(5),
                        Nombre = reader.GetString(6),
                        FechaRegistro = reader.GetDateTime(7)
                    };
                    Producto unP = new Producto
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Peso = reader.GetDecimal(2),
                        Cliente = cliente
                    };
                    productos.Add(unP);
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
            return productos;
        }

        public List<Producto> ProductosEnStock()
        {
            int stock = 0;
            List<Producto> productos = new List<Producto>();
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);
            //InnerJoin para traer resultados que no tienen stock
            string sql = "SELECT Producto.idProducto,Producto.Nombre,(SELECT CAST(SUM(Importacion.cantUni) as INT) " +
                         "                                            FROM Importacion " +
                         "                                            WHERE Producto.idProducto = Importacion.idProducto " +
                         "                                            AND Importacion.estado = 'n') " +
                         "FROM Producto;";


            SqlCommand com = new SqlCommand(sql, con);


            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.IsDBNull(2))
                    {
                         stock = 0;
                    }
                    else
                    {
                        stock = reader.GetInt32(2);
                    }
                    Producto unP = new Producto
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Stock = stock
                    };
                    productos.Add(unP);
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
            return productos;

        }


    }
}
