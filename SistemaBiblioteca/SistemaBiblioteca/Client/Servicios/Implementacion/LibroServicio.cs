using SistemaBiblioteca.Client.Servicios.Contrato;
using SistemaBiblioteca.Shared;
using System.Net.Http.Json;

namespace SistemaBiblioteca.Client.Servicios.Implementacion
{
    public class LibroServicio : ILibroServicio
    {
        
        private readonly HttpClient _http;
        public LibroServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<List<LibroDTO>>> Buscar(string valor)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<LibroDTO>>>($"api/libro/Buscar?valor={valor}");
            return result!;
        }

        public async Task<ResponseDTO<LibroDTO>> Crear(LibroDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/libro/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<LibroDTO>>();
            return response!;
        }

        public async Task<bool> Editar(LibroDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/libro/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<LibroDTO>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/libro/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<LibroDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<LibroDTO>>>("api/libro/Lista");
            return result!;
        }

        public async Task<ResponseDTO<LibroDTO>> Obtener(int idLibro)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<LibroDTO>>($"api/libro/Obtener/{idLibro}");
            return result!;
        }
    }
}
