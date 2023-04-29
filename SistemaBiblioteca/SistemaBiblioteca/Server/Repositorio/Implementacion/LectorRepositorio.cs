using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Server.Models;
using SistemaBiblioteca.Server.Repositorio.Contrato;
using System.Linq;
using System.Linq.Expressions;

namespace SistemaBiblioteca.Server.Repositorio.Implementacion
{
    public class LectorRepositorio : ILectorRepositorio
    {
        private readonly DbbibliotecaBlazorContext _dbContext;

        public LectorRepositorio(DbbibliotecaBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Lector>> Consultar(Expression<Func<Lector, bool>> filtro = null)
        {
            IQueryable<Lector> queryEntidad = filtro == null ? _dbContext.Lectors : _dbContext.Lectors.Where(filtro);
            return queryEntidad;
        }

        public async Task<Lector> Crear(Lector entidad)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                int CantidadDigitos = 5;
                try
                {
                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.First(x => x.Tipo == "Lector");

                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaRegistro = DateTime.Now;

                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();


                    string ceros = string.Concat(Enumerable.Repeat("0", CantidadDigitos));
                    string codigo = ceros + correlativo.UltimoNumero.ToString();
                    codigo = correlativo.Prefijo + codigo.Substring(codigo.Length - CantidadDigitos, CantidadDigitos);

                    entidad.Codigo = codigo;

                    await _dbContext.Lectors.AddAsync(entidad);
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

        public async Task<bool> Editar(Lector entidad)
        {
            try
            {
                _dbContext.Lectors.Update(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(Lector entidad)
        {
            try
            {
                _dbContext.Lectors.Remove(entidad);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<Lector>> Lista()
        {
            try
            {
                return await _dbContext.Lectors.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<Lector> Obtener(Expression<Func<Lector, bool>> filtro = null)
        {
            try
            {
                return await _dbContext.Lectors.Where(filtro).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
