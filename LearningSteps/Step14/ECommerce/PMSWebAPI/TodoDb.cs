using Microsoft.EntityFrameworkCore;
using PMS;
public class TodoDb:DbContext
{
    public TodoDb(DbContextOptions<TodoDb> options): base(options) { }
    public DbSet<Todo> Todos => Set<Todo>();
}