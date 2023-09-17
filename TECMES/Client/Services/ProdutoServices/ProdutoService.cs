using System.Net.Http.Json;

namespace TECMES.Client.Services.ProdutoServices
{
    public class ProdutoService : IProdutoService
    {
        private readonly HttpClient _httpClient;
        public ProdutoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Produto>?> ObterTodos()
        {
            var result = await _httpClient.GetFromJsonAsync<List<Produto>>("api/produto");

            if (result != null)
                return result;

            throw new Exception("Nenhum produto encontrado");
        }

        public async Task<Produto?> ObterPorId(Guid id)
        {
            var result = await _httpClient.GetFromJsonAsync<Produto>($"api/produto/{id}");

            if (result != null)
                return result;

            throw new Exception("Produto não encontrado.");
        }

        public async Task<Produto?> Adicionar(Produto produto)
        {
            var result = await _httpClient.PostAsJsonAsync("api/produto", produto);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<Produto>();
                return response!;
            }

            throw new Exception("Produto já cadastrado no sistema.");
        }

        public async Task<Produto?> Atualizar(Produto produto)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/produto/{produto.Id}", produto);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadFromJsonAsync<Produto>();
                return response!;
            }

            throw new Exception("Produto já cadastrado no sistema.");
        }

        public async Task Remover(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/produto/{id}");

            if (result.IsSuccessStatusCode)
                return;

            throw new Exception("Produto utilizado em uma Ordem de Produção.");
        }
    }
}
