namespace WebApiTest.Data;
using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

public class DataContext : DbContext
{
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<TaskItem>()
            .HasOne(t => t.Priority)    //a task has one priority
            .WithMany(p => p.TaskItems); //priority has many task items
    }
    
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
}
