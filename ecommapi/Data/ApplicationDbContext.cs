using ecommapi.Entity;
using Microsoft.EntityFrameworkCore;

namespace ecommapi.Data;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Brand> Brand { get; set; }

}
