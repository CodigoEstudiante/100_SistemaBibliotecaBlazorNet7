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
    public class LibroController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILibroRepositorio _libroRepositorio;
        public LibroController(ILibroRepositorio libroRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _libroRepositorio = libroRepositorio;
        }


        [HttpGet]
        [Route("Obtener/{idLibro}")]
        public async Task<IActionResult> Obtener(int idLibro)
        {
            ResponseDTO<LibroDTO> _ResponseDTO = new ResponseDTO<LibroDTO>();

            try
            {
                LibroDTO libroDTO;
                Libro encontrado = await _libroRepositorio.Obtener(l => l.IdLibro == idLibro);

                if (encontrado != null)
                {
                    libroDTO = _mapper.Map<LibroDTO>(encontrado);
                    _ResponseDTO = new ResponseDTO<LibroDTO>() { status = true, msg = "ok", value = libroDTO };
                }
                else
                    _ResponseDTO = new ResponseDTO<LibroDTO>() { status = false, msg = "", value = null };


                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<LibroDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Buscar")]
        public async Task<IActionResult> Buscar(string valor)
        {
            ResponseDTO<List<LibroDTO>> _ResponseDTO = new ResponseDTO<List<LibroDTO>>();

            try
            {
                List<LibroDTO> listaLibro = new List<LibroDTO>();
                IQueryable<Libro> query = await _libroRepositorio.Consultar(l => l.Titulo!.ToLower().Contains(valor.ToLower()));
                query = query.Include(r => r.IdCategoriaNavigation);

                listaLibro = _mapper.Map<List<LibroDTO>>(query.ToList());

                if (listaLibro.Count > 0)
                    _ResponseDTO = new ResponseDTO<List<LibroDTO>>() { status = true, msg = "ok", value = listaLibro };
                else
                    _ResponseDTO = new ResponseDTO<List<LibroDTO>>() { status = false, msg = "", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<LibroDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<LibroDTO>> _ResponseDTO = new ResponseDTO<List<LibroDTO>>();

            try
            {
                List<LibroDTO> listaLibro = new List<LibroDTO>();
                IQueryable<Libro> query = await _libroRepositorio.Consultar();
                query = query.Include(r => r.IdCategoriaNavigation);
                listaLibro = _mapper.Map<List<LibroDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<LibroDTO>>() { status = true, msg = "ok", value = listaLibro };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<LibroDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] LibroDTO request)
        {
            ResponseDTO<LibroDTO> _ResponseDTO = new ResponseDTO<LibroDTO>();
            try
            {
                Libro _libro = _mapper.Map<Libro>(request);

                Libro _libroCreado = await _libroRepositorio.Crear(_libro);

                if (_libroCreado.IdLibro != 0)
                    _ResponseDTO = new ResponseDTO<LibroDTO>() { status = true, msg = "ok", value = _mapper.Map<LibroDTO>(_libroCreado) };
                else
                    _ResponseDTO = new ResponseDTO<LibroDTO>() { status = false, msg = "No se pudo crear el libro" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<LibroDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] LibroDTO request)
        {
            ResponseDTO<LibroDTO> _ResponseDTO = new ResponseDTO<LibroDTO>();
            try
            {
                Libro _libro = _mapper.Map<Libro>(request);
                Libro _libroParaEditar = await _libroRepositorio.Obtener(u => u.IdLibro == _libro.IdLibro);

                if (_libroParaEditar != null)
                {

                    _libroParaEditar.Titulo = _libro.Titulo;
                    _libroParaEditar.Autor = _libro.Autor;
                    _libroParaEditar.IdCategoria = _libro.IdCategoria;
                    _libroParaEditar.Editorial = _libro.Editorial;
                    _libroParaEditar.Ubicacion = _libro.Ubicacion;
                    _libroParaEditar.Ejemplares = _libro.Ejemplares;

                    if (_libro.Portada != null)
                        _libroParaEditar.Portada = _libro.Portada;

                    bool respuesta = await _libroRepositorio.Editar(_libroParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<LibroDTO>() { status = true, msg = "ok", value = _mapper.Map<LibroDTO>(_libroParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<LibroDTO>() { status = false, msg = "No se pudo editar el libro" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<LibroDTO>() { status = false, msg = "No se encontró el libro" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<LibroDTO>() { status = false, msg = ex.Message };
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
                Libro _libroEliminar = await _libroRepositorio.Obtener(u => u.IdLibro == id);

                if (_libroEliminar != null)
                {

                    bool respuesta = await _libroRepositorio.Eliminar(_libroEliminar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        _ResponseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el libro", value = "" };
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
