using UnitOfworkAndRepositoryExampleProject.Models;

namespace UnitOfworkAndRepositoryExampleProject.IRepositories;

public interface IUserRepository : IGenericRepository<User>
{
    // This Method will be unique to the user repository
    Task<string> GetFirstNameAndLastName(Guid id);
}
