using UnitOfworkAndRepositoryExampleProject.IRepositories;

namespace UnitOfworkAndRepositoryExampleProject.IConfiguration;

public interface IUnitOfWork
{
    IUserRepository Users { get; }

    Task CompleteAsync();
}
