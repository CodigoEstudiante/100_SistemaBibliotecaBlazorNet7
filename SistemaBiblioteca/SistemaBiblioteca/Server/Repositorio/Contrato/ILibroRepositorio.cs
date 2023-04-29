using SistemaBiblioteca.Server.Models;
using System.Linq.Expressions;

namespace SistemaBiblioteca.Server.Repositorio.Contrato
{
    public interface ILibroRepositorio
    {
        Task<List<Libro>> Lista();
        Task<Libro> Obtener(Expression<Func<Libro, bool>> filtro = null);
        Task<Libro> Crear(Libro entidad);
        Task<bool> Editar(Libro entidad);
        Task<bool> Eliminar(Libro entidad);
        Task<IQueryable<Libro>> Consultar(Expression<Func<Libro, bool>> filtro = null);
    }
}
