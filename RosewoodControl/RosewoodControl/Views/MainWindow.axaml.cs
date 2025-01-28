using System;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace RosewoodControl.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        LukeButton.Content = "testing";
    }

    public void LukeButtonClickHandler(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("Clicked!");
    }
}