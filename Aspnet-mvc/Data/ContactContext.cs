namespace Aspnet_mvc.Data;

using Aspnet_mvc.Models;
using Microsoft.EntityFrameworkCore;
public class ContactContext : DbContext
{
    private readonly string _ContectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Mvc;Integrated Security=True;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";


    public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }

    public DbSet<ContactModel> Contacts { get; set; }
    public DbSet<UserModel> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_ContectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactModel>()
            .HasKey(contact => contact.Id);

        modelBuilder.Entity<UserModel>()
            .HasKey(user => user.Id);
    }
}
