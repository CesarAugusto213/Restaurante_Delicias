using Restaurante_Delicias.Models;

namespace Restaurante_Delicias.ADO
{
    public interface ILoginADO
    {
        public IEnumerable<Sexo> listarSexo();

        public string agregar(Cliente cliente);
    }
}
