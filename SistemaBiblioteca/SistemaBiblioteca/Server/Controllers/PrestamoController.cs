using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaBiblioteca.Server.Models;
using SistemaBiblioteca.Server.Repositorio.Contrato;
using SistemaBiblioteca.Server.Repositorio.Implementacion;
using SistemaBiblioteca.Shared;

namespace SistemaBiblioteca.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPrestamoRepositorio _prestamoRepositorio;
        public PrestamoController(IPrestamoRepositorio prestamoRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _prestamoRepositorio = prestamoRepositorio;
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar(string estadoPrestamo,string codigoLector)
        {
            ResponseDTO<List<PrestamoDTO>> _ResponseDTO = new ResponseDTO<List<PrestamoDTO>>();

            try
            {
                List<PrestamoDTO> listaPrestamo = new List<PrestamoDTO>();
                IQueryable<Prestamo> query = await _prestamoRepositorio.Consultar(
                    p => p.IdEstadoPrestamoNavigation.Descripcion.ToLower().Equals(
                        estadoPrestamo.ToLower() == "todos" ? p.IdEstadoPrestamoNavigation.Descripcion.ToLower() : estadoPrestamo.ToLower())
                    &&
                    p.IdLectorNavigation.Codigo.ToLower().Equals(
                            codigoLector == "na" ? p.IdLectorNavigation.Codigo.ToLower() : codigoLector.ToLower())
                    );

                query = query.Include(e => e.IdEstadoPrestamoNavigation)
                    .Include(lt => lt.IdLectorNavigation)
                    .Include(lb => lb.IdLibroNavigation);

                listaPrestamo = _mapper.Map<List<PrestamoDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<PrestamoDTO>>() { status = true, msg = "ok", value = listaPrestamo };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<PrestamoDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<PrestamoDTO>> _ResponseDTO = new ResponseDTO<List<PrestamoDTO>>();

            try
            {
                List<PrestamoDTO> listaPrestamo = new List<PrestamoDTO>();
                IQueryable<Prestamo> query = await _prestamoRepositorio.Consultar();
                query = query.Include(e => e.IdEstadoPrestamoNavigation)
                    .Include(lt => lt.IdLectorNavigation)
                    .Include(lb => lb.IdLibroNavigation);

                listaPrestamo = _mapper.Map<List<PrestamoDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<PrestamoDTO>>() { status = true, msg = "ok", value = listaPrestamo };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<PrestamoDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] PrestamoDTO request)
        {
            ResponseDTO<PrestamoDTO> _ResponseDTO = new ResponseDTO<PrestamoDTO>();
            try
            {
                Prestamo _prestamo = _mapper.Map<Prestamo>(request);

                Prestamo _prestamoCreado = await _prestamoRepositorio.Crear(_prestamo);

                if (_prestamoCreado.IdLector != 0)
                    _ResponseDTO = new ResponseDTO<PrestamoDTO>() { status = true, msg = "ok", value = _mapper.Map<PrestamoDTO>(_prestamoCreado) };
                else
                    _ResponseDTO = new ResponseDTO<PrestamoDTO>() { status = false, msg = "No se pudo crear el prestamo" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<PrestamoDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] PrestamoDTO request)
        {
            ResponseDTO<PrestamoDTO> _ResponseDTO = new ResponseDTO<PrestamoDTO>();
            try
            {
                Prestamo _prestamo = _mapper.Map<Prestamo>(request);
                Prestamo _prestamoParaEditar = await _prestamoRepositorio.Obtener(u => u.IdPrestamo == _prestamo.IdPrestamo);

                if (_prestamoParaEditar != null)
                {

                    _prestamoParaEditar.IdEstadoPrestamo = _prestamo.IdEstadoPrestamo;
                    _prestamoParaEditar.FechaConfirmacionDevolucion = _prestamo.FechaConfirmacionDevolucion;
                    _prestamoParaEditar.EstadoRecibido = _prestamo.EstadoRecibido;

                    bool respuesta = await _prestamoRepositorio.Editar(_prestamoParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<PrestamoDTO>() { status = true, msg = "ok", value = _mapper.Map<PrestamoDTO>(_prestamoParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<PrestamoDTO>() { status = false, msg = "No se pudo editar el prestamo" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<PrestamoDTO>() { status = false, msg = "No se encontró el prestamo" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<PrestamoDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            ResponseDTO<string> _ResponseDTO = new ResponseDTO<string>();
            try
            {
                Prestamo _prestamoEliminar = await _prestamoRepositorio.Obtener(u => u.IdLector == id);

                if (_prestamoEliminar != null)
                {

                    bool respuesta = await _prestamoRepositorio.Eliminar(_prestamoEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el lector", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }





    }
}
