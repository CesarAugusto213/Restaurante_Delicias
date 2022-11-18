using Microsoft.AspNetCore.Mvc;
using Restaurante_Delicias.ADO;
using Restaurante_Delicias.Models;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

namespace Restaurante_Delicias.Controllers
{
    public class ReporteController : Controller
    {
        private IProductoADO productoADO;

        public ReporteController()
        {
            productoADO = new ProductoADO();
        }

        public IActionResult Reporte()
        {
            IEnumerable<Producto> productos = productoADO.listarProductos();

            return new ViewAsPdf("Reporte", productos)
            {
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                FileName = "reporte de productos.pdf"
            };
        }
    }
}
