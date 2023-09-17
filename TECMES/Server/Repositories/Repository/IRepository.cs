using System.Linq.Expressions;

namespace TECMES.Server.Repositories.Repository
{
    public interface IRepository<TEntity> : IDisposable
    {
        Task<List<TEntity>?> ObterTodos(params Expression<Func<TEntity, object?>>[] includes);
        Task<TEntity?> ObterPorId(Guid id);
        Task<IEnumerable<TEntity>?> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<int> SalvarAlteracoes();
        void EncerrarTracker();
    }
}
