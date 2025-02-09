using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.AspNetCore.SignalR.Client;
using RosewoodControl.Constants;
using RosewoodControl.Models;

namespace RosewoodControl.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private Speaker _speaker1;
    
    [ObservableProperty]
    private Speaker _speaker2;
    
    [ObservableProperty]
    private Speaker _speaker3;
    
    [ObservableProperty]
    private Speaker _speaker4;

    private HubConnection _hubConnection;
    
    public MainWindowViewModel(HubConnection hubConnection)
    {
        Speaker1 = new();
        Speaker2 = new();
        Speaker3 = new();
        Speaker4 = new();
        
        Speaker1.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Speaker1));
        Speaker2.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Speaker2));
        Speaker3.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Speaker3));
        Speaker4.PropertyChanged += (s, e) => OnPropertyChanged(nameof(Speaker4));

        Speaker1.Status = SpeakerConst.Status.Active;
        Speaker1.Colour = SpeakerConst.Colour.Active;
        
        Speaker2.Status = SpeakerConst.Status.Active;
        Speaker2.Colour = SpeakerConst.Colour.Active;

        _hubConnection = hubConnection;
        _hubConnection.On<string, string>("ReceiveDeviceData", LogOnMessage);
    }

    private void LogOnMessage(string deviceId, string data)
    {
        if (data == "1")
        {
            Speaker2.Status = SpeakerConst.Status.Blown;
            Speaker2.Colour = SpeakerConst.Colour.Blown;
        }
        
        if (data == "2")
        {
            Speaker2.Status = SpeakerConst.Status.Active;
            Speaker2.Colour = SpeakerConst.Colour.Active;
        }
        
        Console.WriteLine($"{deviceId}: {data}");
    }
}