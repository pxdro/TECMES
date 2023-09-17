using System.Net.Http.Json;

namespace TECMES.Client.Services.PedidoServices
{
    public class PedidoService : IPedidoService
    {
        private readonly HttpClient _httpClient;
        public PedidoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Pedido>?> ObterTodos()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Pedido>>("api/pedido");

            if (result != null)
                return result;

            throw new Exception("Nenhum pedido encontrado");
        }
        public async Task<Pedido?> ObterPorId(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<Pedido>($"api/pedido/{id}");

            if (result != null)
                return result;

            throw new Exception("Pedido não encontrado.");
        }

        public async Task<Pedido?> Adicionar(Pedido pedido)
        {
            var result = await _httpClient.PostAsJsonAsync("api/pedido", pedido);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<Pedido>();
                return response!;
            }

            throw new Exception("Pedido já cadastrado no sistema.");
        }

        public async Task<Pedido?> Atualizar(Pedido pedido)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/pedido/{pedido.Id}", pedido);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<Pedido>();
                return response!;
            }

            throw new Exception("Pedido já cadastrado no sistema.");
        }

        public async Task Remover(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/pedido/{id}");

            if (result.IsSuccessStatusCode)
                return;

            throw new Exception("Erro ao excluir Pedido.");
        }
    }
}
