using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace ExplicitTaskbar.Models;
public class Taskbar
{
    private readonly HWND _handle;

    internal Taskbar(HWND handle)
    {
        _handle = handle;
    }
    
    public bool IsVisible() => PInvoke.IsWindowVisible(_handle);
    public bool Show() => PInvoke.ShowWindow(_handle, SHOW_WINDOW_CMD.SW_SHOW);
    public bool Hide() => PInvoke.ShowWindow(_handle, SHOW_WINDOW_CMD.SW_HIDE);
    public bool Focus() => PInvoke.SetForegroundWindow(_handle);
}