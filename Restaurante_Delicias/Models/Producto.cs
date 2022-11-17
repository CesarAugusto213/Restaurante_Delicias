using System.ComponentModel.DataAnnotations;

namespace Restaurante_Delicias.Models
{
    public class Producto
    {
        [Required]
        [Display(Name = "Codigo")]
        public int id_prod { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        public string nom_prod { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        public string desc_prod { get; set; }

        [Required]
        [Display(Name = "Precio")]
        public decimal precio { get; set; }

        [Required]
        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Required]
        [Display(Name = "Codigo Tipo")]
        public int id_tipo { get; set; }

        [Display(Name = "Tipo")]
        public string desc_tipo { get; set; }

        [Required]
        [Display(Name = "Foto")]
        public string foto { get; set; }

        public Producto()
        {
            nom_prod = string.Empty;
            desc_prod = string.Empty;
            desc_tipo = string.Empty;
            foto = string.Empty;
        }
    }
}
