using ExplicitTaskbar.Services.Taskbar;
using Microsoft.Extensions.DependencyInjection;

namespace ExplicitTaskbar.Actions;

public abstract class HideVisibleTaskbar
{ 
    public static void Action()
    {
        var taskbarService = App.Current.Services.GetService<ITaskbarService>();
        
        if (taskbarService.Status == TaskbarStatus.Visible)
        {
            taskbarService.HideAllTaskbars();
        }
    }
}