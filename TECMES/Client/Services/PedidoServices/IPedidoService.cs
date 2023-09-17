namespace TECMES.Client.Services.PedidoServices
{
    public interface IPedidoService
    {
        Task<List<Pedido>?> ObterTodos();
        Task<Pedido?> ObterPorId(Guid id);
        Task<Pedido?> Adicionar(Pedido pedido);
        Task<Pedido?> Atualizar(Pedido pedido);
        Task Remover(Guid id);
    }
}
