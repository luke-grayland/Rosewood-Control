using CommunityToolkit.Mvvm.ComponentModel;
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
    
    public MainWindowViewModel()
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
    }
}