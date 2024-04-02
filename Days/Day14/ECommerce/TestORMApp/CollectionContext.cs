//use dotnet cli command
//dotnet add package Microsoft.EntityFramework

using Microsoft.EntityFrameworkCore;
using BOL;

namespace DAL;
public class CollectionContext:DbContext{
    public DbSet<Department> Departments {get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string conString=@"server=localhost;port=3306;user=root; password=password;database=transflower";       
        optionsBuilder.UseMySQL(conString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Set Mapping of Table with POCO
        //Relational        instance: Table
        //Object Oriented   instance: POCO Class
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Department>(entity => 
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Location).IsRequired();
        });
        modelBuilder.Entity<Department>().ToTable("departments");
    }
}

