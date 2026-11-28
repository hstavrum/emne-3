using _330C;
using Microsoft.Extensions.Hosting;
using RazorConsole.Core;
using System.Threading.Tasks;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.UseRazorConsole<Brusautomat>();

        using var host = builder.Build();
        await host.RunAsync();
    }
}