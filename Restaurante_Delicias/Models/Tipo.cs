namespace Restaurante_Delicias.Models
{
    public class Tipo
    {
        public int id_tipo { get; set; }
        public string desc_tipo { get; set; }

        public Tipo()
        {
            desc_tipo = string.Empty;
        }
    }
}
