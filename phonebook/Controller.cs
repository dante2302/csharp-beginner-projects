using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Diagnostics.Eventing.Reader;

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
}

public class InputHandler
{
    private Controller controller = new();

    public void MainMenu()
    {
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
                Updating()
                break;

            case "5":
                Deletion();
                break;
        }
    }

    public void Creation()
    {
        string name = GetStrInput();
        string email = GetStrInput();
        string phoneNumber = GetStrInput();
        var newContact = new Contact { Name = name, Email = email, PhoneNumber = phoneNumber };
        controller.Create(newContact);
        Console.WriteLine("Contact was created!\nType anything to go back...");
        Console.ReadLine();
        MainMenu();
    }

    public void ReadingOne()
    {
        int id = GetIntInput();
        controller.ReadOne(id);
    }

    public int GetIntInput()
    {
        return Convert.ToInt32(Console.ReadLine());
    }

    public string GetStrInput()
    {
        return Console.ReadLine();
    }

}
