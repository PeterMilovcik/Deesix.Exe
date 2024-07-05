using System.Linq.Expressions;

namespace Deesix.Application.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    TEntity Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entity);
    TEntity? GetId(int id);
    Task<TEntity?> GetIdAsync(int id);
    TEntity? Get(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync();
    int Count();
    Task<int> CountAsync();
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void Dispose();
}
