using System.Linq.Expressions;
using Deesix.Application;
using Microsoft.EntityFrameworkCore;

namespace Deesix.Infrastructure.DataAccess;

public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext context;
    private DbSet<TEntity> dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        this.context = context ?? throw new ArgumentNullException(nameof(context));
        dbSet = context.Set<TEntity>();
    }

    public Task<TEntity> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null, 
        Func<IQueryable<TEntity>, 
        IOrderedQueryable<TEntity>>? orderBy = null, 
        string includeProperties = "")
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(TEntity entityToUpdate)
    {
        throw new NotImplementedException();
    }

    public Task Delete(TEntity entityToDelete)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
