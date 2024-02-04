public static class Configuration
{
    private static IConfigurationRoot _manager = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

    public static IConfigurationRoot Manager { get { return _manager; } }
}
