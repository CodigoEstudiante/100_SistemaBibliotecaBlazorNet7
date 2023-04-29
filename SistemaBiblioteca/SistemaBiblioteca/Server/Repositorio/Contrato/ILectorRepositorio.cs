using SistemaBiblioteca.Server.Models;
using System.Linq.Expressions;

namespace SistemaBiblioteca.Server.Repositorio.Contrato
{
    public interface ILectorRepositorio
    {
        Task<List<Lector>> Lista();
        Task<Lector> Obtener(Expression<Func<Lector, bool>> filtro = null);
        Task<Lector> Crear(Lector entidad);
        Task<bool> Editar(Lector entidad);
        Task<bool> Eliminar(Lector entidad);
        Task<IQueryable<Lector>> Consultar(Expression<Func<Lector, bool>> filtro = null);
    }
}
