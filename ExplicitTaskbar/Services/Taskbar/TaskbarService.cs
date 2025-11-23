using Windows.Win32;
using Windows.Win32.Foundation;

namespace ExplicitTaskbar.Services.Taskbar;

public class TaskbarService : ITaskbarService
{
    private const string TaskbarClassName = "Shell_TrayWnd";
    private const string SecondaryTaskbarClassName = "Shell_SecondaryTrayWnd";
    private readonly List<Models.Taskbar> _taskbars = [];
    public TaskbarStatus Status { get; private set; }

    public TaskbarService()
    {
        FindTaskbars();
    }

    private void FindTaskbars()
    {
        var primaryTaskbarHandle = PInvoke.FindWindow(TaskbarClassName, null);
        _taskbars.Add(new Models.Taskbar(primaryTaskbarHandle));

        HWND secondaryTaskbarHandle = default;
        while (true)
        {
            secondaryTaskbarHandle =
                PInvoke.FindWindowEx(HWND.Null, secondaryTaskbarHandle, SecondaryTaskbarClassName, null);
            if (secondaryTaskbarHandle == IntPtr.Zero) break;
            _taskbars.Add(new Models.Taskbar(secondaryTaskbarHandle));
        }
    }

    public void HideAllTaskbars()
    {
        foreach (var taskbar in _taskbars) taskbar.Hide();
        Status = TaskbarStatus.Hidden;
    }

    public void ShowAllTaskbars()
    {
        foreach (var taskbar in _taskbars) taskbar.Show();
        Status = TaskbarStatus.Visible;
    }

    public void FocusPrimaryTaskbar()
    {
        var primaryTaskbar = _taskbars.First();
        primaryTaskbar.Show();
        primaryTaskbar.Focus();
        
        Status = TaskbarStatus.Visible;
    }

    public List<Models.Taskbar> GetTaskbars() => _taskbars;
}