using System;
using Avalonia;
using Avalonia.Controls;

namespace RosewoodControl.Views.Components;

public partial class MainSection : UserControl
{
    public MainSection()
    {
        InitializeComponent();
    }

    private void GainSlider_OnPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.NewValue is double newGainSliderVal)
        {
            newGainSliderVal = Math.Round(newGainSliderVal, 2);
            Console.WriteLine(newGainSliderVal);
        }
    }
}