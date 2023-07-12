using Microsoft.EntityFrameworkCore;
using UnitOfworkAndRepositoryExampleProject.Models;

namespace UnitOfworkAndRepositoryExampleProject.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {

    }

    // This references the user entity into the Database.
    public virtual DbSet<User> Users { get; set; }
}
