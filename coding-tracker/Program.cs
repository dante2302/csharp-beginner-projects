using System.Configuration;
string sAttr;
sAttr = ConfigurationManager.ConnectionStrings["cstring"].ConnectionString;
Console.WriteLine(sAttr);
Console.WriteLine($"The value of Key0 is {sAttr}");

