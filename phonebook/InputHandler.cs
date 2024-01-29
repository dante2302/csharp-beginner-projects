
public class InputHandler
{
    private Controller controller = new();

    public void MainMenu()
    {
        Console.Clear();
        Console.WriteLine("0 - Exit");
        Console.WriteLine("1 - Create");
        Console.WriteLine("2 - Read One");
        Console.WriteLine("3 - Read All");
        Console.WriteLine("4 - Update");
        Console.WriteLine("5 - Delete");
        string input = GetStrInput();
        switch (input)
        {
            case "0":
                Environment.Exit(0);
                break;

            case "1":
                Creation();
                break;

            case "2":
                ReadingOne();
                break;

            case "3":
                ReadingAll();
                break;

            case "4":
                Updating();
                break;

            case "5":
                Deletion();
                break;
        }
    }

    private void Creation()
    {
        Console.WriteLine("Creating a contact");
        Contact newContact = CreationPrompt();
        controller.Create(newContact);
        Console.WriteLine("Contact was created!");
        GoBack();
    }

    private Contact CreationPrompt()
    {
        Console.Write("Name: ");
        string name = GetStrInput();
        Console.WriteLine();
        Console.Write("Email: ");
        string email = GetStrInput();
        Console.WriteLine();
        Console.WriteLine("Phone Number: ");
        string phoneNumber = GetStrInput();
        var newContact = new Contact { Name = name, Email = email, PhoneNumber = phoneNumber };
        return newContact;
    }

    private void ReadingOne()
    {
        Console.WriteLine("Id for Reading: ");
        int id = GetIntInput();
        Contact contact = controller.ReadOne(id);
        Printer.PrintOneContact(contact);
        GoBack();
    }

    private void ReadingAll()
    {
        Console.WriteLine("Reading...");
        List<Contact> allContacts = controller.ReadAll();
        Printer.PrintContacts(allContacts);
        GoBack();
    }

    private void Updating()
    {
        Console.WriteLine("Id of the contact for updating: ");
        int id = GetIntInput();
        Contact updatedContact = CreationPrompt();
        updatedContact.Id = id;
        controller.Update(id, updatedContact);
        GoBack();
    }
    private void Deletion()
    {
        Console.WriteLine("Id of the contact for deletion: ");
        int id = GetIntInput();
        controller.Delete(id);
        GoBack();
    }

    public int GetIntInput()
    {
        return Convert.ToInt32(Console.ReadLine());
    }

    public string GetStrInput()
    {
        return Console.ReadLine();
    }

    public void GoBack()
    {
        Console.WriteLine("Type anything to go back...");
        Console.ReadLine();
        MainMenu();
    }

}
