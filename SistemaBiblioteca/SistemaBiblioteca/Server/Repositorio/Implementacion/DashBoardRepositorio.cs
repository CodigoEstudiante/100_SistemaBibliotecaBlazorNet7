using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Server.Models;
using SistemaBiblioteca.Server.Repositorio.Contrato;

namespace SistemaBiblioteca.Server.Repositorio.Implementacion
{
    public class DashBoardRepositorio : IDashBoardRepositorio
    {
        private readonly DbbibliotecaBlazorContext _dbContext;

        public DashBoardRepositorio(DbbibliotecaBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> PrestamosPendientes()
        {
            try
            {
                IQueryable<Prestamo> query = _dbContext.Prestamos;
                int total = query.Where(p => p.IdEstadoPrestamo == 1).Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> PrestamosRegistrados()
        {
            try
            {
                IQueryable<Prestamo> query = _dbContext.Prestamos;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalLectores()
        {
            try
            {
                IQueryable<Lector> query = _dbContext.Lectors;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> TotalLibros()
        {
            try
            {
                IQueryable<Libro> query = _dbContext.Libros;
                int total = query.Count();
                return total;
            }
            catch
            {
                throw;
            }
        }
    }
}
