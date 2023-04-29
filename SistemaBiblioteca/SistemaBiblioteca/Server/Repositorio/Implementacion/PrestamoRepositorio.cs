using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Server.Models;
using SistemaBiblioteca.Server.Repositorio.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace SistemaBiblioteca.Server.Repositorio.Implementacion
{
    public class PrestamoRepositorio : IPrestamoRepositorio
    {
        private readonly DbbibliotecaBlazorContext _dbContext;

        public PrestamoRepositorio(DbbibliotecaBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IQueryable<Prestamo>> Consultar(Expression<Func<Prestamo, bool>> filtro = null)
        {
            IQueryable<Prestamo> queryEntidad = filtro == null ? _dbContext.Prestamos : _dbContext.Prestamos.Where(filtro);
            return queryEntidad;
        }

        public async Task<Prestamo> Crear(Prestamo entidad)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                int CantidadDigitos = 5;
                try
                {
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.First(x => x.Tipo == "Prestamo");

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string codigo = ceros + correlativo.UltimoNumero.ToString();
                    codigo = correlativo.Prefijo + codigo.Substring(codigo.Length - CantidadDigitos, CantidadDigitos);

                    entidad.Codigo = codigo;

                    await _dbContext.Prestamos.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    return entidad;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public async Task<bool> Editar(Prestamo entidad)
        {
            try
            {
                _dbContext.Prestamos.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Prestamo entidad)
        {
            try
            {
                _dbContext.Prestamos.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Prestamo>> Lista()
        {

            try
            {
                return await _dbContext.Prestamos.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Prestamo> Obtener(Expression<Func<Prestamo, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Prestamos.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
