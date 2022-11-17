using Microsoft.Data.SqlClient;
using Restaurante_Delicias.Models;

namespace Restaurante_Delicias.ADO
{
    public class ProductoADO : IProductoADO
    {
        private string conexion;

        public ProductoADO()
        {
            conexion = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("cn");
        }

        public IEnumerable<Producto> listarProductos()
        {
            List<Producto> lista = new List<Producto>();

            using(SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("usp_listarProductos", cn);
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Producto()
                    {
                        id_prod = dr.GetInt32(0),
                        nom_prod = dr.GetString(1),
                        desc_prod = dr.GetString(2),
                        desc_tipo = dr.GetString(3),
                        cantidad = dr.GetInt32(4),
                        precio = dr.GetDecimal(5)
                    });
                }
            }
            return lista;
        }
    }
}
