using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Restaurante_Delicias.ADO;
using Restaurante_Delicias.Models;

namespace Restaurante_Delicias.Controllers
{
    public class MantenimientoController : Controller
    {
        private IProductoADO productoADO;
        private IDet_boletaADO det_BoletaADO;
        private ITipoADO tipoADO;

        public MantenimientoController()
        {
            productoADO = new ProductoADO();
            det_BoletaADO = new Det_boletaADO();
            tipoADO = new TipoADO();
        }


        public async Task<IActionResult> Mantenimiento()
        {
            if (HttpContext.Session.GetString("Carrito") == null)
            {
                HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(new List<Det_boleta>()));
                ViewBag.count = 0;
            }
            else
            {
                contador();
            }

            return View(await Task.Run(() => productoADO.listarProductos()));
        }

        public async Task<ActionResult> Crear()
        {
            contador();
            ViewBag.tipos = new SelectList(await Task.Run(() => tipoADO.listarTipos()), "id_tipo", "desc_tipo");
            return View(new Producto());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.tipos = new SelectList(await Task.Run(() => tipoADO.listarTipos()), "id_tipo", "desc_tipo");
                return View(producto);
            }
            else
            {
                ViewBag.tipos = new SelectList(await Task.Run(() => tipoADO.listarTipos()), "id_tipo", "desc_tipo");
                ViewBag.mensaje = productoADO.guardarProducto(producto) + " Insertado";
                return View(producto);
            }
        }

        Producto? Obtener(int id)
        {
            Producto? model = productoADO.listarProductos().Where(p => p.id_prod == id).FirstOrDefault();
            return model;
        }

        public async Task<ActionResult> Editar(int id)
        {
            contador();
            Producto? producto = Obtener(id);
            if(producto == null)
            {
                return View("Error");
            }
            else
            {
                ViewBag.tipos = new SelectList(await Task.Run(() => tipoADO.listarTipos()), "id_tipo", "desc_tipo");
                return View(producto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Producto producto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.tipos = new SelectList(await Task.Run(() => tipoADO.listarTipos()), "id_tipo", "desc_tipo");
                return View(producto);
            }
            else
            {
                ViewBag.tipos = new SelectList(await Task.Run(() => tipoADO.listarTipos()), "id_tipo", "desc_tipo");
                ViewBag.mensaje = productoADO.guardarProducto(producto) + " Editado"; ;
                return View(producto);
            }
        }

        public IActionResult Eliminar(int? id)
        {
            if(id == null)
            {
                return View("Error");
            }
            else
            {
                productoADO.eliminarProducto(id);
            }

            return RedirectToAction("Mantenimiento");
        }

        public void contador()
        {
            IEnumerable<Det_boleta> carrito = JsonConvert.DeserializeObject<List<Det_boleta>>(HttpContext.Session.GetString("Carrito"));
            ViewBag.count = carrito.Count();
        }
    }
}
