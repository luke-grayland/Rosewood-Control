using CommunityToolkit.Mvvm.ComponentModel;
using RosewoodControl.Constants;

namespace RosewoodControl.Models;

public partial class Speaker : ObservableObject
{
    [ObservableProperty]
    private string _status = SpeakerConst.Status.Offline;
    
    [ObservableProperty]
    private string _colour = SpeakerConst.Colour.Offline;
}