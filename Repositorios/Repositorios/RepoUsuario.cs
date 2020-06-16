using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Repositorios
{
    public class RepoUsuario : IRepositorio<Usuario>
    {
        public bool Alta(Usuario usuario)
        {
            bool ret = false;
            if (Usuario.ComplejidadPassword(usuario.Clave) == "ok" && Usuario.ValidarCedula(usuario.Ci))
            {

                string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
                SqlConnection con = new SqlConnection(strCon);

                string sql = "INSERT Usuario (ci,clave,rol,email)  VALUES   (@ci, @clave, @rol,@email);";
                SqlCommand com = new SqlCommand(sql, con);

                com.Parameters.AddWithValue("@ci", usuario.Ci);
                com.Parameters.AddWithValue("@clave", usuario.Clave);
                com.Parameters.AddWithValue("@rol", usuario.Rol);
                com.Parameters.AddWithValue("@email", usuario.Email);

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
            }
            return ret;
        }

        public bool Baja(object id)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarPorId(object id)
        {
            Usuario usuario = null;
            string ciBuscada = (string)id;
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;
            SqlConnection con = new SqlConnection(strCon);

            string sql = "SELECT * FROM Usuario WHERE Ci=@ci";
            SqlCommand com = new SqlCommand(sql, con);

            com.Parameters.AddWithValue("@ci", ciBuscada);
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    usuario = new Usuario
                    {
                        Id = reader.GetInt32(0),
                        Ci = reader.GetString(1),
                        Clave = reader.GetString(2),
                        Rol = reader.GetString(3)
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

            return usuario;
        }

        public bool Modificacion(Usuario obj)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> TraerTodo()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string strCon = ConfigurationManager.ConnectionStrings["stringConBD"].ConnectionString;

            SqlConnection con = new SqlConnection(strCon);


            string sql = "SELECT * FROM Usuario";
            SqlCommand com = new SqlCommand(sql, con);


            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    Usuario usuario = new Usuario
                    {
                        Id = (int)reader.GetDecimal(0),
                        Ci = reader.GetString(1),
                        Clave = reader.GetString(2),
                        Rol = reader.GetString(3),
                    };
                    usuarios.Add(usuario);
                }
                con.Close();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return usuarios;
        }
    }
}
