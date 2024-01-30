using Microsoft.Extensions.Configuration;

namespace Configuration;

public class JsonConfigFactory : IConfigFactory
{
    public Config Create()
    {
        var configuration = BuildConfiguration();
        return configuration.Get<Config>();
    }

    private IConfigurationRoot BuildConfiguration()
    {
        return new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
    }
}
