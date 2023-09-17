using TECMES.Shared.Models;

namespace TECMES.Server.Services.UsuarioServices
{
    public interface IUsuarioService : IDisposable
    {
        Task<List<Usuario>?> ObterTodos();
        Task<Usuario?> ObterPorId(Guid id);
        Task<Usuario?> ObterPorEmail(string email);
        Task<Usuario?> Adicionar(Usuario usuario);
        Task<Usuario?> Atualizar(Usuario usuario);
        Task Remover(Guid id);
        Task<Usuario?> Logar(Usuario usuario);
        string CriarJWTToken(Usuario usuario);
    }
}
