using System.Net.Http.Json;

namespace TECMES.Client.Services.ProducaoServices
{
    public class ProducaoService : IProducaoService
    {
        private readonly HttpClient _httpClient;
        public ProducaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Producao>?> ObterTodos()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Producao>>("api/producao");

            if (result != null)
                return result;

            throw new Exception("Nenhum produção encontrada");
        }
        public async Task<Producao?> ObterPorId(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<Producao>($"api/producao/{id}");

            if (result != null)
                return result;

            throw new Exception("Produção não encontrada.");
        }

        public async Task<Producao?> Adicionar(Producao producao)
        {
            var result = await _httpClient.PostAsJsonAsync("api/producao", producao);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<Producao>();
                return response!;
            }

            throw new Exception("Produção já cadastrada no sistema.");
        }

        public async Task<Producao?> Atualizar(Producao producao)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/producao/{producao.Id}", producao);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<Producao>();
                return response!;
            }

            throw new Exception("Produção já cadastrada no sistema.");
        }

        public async Task Remover(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/producao/{id}");

            if (result.IsSuccessStatusCode)
                return;

            throw new Exception("Produção utilizada em um Pedido.");
        }
    }
}
