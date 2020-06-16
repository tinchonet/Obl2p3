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
    public class RepoCliente : IRepoCliente
    {
        public Cliente BuscarPorId(object id)
        {
            Cliente cliente = null;
            string rutBuscado = (string)id;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;

            SqlConnection con = new SqlConnection(strCon);

            string sql = "SELECT * FROM Cliente WHERE rut=@rut;";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@rut", rutBuscado);
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    cliente = new Cliente
                    {
                        Id = reader.GetInt32(0),
                        Rut = reader.GetString(1),
                        Nombre = reader.GetString(2),
                        FechaRegistro = reader.GetDateTime(3)
                    };
                }
                con.Close();
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            return cliente;
        }


        public List<Cliente> TraerTodo()
        {
            List<Cliente> clientes = new List<Cliente>();
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;

            SqlConnection con = new SqlConnection(strCon);


            string sql = "SELECT * FROM Cliente";
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
                        FechaRegistro = reader.GetDateTime(3),
                    };
                    clientes.Add(unC);
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

            return clientes;

        }

       

        public bool Alta(Cliente obj)
        {
            throw new NotImplementedException();
        }

        public bool Baja(object id)
        {
            throw new NotImplementedException();
        }

        public bool Modificacion(Cliente obj)
        {
            throw new NotImplementedException();
        }

        public decimal ObtenerPorcentajeDescuento()
        {
            decimal descuento = 0;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "SELECT StaticCliente.Descuento " +
                         "FROM StaticCliente";
            SqlCommand com = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    descuento = reader.GetDecimal(0);
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

            return descuento;

        }

        public decimal ObtenerPorcentajeGanancia()
        {
            decimal ganancia = 0;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "SELECT StaticCliente.porcGanancia " +
                         "FROM StaticCliente";
            SqlCommand com = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    ganancia = reader.GetDecimal(0); 
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

            return ganancia;
        }

        public int ObtenerAntiguedadMinima()
        {
            int ganancia = 0;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "SELECT StaticCliente.antiguedadMin " +
                         "FROM StaticCliente";
            SqlCommand com = new SqlCommand(sql, con);

            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                    ganancia = (int)reader.GetDecimal(0);
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

            return ganancia;
        }
    }
}




//(Lock, descuento, antiguedadMin) VALUES('X',9,4)