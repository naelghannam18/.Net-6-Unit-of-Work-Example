using UnitOfworkAndRepositoryExampleProject.IConfiguration;
using UnitOfworkAndRepositoryExampleProject.IRepositories;
using UnitOfworkAndRepositoryExampleProject.Repositories;

namespace UnitOfworkAndRepositoryExampleProject.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext Context;
    private readonly ILogger Logger;

    public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
    {
        Context = context;
        Logger = loggerFactory.CreateLogger("Logs");

        Users = new UserRepository(Context, Logger);
    }

    public IUserRepository Users { get; private set; }

    public async Task CompleteAsync()
    {
        await Context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Context.Dispose();
    }

}
