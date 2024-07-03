using BlazorWASM;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Json;

namespace Company.WebApplication1;
public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(@"https://api.miraclecat.com/") });

        await builder.Build().RunAsync();
    }
}