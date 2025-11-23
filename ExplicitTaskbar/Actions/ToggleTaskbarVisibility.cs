using ExplicitTaskbar.Services.Taskbar;
using Microsoft.Extensions.DependencyInjection;

namespace ExplicitTaskbar.Actions;

public abstract class ToggleTaskbarVisibility
{
    public static void Action()
    {
        var taskbarService = App.Current.Services.GetService<ITaskbarService>();
        
        switch (taskbarService.Status)
        {
            case TaskbarStatus.Hidden:
                taskbarService.FocusPrimaryTaskbar();
                break;
            
            case TaskbarStatus.Visible:
                taskbarService.HideAllTaskbars();
                break;
        }
    }
}