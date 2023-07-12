using Microsoft.EntityFrameworkCore;
using UnitOfworkAndRepositoryExampleProject.Data;
using UnitOfworkAndRepositoryExampleProject.IRepositories;

namespace UnitOfworkAndRepositoryExampleProject.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected ApplicationDbContext Context;
    protected DbSet<T> DbSet;
    protected readonly ILogger Logger;
    public GenericRepository(ApplicationDbContext context, ILogger logger)
    {
        Context = context;
        DbSet = Context.Set<T>();
        Logger = logger;
    }

    public virtual async Task<bool> Add(T entity)
    {
        await DbSet.AddAsync(entity);
        return true;
    }

    public virtual async Task<IEnumerable<T>> All()
    {
        return await DbSet.AsNoTracking().ToListAsync();
    }

    public virtual async Task<bool> Delete(Guid id)
    {
        var user = await GetById(id);
        if(user is not null)
        {
            DbSet.Remove(user);
            return true;
        }
        return false;
    }

    public virtual async Task<T> GetById(Guid id)
    {
        return await DbSet.FindAsync(id);
    }

    public virtual Task<bool> Upsert(T entity)
    {
        throw new NotImplementedException();
    }
}
