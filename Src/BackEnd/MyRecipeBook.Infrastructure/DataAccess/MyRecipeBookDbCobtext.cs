using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Infrastructure.DataAccess;

public class MyRecipeBookDbCobtext : DbContext
{
    public MyRecipeBookDbCobtext(DbContextOptions<MyRecipeBookDbCobtext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyRecipeBookDbCobtext).Assembly);
    }
}
