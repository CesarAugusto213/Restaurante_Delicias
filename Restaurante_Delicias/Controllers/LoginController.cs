using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurante_Delicias.Models;
using Restaurante_Delicias.ADO;

namespace Restaurante_Delicias.Controllers
{
    public class LoginController : Controller
    {
        const string SesionUsuario = "_User";
        private readonly IConfiguration _configuration;
        public static int idUsuario;

        private ILoginADO loginADO;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
            loginADO = new LoginADO();
        }

        public IActionResult Index()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public IActionResult Index(Cliente cliente)
        {
            string con = _configuration["ConnectionStrings:cn"];

            if (string.IsNullOrEmpty(cliente.usuario) || string.IsNullOrEmpty(cliente.pass))
            {
                ModelState.AddModelError(string.Empty, "Ingrese los datos solicitados");
            }
            else
            {
                using (SqlConnection cn = new SqlConnection(con))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand("usp_Login", cn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario", cliente.usuario);
                    cmd.Parameters.AddWithValue("@Clave", cliente.pass);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        HttpContext.Session.SetString(SesionUsuario, cliente.usuario);
                        idUsuario = dr.GetInt32(2);
                        return RedirectToAction("Catalogo", "Ecommerce");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Verifique sus credenciales");
                    }
                }
            }
            return View(cliente);
        }

        //Formulario de Registro
        public async Task<ActionResult> Crear()
        {
            ViewBag.sexo = new SelectList(await Task.Run(() => loginADO.listarSexo()), "id_sexo", "sexo");
            return View(new Cliente());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.sexo = new SelectList(await Task.Run(() => loginADO.listarSexo()), "id_sexo", "sexo");
                return View(cliente);
            }
            else
            {
                ViewBag.sexo = new SelectList(await Task.Run(() => loginADO.listarSexo()), "id_sexo", "sexo");
                ViewBag.mensaje = loginADO.agregar(cliente);
                return View(cliente);
            }
        }

    }
}
