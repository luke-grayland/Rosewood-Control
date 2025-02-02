using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Markup.Xaml;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using RosewoodControl.ViewModels;
using RosewoodControl.Views;

namespace RosewoodControl;

public partial class App : Application
{
    public static IServiceProvider Services { get; private set; }
    
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
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<HubConnection>(provider =>
        {
            try
            {
                Console.WriteLine("Attempting to connect to hub");
                var connection = new HubConnectionBuilder()
                    .WithUrl("https://localhost:7181/DeviceHub")
                    .WithAutomaticReconnect()
                    .Build();

                connection.Reconnecting += (error) =>
                {
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