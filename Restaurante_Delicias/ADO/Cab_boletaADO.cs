using Microsoft.Data.SqlClient;
using Restaurante_Delicias.Models;
using Restaurante_Delicias.Controllers;

namespace Restaurante_Delicias.ADO
{
    public class Cab_boletaADO : ICab_boletaADO
    {
        private string conexion;

        public Cab_boletaADO()
        {
            conexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("cn");
        }

        public IEnumerable<Cab_boleta> listarBoleta()
        {
            List<Cab_boleta> lista = new List<Cab_boleta>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_reporteBoleta @cliente", cn);
                cmd.Parameters.AddWithValue("@cliente", LoginController.idUsuario);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Cab_boleta()
                    {
                        num_boleta = dr.GetInt32(0),
                        fecha_boleta = dr.GetDateTime(1),
                        total = dr.GetDecimal(2),
                        id_cliente = dr.GetInt32(3)
                    });
                }
            }
            return lista;
        }
    }
}
