using Restaurante_Delicias.ADO;
using Restaurante_Delicias.Models;
using Microsoft.Data.SqlClient;

namespace Restaurante_Delicias.ADO
{
    public class TipoADO : ITipoADO
    {
        private string conexion;
        public TipoADO()
        {
            conexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("cn");
        }

        public IEnumerable<Tipo> listarTipos()
        {
            List<Tipo> lista = new List<Tipo>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_listarTipo", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Tipo()
                    {
                        id_tipo = dr.GetInt32(0),
                        desc_tipo = dr.GetString(1)
                    });
                }
            }

            return lista;
        }
    }
}
