using System.Configuration;
Console.WriteLine(ConfigurationManager.ConnectionStrings["main"].ConnectionString);
var context = new ContactContext();
