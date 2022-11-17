using System.ComponentModel.DataAnnotations;

namespace Restaurante_Delicias.Models
{
    public class Cliente
    {
        [Required]
        [Display(Name = "Codigo")]
        public int id_cliente { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        public string apellido { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public string usuario { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        public string pass { get; set; }

        [Required]
        [Display(Name = "Sexo")]
        public int id_sexo { get; set; }

        public Cliente()
        {
            nombre = string.Empty;
            apellido = string.Empty;
            usuario = string.Empty;
            pass = string.Empty;
        }
    }
}
