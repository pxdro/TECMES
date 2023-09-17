namespace TECMES.Client.Services.ProdutoServices
{
    public interface IProdutoService
    {
        Task<List<Produto>?> ObterTodos();
        Task<Produto?> ObterPorId(Guid id);
        Task<Produto?> Adicionar(Produto produto);
        Task<Produto?> Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}
