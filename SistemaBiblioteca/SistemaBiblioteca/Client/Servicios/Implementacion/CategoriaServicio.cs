using SistemaBiblioteca.Client.Servicios.Contrato;
using SistemaBiblioteca.Shared;
using System.Net.Http.Json;

namespace SistemaBiblioteca.Client.Servicios.Implementacion
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly HttpClient _http;
        public CategoriaServicio(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<List<CategoriaDTO>>> Lista()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<List<CategoriaDTO>>>("api/categoria/Lista");
            return result!;
        }
    }
}
