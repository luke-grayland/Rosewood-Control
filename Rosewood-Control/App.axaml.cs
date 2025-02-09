using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using RosewoodControl.ViewModels;
using RosewoodControl.Views;
using Microsoft.Extensions.Configuration;

namespace RosewoodControl;

public partial class App : Application
{
    public static IServiceProvider? Services { get; private set; }
    private static IConfiguration? Configuration { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Services = serviceCollection.BuildServiceProvider();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DisableAvaloniaDataAnnotationValidation();
            var mainWindowViewModel = Services.GetService<MainWindowViewModel>();
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainWindowViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        
        Configuration = configurationBuilder.Build();
        services.AddSingleton(Configuration);
        
        services.AddSingleton<HubConnection>(_ =>
        {
            try
            {
                var hubUrl = Configuration["AppSettings:HubUrl"] ??
                             throw new Exception("Unable to read Hub URL from config");
                
                Console.WriteLine($"Attempting to connect to hub: {hubUrl}");
                var connection = new HubConnectionBuilder()
                    .WithUrl(hubUrl)
                    .WithAutomaticReconnect()
                    .Build();

                connection.Reconnecting += (ex) =>
                {
                    Console.WriteLine($"Disconnected with error: {ex?.Message ?? "[none]"}");
                    Console.WriteLine("Reconnecting...");
                    return Task.CompletedTask;
                };
                
                connection.StartAsync();
                Console.WriteLine("Connected to hub");
                return connection;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to connect to hub: {e.Message}");
                throw;
            }
        });

        services.AddSingleton<MainWindowViewModel>();
    }
    
    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}