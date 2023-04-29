using SistemaBiblioteca.Shared;

namespace SistemaBiblioteca.Client.Servicios.Contrato
{
    public interface ICategoriaServicio
    {
        Task<ResponseDTO<List<CategoriaDTO>>> Lista();
    }
}
