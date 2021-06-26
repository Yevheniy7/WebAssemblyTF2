using BlazorClient.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;
using ModelBuilder.DataModel;
using System;
using System.IO;
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

            string rootDir = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../"));
            string modelPath = Path.Combine(rootDir, "TFInceptionModel/PredictionModel.zip");

            Uri BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            String x = BaseAddress.ToString() + "data/TFInceptionModel/PredictionModel.zip";


            //Problem here
            builder.Services.AddPredictionEnginePool<ImageInputData, ImageLabelPredictions>().FromFile("TFInceptionModel/PredictionModel.zip");

            builder.Services.AddSingleton<ImageClassificationService>();

            await builder.Build().RunAsync();
        }
    }
}

//https://github.com/dotnet/aspnetcore/issues/22400

//télécharger dans une variable 

//ssh nom de la machine 
//port 80