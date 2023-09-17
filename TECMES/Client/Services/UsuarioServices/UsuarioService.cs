using System.Net.Http.Json;

namespace TECMES.Client.Services.UsuarioServices
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;
        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Usuario>?> ObterTodos()
        {
            var resultado = await _httpClient.GetFromJsonAsync<List<Usuario>>("api/usuario");

            if (resultado != null)
                return resultado;
            throw new Exception("Nenhum usuário encontrado");
        }

        public async Task<Usuario?> ObterPorId(Guid id)
        {
            var resultado = await _httpClient.GetFromJsonAsync<Usuario>($"api/usuario/{id}");

            if (resultado != null)
                return resultado;
            throw new Exception("Nenhum usuário encontrado");
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        {
            var resultado = await _httpClient.GetFromJsonAsync<Usuario>($"api/usuario/{email}");

            if (resultado != null)
                return resultado;
            throw new Exception("Nenhum usuário encontrado");
        }

        public async Task<Usuario?> Adicionar(Usuario usuario)
        {
            var resultado = await _httpClient.PostAsJsonAsync("api/usuario", usuario);

            if (resultado.IsSuccessStatusCode)
            {
                var resposta = await resultado.Content.ReadFromJsonAsync<Usuario>();
                return resposta!;
            }
            else
                throw new Exception("Email já cadastrado no sistema.");
        }

        public async Task<Usuario?> Atualizar(Usuario usuario)
        {
            var resultado = await _httpClient.PostAsJsonAsync($"api/usuario/{usuario.Id}", usuario);

            if (resultado.IsSuccessStatusCode)
            {
                var resposta = await resultado.Content.ReadFromJsonAsync<Usuario>();
                return resposta!;
            }
            else
                throw new Exception("Email já cadastrado no sistema.");
        }

        public async Task Remover(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/usuario/{id}");

            if (result.IsSuccessStatusCode)
                return;

            throw new Exception("Erro ao excluir usuário.");
        }

        public async Task<Usuario?> Logar(Usuario usuario)
        {
            var resultado = await _httpClient.PostAsJsonAsync($"api/usuario/login", usuario);
            var resposta = await resultado.Content.ReadFromJsonAsync<Usuario>();

            if (resultado.IsSuccessStatusCode)
                return resposta!;
            else if (resposta != null)
                throw new Exception(resposta?.Error);

            throw new Exception("Falha no Login.");
        }
    }
}
