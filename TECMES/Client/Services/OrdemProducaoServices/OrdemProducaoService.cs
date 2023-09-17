using System.Net.Http.Json;

namespace TECMES.Client.Services.OrdemProducaoServices
{
    public class OrdemProducaoService : IOrdemProducaoService
    {
        private readonly HttpClient _httpClient;
        public OrdemProducaoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrdemProducao>?> ObterTodos()
        {
            var result = await _httpClient.GetFromJsonAsync<List<OrdemProducao>>("api/ordemproducao");

            if (result != null)
                return result;
            throw new Exception("Nenhuma ordem de produção encontrado");
        }

        public async Task<OrdemProducao?> ObterPorId(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<OrdemProducao>($"api/ordemproducao/{id}");

            if (result != null)
                return result;

            throw new Exception("Ordem de produção não encontrada.");
        }

        public async Task<OrdemProducao?> Adicionar(OrdemProducao ordemProducao)
        {
            var result = await _httpClient.PostAsJsonAsync("api/ordemproducao", ordemProducao);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<OrdemProducao>();
                return response!;
            }

            throw new Exception("Ordem de produção já cadastrada no sistema.");
        }

        public async Task<OrdemProducao?> Atualizar(OrdemProducao ordemproducao)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/ordemproducao/{ordemproducao.Id}", ordemproducao);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<OrdemProducao>();
                return response!;
            }

            throw new Exception("Ordem de produção já cadastrada no sistema.");
        }

        public async Task Remover(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/ordemproducao/{id}");

            if (result.IsSuccessStatusCode)
                return;

            throw new Exception("Ordem de produção utilizada em uma Produção.");
        }
    }
}
