using Microsoft.EntityFrameworkCore;
using UnitOfworkAndRepositoryExampleProject.Data;
using UnitOfworkAndRepositoryExampleProject.IRepositories;
using UnitOfworkAndRepositoryExampleProject.Models;

namespace UnitOfworkAndRepositoryExampleProject.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
    {

    }

    public async Task<string> GetFirstNameAndLastName(Guid id)
    {
        var user = await GetById(id);
        if (user is not null) return string.Concat(user.FirstName, user.LastName);
        return string.Empty;
    }

    public override async Task<IEnumerable<User>> All()
    {
        try
        {
            return await DbSet.ToListAsync();
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "User Repo: All() Method Error", typeof(UserRepository));
            return Enumerable.Empty<User>();
        }
    }

    public override async Task<bool> Upsert(User entity)
    {
        try
        {
            var existing = await DbSet.Where(u => u.Id == entity.Id).FirstOrDefaultAsync();
            if (existing is not null)
            {
                existing.FirstName = entity.FirstName;
                existing.LastName = entity.LastName;
                existing.Email = entity.Email;
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "User Repository: Upsert() Method Error", typeof(UserRepository));
            return false;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var existing = await DbSet.Where(u => u.Id == id).FirstOrDefaultAsync();
            if (existing is not null)
            {
                DbSet.Remove(existing);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "User Repository: Delete() Method Error", typeof(UserRepository));
            return false;
        }
    }
}
