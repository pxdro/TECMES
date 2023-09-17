namespace TECMES.Client.Services.ProducaoServices
{
    public interface IProducaoService
    {
        Task<List<Producao>?> ObterTodos();
        Task<Producao?> ObterPorId(Guid id);
        Task<Producao?> Adicionar(Producao producao);
        Task<Producao?> Atualizar(Producao producao);
        Task Remover(Guid id);
    }
}
