using ChatGptConsoleBot.Dto.Config;
using Microsoft.Extensions.Configuration;

namespace ChatGptConsoleBot.Factories;

internal class JsonConfigFactory : IConfigFactory
{
    public Config Create()
    {
        var configuration = this.BuildConfiguration();
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
