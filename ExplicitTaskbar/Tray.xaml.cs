using System.Windows;

namespace ExplicitTaskbar;

public partial class Tray : Window
{
    public Tray(TrayViewModel viewModel)
    {
        DataContext = viewModel;
        InitializeComponent();
    }
}