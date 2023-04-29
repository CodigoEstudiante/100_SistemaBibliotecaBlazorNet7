using SistemaBiblioteca.Shared;

namespace SistemaBiblioteca.Client.Servicios.Contrato
{
    public interface IPrestamoServicio
    {
        Task<ResponseDTO<List<PrestamoDTO>>> Lista();
        Task<ResponseDTO<List<PrestamoDTO>>> Buscar(string estadoPrestamo, string codigoLector);
        Task<ResponseDTO<PrestamoDTO>> Obtener(int idPrestamo);
        Task<ResponseDTO<PrestamoDTO>> Crear(PrestamoDTO entidad);
        Task<bool> Editar(PrestamoDTO entidad);
        Task<bool> Eliminar(int id);

    }
}
