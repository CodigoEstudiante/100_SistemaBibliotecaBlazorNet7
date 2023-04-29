namespace SistemaBiblioteca.Server.Repositorio.Contrato
{
    public interface IDashBoardRepositorio
    {
        Task<int> TotalLibros();
        Task<int> TotalLectores();
        Task<int> PrestamosRegistrados();
        Task<int> PrestamosPendientes();
    }
}
