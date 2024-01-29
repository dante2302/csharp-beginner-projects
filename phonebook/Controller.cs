namespace phonebook
{
    public class Controller
    {
        private ContactContext context = new();
        public void Create(Contact newContact)
        {
            context.Add(newContact);
            context.SaveChanges();
        }
    }

    public class InputHandler
    {
        public void MainMenu()
        {
            Console.WriteLine();
        }

        public void Creation() 
        {
            Controller controller = new();
            string name = GetStrInput();
            string email = GetStrInput();
            string phoneNumber = GetStrInput();
            var newContact = new Contact { Name = name, Email = email, PhoneNumber = phoneNumber };
            controller.Create(newContact);
            Console.WriteLine("Contact was created!\nType anything to go back...");
            Console.ReadLine();
            MainMenu();
        }

        public string GetStrInput()
        {
            return Console.ReadLine();
        }

    }
}
