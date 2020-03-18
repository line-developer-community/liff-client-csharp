using LineDC.Liff;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace LineDC.LiffOnBlazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            
#if DEBUG
            var liffId  = "1653926279-KLQm83d2";
#else       
            var liffId    = "1653926279-Q4lOAB98";
#endif
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddBaseAddressHttpClient();
            builder.Services.AddSingleton<ILiffClient>(new LiffClient(liffId));
            var host = builder.Build();
            await host.RunAsync();
        }

    }
}
