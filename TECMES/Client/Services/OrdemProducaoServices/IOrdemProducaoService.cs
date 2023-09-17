namespace TECMES.Client.Services.OrdemProducaoServices
{
    public interface IOrdemProducaoService
    {
        Task<List<OrdemProducao>?> ObterTodos();
        Task<OrdemProducao?> ObterPorId(Guid id);
        Task<OrdemProducao?> Adicionar(OrdemProducao ordemProducao);
        Task<OrdemProducao?> Atualizar(OrdemProducao ordemProducao);
        Task Remover(Guid id);
    }
}
