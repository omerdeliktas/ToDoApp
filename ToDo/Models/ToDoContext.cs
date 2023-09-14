using Microsoft.EntityFrameworkCore;

namespace ToDo.Models;

public class ToDoContext : DbContext 
{
    public ToDoContext(DbContextOptions<ToDoContext> options): base(options) { }


    public DbSet<ToDo> ToDos { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;    
    public DbSet<Status> Statuss { get; set; } = null!;   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = "work", Name = "Calisma" },
            new Category { CategoryId = "home", Name = "Ev" },
            new Category { CategoryId = "ex", Name = "Egzersiz" },
            new Category { CategoryId = "shop", Name = "Alisveris" },
            new Category { CategoryId = "call", Name = "iletisim" }

            );
        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = "open", Name = "Devam Eden Görev" },
            new Status { StatusId = "closed", Name = "Yapıldı" }
            );
    }
}
