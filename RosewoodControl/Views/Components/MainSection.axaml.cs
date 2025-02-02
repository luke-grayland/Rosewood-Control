using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;

namespace RosewoodControl.Views.Components;

public partial class MainSection : UserControl
{
    private readonly HubConnection _hubConnection;
    
    public MainSection()
    {
        InitializeComponent();
        _hubConnection = App.Services.GetRequiredService<HubConnection>();
    }

    private void GainSlider_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is double newGainSliderVal)
        {
            newGainSliderVal = Math.Round(newGainSliderVal, 2);
            Console.WriteLine(newGainSliderVal);
            
            Task.Run(async () =>
            {
                if (_hubConnection.State != HubConnectionState.Connected)
                {
                    Console.WriteLine("No active connection");
                    return;
                }
                await _hubConnection.InvokeAsync("SendCommand", "Pi123", newGainSliderVal.ToString());
            });
        }
    }
}