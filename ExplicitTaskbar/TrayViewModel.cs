using System.Diagnostics;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ExplicitTaskbar;

public partial class TrayViewModel : ObservableObject
{
    private string _aboutUrl = "https://github.com/Peloponeso31/ExplicitTaskbar";
    
    [RelayCommand]
    private void OpenAbout()
    {
        try
        {
            Process.Start(new ProcessStartInfo(_aboutUrl) { UseShellExecute = true });
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
    
    [RelayCommand]
    private void Close()
    {
        Application.Current.Shutdown();
    }
}