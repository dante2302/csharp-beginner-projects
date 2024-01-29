using Microsoft.EntityFrameworkCore;
using System.Configuration;


public class ContactContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    
      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["main"].ConnectionString);
    }

    public Contact GetById(int id)
    {
        Contact contact = Contacts.Find(id);
        return contact;
    }

}

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}