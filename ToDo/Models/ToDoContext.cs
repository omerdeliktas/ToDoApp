using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Models;

public class ToDoContext : DbContext
{
    public ToDoContext(DbContextOptions<ToDoContext> options) : base(options) { }


    public DbSet<ToDo> ToDos { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Status> Statuss { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = "work", Name = "Calisma" },
            new Category { CategoryId = "home", Name = "Ev İsleri" },
            new Category { CategoryId = "ex", Name = "Egzersiz" },
            new Category { CategoryId = "shop", Name = "Alisveris" },
            new Category { CategoryId = "call", Name = "iletisim - mail" }

            );
        modelBuilder.Entity<Status>().HasData(
            new Status { StatusId = "open", Name = "Devam Eden" },
            new Status { StatusId = "closed", Name = "Yapıldı" }
            );
    }

    /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
     {
         optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB; Database=ToDos; Trusted_Connection=true");
     }*/
}
