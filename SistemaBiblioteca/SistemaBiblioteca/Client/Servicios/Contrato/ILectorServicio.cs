using SistemaBiblioteca.Shared;

namespace SistemaBiblioteca.Client.Servicios.Contrato
{
    public interface ILectorServicio
    {
        Task<ResponseDTO<List<LectorDTO>>> Lista();
        Task<ResponseDTO<LectorDTO>> Obtener(int idLector);
        Task<ResponseDTO<LectorDTO>> Crear(LectorDTO entidad);
        Task<bool> Editar(LectorDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
