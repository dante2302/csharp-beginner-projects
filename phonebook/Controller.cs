
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;

public class Controller
{
    private ContactContext context = new();
    public void Create(Contact newContact)
    {
        context.Add(newContact);
        context.SaveChanges();
    }

    public Contact ReadOne(int id)
    {
        return context.GetById(id);
    }

    public List<Contact> ReadAll()
    {
        return context.Contacts
                        .OrderBy(c => c.Id)
                        .ToList();
    }

    public void Update(int id,Contact updatedContact)
    {
        Contact contactForUpdate = ReadOne(id);
        contactForUpdate.Name = updatedContact.Name;
        contactForUpdate.Email = updatedContact.Email;
        contactForUpdate.PhoneNumber = updatedContact.PhoneNumber;
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        Contact contactForDeletion = ReadOne(id);
        context.Contacts.Remove(contactForDeletion);
        context.SaveChanges();
    }
}
