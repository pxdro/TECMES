using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TECMES.Server.Context;
using TECMES.Shared.Models;

namespace TECMES.Server.Repositories.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly TECMESContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(TECMESContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>?> ObterTodos(params Expression<Func<TEntity, object?>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            if (includes != null)
                foreach (var include in includes)
                    query = query.Include(include);

            return await query.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity?> ObterPorId(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>?> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            _dbSet.Add(entity);
            await SalvarAlteracoes();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            _dbSet.Update(entity);
            await SalvarAlteracoes();
        }

        public virtual async Task Remover(Guid id)
        {
            _dbSet.Remove(new TEntity { Id = id });
            await SalvarAlteracoes();
        }

        public async Task<int> SalvarAlteracoes()
        {
            return await _context.SaveChangesAsync();
        }

        public void EncerrarTracker()
        {
            _context.ChangeTracker.Clear();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
