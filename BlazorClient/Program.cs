using BlazorClient.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using ModelBuilder.DataModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


            builder.Services.AddPredictionEnginePool<ImageInputData, ImageLabelPredictions>()
            .FromFile(PathUtilities.GetPathFromBinFolder("PredictionModel.zip"));

            builder.Services.AddSingleton<ImageClassificationService>();

            await builder.Build().RunAsync();
        }
    }
}
