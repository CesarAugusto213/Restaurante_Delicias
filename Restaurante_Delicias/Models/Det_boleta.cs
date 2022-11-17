using System.ComponentModel.DataAnnotations;

namespace Restaurante_Delicias.Models
{
    public class Det_boleta
    {
        [Display(Name = "Boleta")]
        public int num_boleta { get; set; }

        [Display(Name = "Codigo")]
        public int id_prod { get; set; }

        [Display(Name = "Nombre")]
        public string nom_prod { get; set; }

        [Display(Name = "Precio")]
        public decimal precio { get; set; }

        [Display(Name = "Cantidad")]
        public int cantidad { get; set; }

        [Display(Name = "Subtotal")]
        public decimal subtotal { get; set; }

        [Display(Name = "Precio S/.")]
        public decimal importe 
        { 
            get 
            {
                return cantidad * precio;
            } 
        }

        [Display(Name = "Foto")]
        public string foto
        {
            get
            {
                return string.Format("~/img/productos/{0}.jpg", this.id_prod);
            }
        }

        public Det_boleta()
        {
            nom_prod = string.Empty;
        }

    }
}
