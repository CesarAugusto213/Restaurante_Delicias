using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Restaurante_Delicias.ADO;
using Restaurante_Delicias.Models;

namespace Restaurante_Delicias.Controllers
{
    public class EcommerceController : Controller
    {
        //Definir Interfaces
        private IProductoADO productoADO;
        private IDet_boletaADO det_BoletaADO;
        private ICab_boletaADO cab_BoletaADO;

        public EcommerceController()
        {
            productoADO = new ProductoADO();
            det_BoletaADO = new Det_boletaADO();
            cab_BoletaADO = new Cab_boletaADO();
        }

        //Listar Catalogo
        public async Task<IActionResult> Catalogo()
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

        //Obtener Productos
        public Producto Obtener(int id)
        {
            Producto obj = productoADO.listarProductos().Where(p => p.id_prod == id).FirstOrDefault();
            if(obj == null)
            {
                obj = new Producto();
            }
            return obj;
        }

        public async Task<IActionResult> Agregar(int id)
        {
            contador();
            return View(await Task.Run(() => Obtener(id)));
        }

        [HttpPost]
        public ActionResult Agregar(int id, int cantidad)
        {
            Producto obj = Obtener(id);
            if (obj != null)
            {
                if (cantidad <= 0)
                {
                    ViewBag.mensaje = "Ingrese una cantidad mayor a cero.";
                    return View(obj);
                }

                if (obj.cantidad < cantidad)
                {
                    ViewBag.mensaje = string.Format("El producto solo dispone de {0} unidades", obj.cantidad);
                    return View(obj);
                }
                else
                {
                    Det_boleta detalle = new Det_boleta()
                    {
                        id_prod = obj.id_prod,
                        nom_prod = obj.nom_prod,
                        precio = obj.precio,
                        cantidad = cantidad
                    };

                    List<Det_boleta> carrito = JsonConvert.DeserializeObject<List<Det_boleta>>(HttpContext.Session.GetString("Carrito"));

                    // Verificar si el producto existe
                    Det_boleta temp = carrito.Find(i => i.id_prod == id);
                    if (temp == null)
                    {
                        carrito.Add(detalle);
                    }
                    else
                    {
                        temp.cantidad = detalle.cantidad;
                    }

                    HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(carrito));
                    return RedirectToAction("Carrito");
                }
            }
            else
            {
                return RedirectToAction("Catalogo");
            }
        }

        //Agregar a carrito
        public ActionResult Carrito()
        {
            if (HttpContext.Session.GetString("Carrito") == null)
            {
                return RedirectToAction("Catalogo");
            }
            IEnumerable<Det_boleta> carrito = JsonConvert.DeserializeObject<List<Det_boleta>>(HttpContext.Session.GetString("Carrito"));
            if (carrito.Count() == 0)
            {
                return RedirectToAction("Catalogo");
            }

            ViewBag.count = carrito.Count();
            ViewBag.total = carrito.Sum(i => i.importe);
            return View(carrito);
        }

        //Quitar de carrito
        public IActionResult Quitar(int id)
        {
            List<Det_boleta> carrito = JsonConvert.DeserializeObject<List<Det_boleta>>(HttpContext.Session.GetString("Carrito"));
            if (carrito == null || id <= 0)
            {
                return RedirectToAction("Catalogo");
            }
            Det_boleta obj = carrito.Where(i => i.id_prod == id).First();
            if (obj != null)
            {
                carrito.Remove(obj);
                HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(carrito));
            }
            else
            {
                ViewBag.mensaje = "Ocurrio un error al quitar el producto";
            }
            return RedirectToAction("Carrito");
        }

        //Facturar
        public IActionResult Facturar(double total)
        {
            List<Det_boleta> carrito = JsonConvert.DeserializeObject<List<Det_boleta>>(HttpContext.Session.GetString("Carrito"));

            if (carrito == null)
            {
                return RedirectToAction("Catalogo");
            }

            ViewBag.mensaje = "Mensaje: " + det_BoletaADO.agregarDetBoleta(carrito, total);
            return RedirectToAction("ReporteBoleta");
        }

        public async Task<IActionResult> ReporteBoleta()
        {
            HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(new List<Det_boleta>()));

            return View(await Task.Run(() => cab_BoletaADO.listarBoleta()));
        }

        public async Task<IActionResult> ReporteDetBoleta(int num_boleta)
        {
            return View(await Task.Run(() => det_BoletaADO.listarDetalles(num_boleta)));
        }

        public void contador()
        {
            IEnumerable<Det_boleta> carrito = JsonConvert.DeserializeObject<List<Det_boleta>>(HttpContext.Session.GetString("Carrito"));
            ViewBag.count = carrito.Count();
        }
    }
}
