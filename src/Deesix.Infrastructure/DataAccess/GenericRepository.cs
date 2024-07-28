using System.Linq.Expressions;
using Deesix.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Deesix.Infrastructure.DataAccess;

public sealed class GenericRepository<TEntity>(ApplicationDbContext applicationDbContext) : IRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext context = applicationDbContext;

    public TEntity Add(TEntity model)
    {
        var entry = context.Set<TEntity>().Add(model);
        return entry.Entity;
    }

    public void AddRange(IEnumerable<TEntity> model)
    {
        context.Set<TEntity>().AddRange(model);
    }

    public TEntity? GetById(int id) => 
        context.Set<TEntity>().Find(id);

    public async Task<TEntity?> GetIdAsync(int id) => 
        await context.Set<TEntity>().FindAsync(id);

    public TEntity? Get(Expression<Func<TEntity, bool>> predicate) => 
        context.Set<TEntity>().FirstOrDefault(predicate);

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate) => 
        await context.Set<TEntity>().FirstOrDefaultAsync(predicate);

    public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate) => 
        context.Set<TEntity>().Where<TEntity>(predicate).ToList();

    public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate) => 
        await Task.Run(() => context.Set<TEntity>().Where<TEntity>(predicate));

    public IEnumerable<TEntity> GetAll() => 
        context.Set<TEntity>().ToList();

    public async Task<IEnumerable<TEntity>> GetAllAsync() => 
        await Task.Run(() => context.Set<TEntity>());

    public int Count() => 
        context.Set<TEntity>().Count();

    public async Task<int> CountAsync() => 
        await context.Set<TEntity>().CountAsync();

    public void Update(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public void Remove(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public void Dispose() => context.Dispose();

    public void SaveChanges() => context.SaveChanges();
}
