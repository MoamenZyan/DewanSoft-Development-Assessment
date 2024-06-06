using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

public class ApplicationDatabaseContext : DbContext
{
    public ApplicationDatabaseContext(DbContextOptions options) : base(options) {}
    public DbSet<Item> Item {get; set;}
    public DbSet<Reciept> Reciept {get; set;}
    public DbSet<RecieptItems> Reciept_Items {get; set;}

    // To make composite key
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecieptItems>().HasKey(k => new {k.Item_Id, k.Reciept_Id}).HasName("PK_RecieptItems");

        // Configure the relationships
        modelBuilder.Entity<RecieptItems>()
            .HasOne(ri => ri.Item)
            .WithMany()
            .HasForeignKey(ri => ri.Item_Id);

        modelBuilder.Entity<RecieptItems>()
            .HasOne(ri => ri.Reciept)
            .WithMany()
            .HasForeignKey(ri => ri.Reciept_Id);
            base.OnModelCreating(modelBuilder);
    }
}