using System.ComponentModel.DataAnnotations;

namespace Restaurante_Delicias.Models
{
    public class Cab_boleta
    {
        [Display(Name = "Numero de Boleta")]
        public int num_boleta { get; set; }

        [Display(Name = "Fecha")]
        public DateTime fecha_boleta { get; set; }

        [Display(Name = "Total")]
        public decimal total { get; set; }

        [Display(Name = "Codigo del Cliente")]
        public int id_cliente { get; set; }
    }
}
