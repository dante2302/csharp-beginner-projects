using ConsoleTableExt;

public static class Printer
{
    public static void PrintContacts(List<Contact> contactList)
    {
        ConsoleTableBuilder
            .From(contactList)
            .WithTitle("Contacts")
            .ExportAndWriteLine();
    }

    public static void PrintOneContact(Contact contact)
    {
        List<Contact> tempList = [contact];
        ConsoleTableBuilder
            .From(tempList)
            .WithTitle($"Contact #{contact.Id}")
            .ExportAndWriteLine();
    }

}