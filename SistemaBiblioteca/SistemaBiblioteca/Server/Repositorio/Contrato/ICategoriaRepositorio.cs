using SistemaBiblioteca.Server.Models;

namespace SistemaBiblioteca.Server.Repositorio.Contrato
{
    public interface ICategoriaRepositorio
    {
        Task<List<Categoria>> Lista();
    }
}
