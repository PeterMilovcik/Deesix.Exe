using System.Linq.Expressions;

namespace Deesix.Application;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);

    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    Task<TEntity> InsertAsync(TEntity entity);

    Task DeleteAsync(int id);

    Task Delete(TEntity entityToDelete);

    Task Update(TEntity entityToUpdate);
}
