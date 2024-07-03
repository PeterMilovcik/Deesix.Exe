using System.Linq.Expressions;

namespace Deesix.Application;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(object id);

    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "");

    Task InsertAsync(TEntity entity);

    Task DeleteAsync(object id);

    Task Delete(TEntity entityToDelete);

    Task Update(TEntity entityToUpdate);
}
