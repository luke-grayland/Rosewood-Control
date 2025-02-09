using Avalonia;
using Avalonia.Controls.Primitives;
using RosewoodControl.Constants;

namespace RosewoodControl.Views.Components;

public class SpeakerControl : TemplatedControl
{
    public static readonly StyledProperty<string> StatusTextProperty = AvaloniaProperty.Register<SpeakerControl, string>(
        nameof(StatusText), SpeakerConst.Status.Offline);

    public string StatusText
    {
        get => GetValue(StatusTextProperty);
        set => SetValue(StatusTextProperty, value);
    }

    public static readonly StyledProperty<string> StatusColourProperty = AvaloniaProperty.Register<SpeakerControl, string>(
        nameof(StatusColour), SpeakerConst.Colour.Offline);

    public string StatusColour
    {
        get => GetValue(StatusColourProperty);
        set => SetValue(StatusColourProperty, value);
    }
}