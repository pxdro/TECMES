namespace TECMES.Client.Services.UsuarioServices
{
    public interface IUsuarioService
    {
        Task<List<Usuario>?> ObterTodos();
        Task<Usuario?> ObterPorId(Guid id);
        Task<Usuario?> ObterPorEmail(string email);
        Task<Usuario?> Adicionar(Usuario usuario);
        Task<Usuario?> Atualizar(Usuario usuario);
        Task Remover(Guid id);
        Task<Usuario?> Logar(Usuario usuario);
    }
}
