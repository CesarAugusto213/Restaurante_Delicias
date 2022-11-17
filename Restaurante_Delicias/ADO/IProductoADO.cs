using Restaurante_Delicias.Models;

namespace Restaurante_Delicias.ADO
{
    public interface IProductoADO
    {
        public IEnumerable<Producto> listarProductos();

        string guardarProducto(Producto producto);

        public void eliminarProducto(int? id);
    }
}
