using Restaurante_Delicias.Models;

namespace Restaurante_Delicias.ADO
{
    public interface ITipoADO
    {
        public IEnumerable<Tipo> listarTipos();
    }
}
