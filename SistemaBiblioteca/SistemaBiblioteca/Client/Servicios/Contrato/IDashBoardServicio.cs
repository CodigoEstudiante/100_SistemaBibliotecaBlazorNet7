using SistemaBiblioteca.Shared;

namespace SistemaBiblioteca.Client.Servicios.Contrato
{
    public interface IDashBoardServicio
    {
        Task<ResponseDTO<DashBoardDTO>> Resumen();
    }
}
