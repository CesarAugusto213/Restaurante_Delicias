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
                        precio = dr.GetDecimal(5),
                        id_tipo = dr.GetInt32(6),
                        foto = dr.GetString(7)
                    });
                }
            }
            return lista;
        }

        public void eliminarProducto(int? id)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_eliminarProducto", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public string guardarProducto(Producto producto)
        {
            string msg = string.Empty;

            using (SqlConnection cn = new SqlConnection(conexion))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ProductoMerge", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idprod", producto.id_prod);
                    cmd.Parameters.AddWithValue("@nombre", producto.nom_prod);
                    cmd.Parameters.AddWithValue("@desc", producto.desc_prod);
                    cmd.Parameters.AddWithValue("@precio", producto.precio);
                    cmd.Parameters.AddWithValue("@cantidad", producto.cantidad);
                    cmd.Parameters.AddWithValue("@idtipo", producto.id_tipo);
                    cmd.Parameters.AddWithValue("@foto", producto.foto);
                    cn.Open();
                    int c = cmd.ExecuteNonQuery();
                    msg = $"{c} Producto";
                }
                catch(Exception e)
                {
                    msg = e.Message;
                }
            }

            return msg;
        }
    }
}
