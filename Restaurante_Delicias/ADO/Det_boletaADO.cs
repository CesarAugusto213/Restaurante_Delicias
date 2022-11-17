using Microsoft.Data.SqlClient;
using Restaurante_Delicias.Models;
using Restaurante_Delicias.Controllers;

namespace Restaurante_Delicias.ADO
{
    public class Det_boletaADO : IDet_boletaADO
    {
        private string conexion;

        public Det_boletaADO()
        {
            conexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("cn");
        }

        public string agregarDetBoleta(List<Det_boleta> det_boletas, double total)
        {
            string mensaje = string.Empty;
            //int x, y = 0;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_agregarCabBoleta", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@id_cliente", LoginController.idUsuario);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();

                    foreach (var item in det_boletas)
                    {
                        SqlCommand cmd2 = new SqlCommand("usp_agregarDetBoleta", cn);
                        cmd2.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd2.Parameters.AddWithValue("@id_prod", item.id_prod);
                        cmd2.Parameters.AddWithValue("@cantidad", item.cantidad);
                        cmd2.Parameters.AddWithValue("@importe", item.importe);
                        cn.Open();
                        cmd2.ExecuteNonQuery();
                        cn.Close();
                    }

                    mensaje = "Transaccion Completada";

                }
                catch(Exception e)
                {
                    mensaje = e.Message;
                }
            }

            return mensaje;
        }

        public IEnumerable<Det_boleta> listarDetalles(int num_boleta)
        {
            List<Det_boleta> lista = new List<Det_boleta>();

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_reportDetalle @cliente", cn);
                cmd.Parameters.AddWithValue("@cliente", num_boleta);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Det_boleta()
                    {
                        num_boleta = dr.GetInt32(0),
                        id_prod = dr.GetInt32(1),
                        cantidad = dr.GetInt32(2),
                        subtotal = dr.GetDecimal(3)
                    });
                }
            }
            return lista;
        }
    }
}
