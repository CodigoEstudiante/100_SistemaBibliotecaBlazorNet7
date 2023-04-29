using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Server.Models;
using SistemaBiblioteca.Server.Repositorio.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace SistemaBiblioteca.Server.Repositorio.Implementacion
{
    public class LibroRepositorio : ILibroRepositorio
    {
        private readonly DbbibliotecaBlazorContext _dbContext;

        public LibroRepositorio(DbbibliotecaBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IQueryable<Libro>> Consultar(Expression<Func<Libro, bool>> filtro = null)
        {
            IQueryable<Libro> queryEntidad = filtro == null ? _dbContext.Libros : _dbContext.Libros.Where(filtro);
            return queryEntidad;
        }

        public async Task<Libro> Crear(Libro entidad)
        {
            try
            {
                _dbContext.Set<Libro>().Add(entidad);
                await _dbContext.SaveChangesAsync();
                return entidad;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Editar(Libro entidad)
        {
            try
            {
                _dbContext.Libros.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Libro entidad)
        {
            try
            {
                _dbContext.Libros.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Libro>> Lista()
        {
            try
            {
                return await _dbContext.Libros.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Libro> Obtener(Expression<Func<Libro, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Libros.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
