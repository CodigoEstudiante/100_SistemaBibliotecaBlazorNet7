using SistemaBiblioteca.Client.Servicios.Contrato;
using SistemaBiblioteca.Shared;
using System.Net.Http.Json;

namespace SistemaBiblioteca.Client.Servicios.Implementacion
{
    public class PrestamoServicio : IPrestamoServicio
    {
        private readonly HttpClient _http;
        public PrestamoServicio(HttpClient http)
        {
            _http = http;
        }

        public async Task<ResponseDTO<List<PrestamoDTO>>> Buscar(string estadoPrestamo, string codigoLector)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PrestamoDTO>>>($"api/prestamo/Buscar?estadoPrestamo={estadoPrestamo}&codigoLector={codigoLector}");
            return result!;
        }

        public async Task<ResponseDTO<PrestamoDTO>> Crear(PrestamoDTO entidad)
        {
            var result = await _http.PostAsJsonAsync("api/prestamo/Guardar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<PrestamoDTO>>();
            return response!;
        }

        public async Task<bool> Editar(PrestamoDTO entidad)
        {
            var result = await _http.PutAsJsonAsync("api/prestamo/Editar", entidad);
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<PrestamoDTO>>();
            return response!.status;
        }

        public async Task<bool> Eliminar(int id)
        {
            var result = await _http.DeleteAsync($"api/prestamo/Eliminar/{id}");
            var response = await result.Content.ReadFromJsonAsync<ResponseDTO<string>>();
            return response!.status;
        }

        public async Task<ResponseDTO<List<PrestamoDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<PrestamoDTO>>>("api/prestamo/Lista");
            return result!;
        }

        public async Task<ResponseDTO<PrestamoDTO>> Obtener(int idPrestamo)
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<PrestamoDTO>>($"api/prestamo/Obtener/{idPrestamo}");
            return result!;
        }
    }
}
