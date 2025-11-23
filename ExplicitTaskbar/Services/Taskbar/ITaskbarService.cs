namespace ExplicitTaskbar.Services.Taskbar;

public enum TaskbarStatus
{
    Hidden,
    Visible,
}

public enum TaskbarType
{
    Primary,
    Second,
    Third,
    Forth,
    Fifth,
    Sixth,
    Seventh,
    Eighth,
    Ninth,
    Tenth,
}

public interface ITaskbarService
{
    /// <summary>
    /// Gets the current visibility state of the taskbar, represented by the
    /// <see cref="TaskbarStatus"/> enumeration.  
    /// This value indicates whether the taskbar is currently <see cref="TaskbarStatus.Visible"/>
    /// or <see cref="TaskbarStatus.Hidden"/>.
    /// </summary>
    public TaskbarStatus Status { get; }
    
    /// <summary>
    /// Retrieves all taskbars currently detected on the system.
    /// </summary>
    /// <returns>
    /// A <see cref="List{T}"/> of <see cref="Models.Taskbar"/> instances,
    /// where the first element represents the primary taskbar and any
    /// subsequent elements correspond to secondary taskbars on additional monitors.
    /// </returns>
    public List<Models.Taskbar> GetTaskbars();

    /// <summary>
    /// Hides every detected taskbar window, including the primary and all
    /// secondary taskbars.  
    /// Uses reliable window manipulation flags to ensure that each taskbar
    /// is fully hidden regardless of monitor count or auto-hide settings.
    /// </summary>
    public void HideAllTaskbars();

    /// <summary>
    /// Restores visibility for the primary taskbar and all secondary
    /// taskbars that were previously hidden.  
    /// Ensures that each taskbar window is shown consistently across all
    /// monitors, even after display configuration changes.
    /// </summary>
    public void ShowAllTaskbars();

    /// <summary>
    /// Brings the primary taskbar window to the foreground or activates it
    /// for interaction.  
    /// This targets only the main taskbar (<c>Shell_TrayWnd</c>) and does not
    /// affect secondary taskbars on additional monitors.
    /// </summary>
    public void FocusPrimaryTaskbar();
}