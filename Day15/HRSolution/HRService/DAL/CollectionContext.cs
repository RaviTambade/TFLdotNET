using Microsoft.EntityFrameworkCore;
using HRService.BOL;

namespace HRService.DAL;
public class CollectionContext:DbContext{
    public DbSet<Employee> Employees{get;set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        string conString=@"server=localhost;port=3306;user=root;password=password;database=transflower";
        optionsBuilder.UseMySQL(conString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>(entity=>{
            entity.HasKey(e => e.Id);
            entity.Property(e=>e.FirstName).IsRequired();
            entity.Property(e=>e.LastName).IsRequired();
            entity.Property(e=>e.Email).IsRequired();
            entity.Property(e=>e.Password).IsRequired();
            entity.Property(e=>e.Address).IsRequired();
            entity.Property(e=>e.DeptId).IsRequired();
            entity.Property(e=>e.ManagerId).IsRequired();
        });
        modelBuilder.Entity<Employee>().ToTable("employees");
    }
}