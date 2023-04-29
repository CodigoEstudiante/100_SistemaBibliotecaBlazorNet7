using SistemaBiblioteca.Shared;

namespace SistemaBiblioteca.Client.Servicios.Contrato
{
    public interface ILibroServicio
    {
        Task<ResponseDTO<List<LibroDTO>>> Lista();
        Task<ResponseDTO<LibroDTO>> Obtener(int idLibro);
        Task<ResponseDTO<List<LibroDTO>>> Buscar(string valor);
        Task<ResponseDTO<LibroDTO>> Crear(LibroDTO entidad);
        Task<bool> Editar(LibroDTO entidad);
        Task<bool> Eliminar(int id);
    }
}
