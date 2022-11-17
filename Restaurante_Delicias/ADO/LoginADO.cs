using Microsoft.Data.SqlClient;
using Restaurante_Delicias.Models;

namespace Restaurante_Delicias.ADO
{
    public class LoginADO : ILoginADO
    {
        private string conexion;

        public LoginADO()
        {
            conexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("cn");
        }

        public string agregar(Cliente cliente)
        {
            string mensaje = string.Empty;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_clienteAgregar", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                    cmd.Parameters.AddWithValue("@apellido", cliente.apellido);
                    cmd.Parameters.AddWithValue("@usuario", cliente.usuario);
                    cmd.Parameters.AddWithValue("@pass", cliente.pass);
                    cmd.Parameters.AddWithValue("@id_sexo", cliente.id_sexo);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    mensaje = $"Se ha insertado {c} Usuario";
                }
                catch (Exception e)
                {
                    mensaje = e.Message;
                }
            }
            return mensaje;
        }

        public IEnumerable<Sexo> listarSexo()
        {
            List<Sexo> lista = new List<Sexo>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_listarSexo", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Sexo()
                    {
                        id_sexo = dr.GetInt32(0),
                        sexo = dr.GetString(1)
                    });
                }
            }
            return lista;
        }
    }
}
