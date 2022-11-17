using Restaurante_Delicias.Models;

namespace Restaurante_Delicias.ADO
{
    public interface IDet_boletaADO
    {
        public string agregarDetBoleta(List<Det_boleta> det_boletas, double total);
        public IEnumerable<Det_boleta> listarDetalles(int num_boleta);
    }
}
