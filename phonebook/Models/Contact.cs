using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;

public class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["main"].ConnectionString);
    }

}

public class Contact
{
    public int Id;
    public string Name;
    public string Email;
    public string PhoneNumber;
}