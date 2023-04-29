using SistemaBiblioteca.Client.Servicios.Contrato;
using SistemaBiblioteca.Shared;
using System.Net.Http.Json;

namespace SistemaBiblioteca.Client.Servicios.Implementacion
{
    public class LectorServicio : ILectorServicio
    {
        private readonly HttpClient _http;
        public LectorServicio(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<LectorDTO>> Crear(LectorDTO entidad)
        {

            var result = await _http.PostAsJsonAsync("api/lector/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<LectorDTO>>();
            return response!;
        }

        public async Task<bool> Editar(LectorDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/lector/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<LectorDTO>>();

            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/lector/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<LectorDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<LectorDTO>>>("api/lector/Lista");
            return result!;
        }

        public async Task<ResponseDTO<LectorDTO>> Obtener(int idLector)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<LectorDTO>>($"api/lector/Obtener/{idLector}");
            return result!;
        }
    }
}
