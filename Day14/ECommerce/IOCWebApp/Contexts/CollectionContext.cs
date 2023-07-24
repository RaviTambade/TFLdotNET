using Microsoft.EntityFrameworkCore;
using IOCWebApp.Models;

namespace IOCWebApp.Contexts
{
    public class CollectionContext:DbContext{
    public DbSet<Product> Products {get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string conString="server=127.0.0.1;uid=root;" +  "pwd=password;database=transflower";
        optionsBuilder.UseMySQL(conString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>(entity => 
        {
          entity.HasKey(e => e.Id);
          entity.Property(e => e.Title).IsRequired();
          entity.Property(e => e.UnitPrice).IsRequired();
          entity.Property(e => e.Quantity).IsRequired();
        });
    }
    }
}