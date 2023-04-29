using SistemaBiblioteca.Client.Pages;
using SistemaBiblioteca.Client.Servicios.Contrato;
using SistemaBiblioteca.Shared;
using System.Net.Http.Json;

namespace SistemaBiblioteca.Client.Servicios.Implementacion
{
    public class DashBoardServicio : IDashBoardServicio
    {
        private readonly HttpClient _http;
        public DashBoardServicio(HttpClient http)
        {
            _http = http;
        }
        public async Task<ResponseDTO<DashBoardDTO>> Resumen()
        {
            var result = await _http.GetFromJsonAsync<ResponseDTO<DashBoardDTO>>("api/dashboard/Resumen");
            return result!;
        }
    }
}
