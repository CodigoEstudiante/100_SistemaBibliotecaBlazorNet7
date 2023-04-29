using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaBiblioteca.Server.Models;
using SistemaBiblioteca.Server.Repositorio.Contrato;
using SistemaBiblioteca.Server.Repositorio.Implementacion;
using SistemaBiblioteca.Shared;

namespace SistemaBiblioteca.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILectorRepositorio _lectorRepositorio;
        public LectorController(ILectorRepositorio lectorRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _lectorRepositorio = lectorRepositorio;
        }

        [HttpGet]
        [Route("Obtener/{idLector}")]
        public async Task<IActionResult> Obtener(int idLector)
        {
            ResponseDTO<LectorDTO> _ResponseDTO = new ResponseDTO<LectorDTO>();

            try
            {
                LectorDTO lectorDTO;
                Lector encontrado = await _lectorRepositorio.Obtener(l => l.IdLector == idLector);

                if(encontrado != null)
                {
                    lectorDTO = _mapper.Map<LectorDTO>(encontrado);
                    _ResponseDTO = new ResponseDTO<LectorDTO>() { status = true, msg = "ok", value = lectorDTO };
                }
                else 
                    _ResponseDTO = new ResponseDTO<LectorDTO>() { status = false, msg = "", value = null };


                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<LectorDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            ResponseDTO<List<LectorDTO>> _ResponseDTO = new ResponseDTO<List<LectorDTO>>();

            try
            {
                List<LectorDTO> listaLectores = new List<LectorDTO>();
                IQueryable<Lector> query = await _lectorRepositorio.Consultar();

                listaLectores = _mapper.Map<List<LectorDTO>>(query.ToList());

                _ResponseDTO = new ResponseDTO<List<LectorDTO>>() { status = true, msg = "ok", value = listaLectores };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<List<LectorDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] LectorDTO request)
        {
            ResponseDTO<LectorDTO> _ResponseDTO = new ResponseDTO<LectorDTO>();
            try
            {
                Lector _lector = _mapper.Map<Lector>(request);

                Lector _lectorCreado = await _lectorRepositorio.Crear(_lector);

                if (_lectorCreado.IdLector != 0)
                    _ResponseDTO = new ResponseDTO<LectorDTO>() { status = true, msg = "ok", value = _mapper.Map<LectorDTO>(_lectorCreado) };
                else
                    _ResponseDTO = new ResponseDTO<LectorDTO>() { status = false, msg = "No se pudo crear el lector" };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<LectorDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }


        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] LectorDTO request)
        {
            ResponseDTO<LectorDTO> _ResponseDTO = new ResponseDTO<LectorDTO>();
            try
            {
                Lector _lector = _mapper.Map<Lector>(request);
                Lector _lectorParaEditar = await _lectorRepositorio.Obtener(u => u.IdLector == _lector.IdLector);

                if (_lectorParaEditar != null)
                {

                    _lectorParaEditar.Nombre = _lector.Nombre;
                    _lectorParaEditar.Apellido = _lector.Apellido;
                    _lectorParaEditar.Correo = _lector.Correo;

                    bool respuesta = await _lectorRepositorio.Editar(_lectorParaEditar);

                    if (respuesta)
                        _ResponseDTO = new ResponseDTO<LectorDTO>() { status = true, msg = "ok", value = _mapper.Map<LectorDTO>(_lectorParaEditar) };
                    else
                        _ResponseDTO = new ResponseDTO<LectorDTO>() { status = false, msg = "No se pudo editar el lector" };
                }
                else
                {
                    _ResponseDTO = new ResponseDTO<LectorDTO>() { status = false, msg = "No se encontró el lector" };
                }

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<LectorDTO>() { status = false, msg = ex.Message };
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
                Lector _lectorEliminar = await _lectorRepositorio.Obtener(u => u.IdLector == id);

                if (_lectorEliminar != null)
                {

                    bool respuesta = await _lectorRepositorio.Eliminar(_lectorEliminar);

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
