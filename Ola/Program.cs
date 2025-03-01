using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ola.Application;
using Ola.Application.TargetResolution;
using Ola.Domain;
using Ola.Domain.Configuration;
using Ola.Domain.FileSystem;
using Ola.Domain.Logging;
using Ola.Domain.TargetResolution;
using Ola.FileSystem;
using Ola.Logging;

namespace Ola;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.Configure<ToolOptions>(builder.Configuration);
        builder.Services.AddSingleton<ILogger, Logger>();
        builder.Services.AddTransient<IFileService, FileService>();
        builder.Services.AddTransient<ITargetResolutionService, TargetResolutionService>();
        builder.Services.AddTransient<IMainService, MainService>();
        builder.Services.AddHostedService<ServiceShell>();
        
        var host = builder.Build();
        await host.RunAsync();
    }
}