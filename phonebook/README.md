# Contact Management Console Application
This console application allows users to manage contacts and their phone numbers stored in a SQL Server database. Below are the requirements fulfilled by this application:

 ## Requirements Fulfilled
- Add Contact
- View Contacts
- Update Contacts
- Delete Contacts
- ### Contact Class Structure
```csharp
class Contact
{
  public int Id { get; set; }
  public string Name { get; set; }
  public string PhoneNumber { get; set; }
  public string Email { get; set; }
}
```
- Code-First Approach with EF Core
- SQL Server as the backend database.
- The connection string to the SQL Server instance is configurable via an appsettings.json file.
