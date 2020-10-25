using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SqlPocoGenerator.Services;
using System.Threading.Tasks;

namespace SqlPocoGenerator
{
    public class Program
    {
        public static Task Main()
        {
            return new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<SqlPocoGeneratorService>();
                })
                .RunConsoleAsync();
        }
    }
}