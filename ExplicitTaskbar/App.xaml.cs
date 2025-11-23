using System.Windows;
using ExplicitTaskbar.Services.Taskbar;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui.Appearance;
using System.Windows.Automation;
using ExplicitTaskbar.Actions;
using ExplicitTaskbar.Services.Hooks;

namespace ExplicitTaskbar;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider Services { get; }
    private static readonly ITaskbarService TaskbarService = App.Current.Services.GetService<ITaskbarService>();
    private static readonly IEnumerable<IActionHook> Hooks = App.Current.Services.GetServices<IActionHook>();

    public App()
    {
        Services = ConfigureServices();
        InitializeComponent();
        ApplicationThemeManager.ApplySystemTheme();
        Automation.AddAutomationFocusChangedEventHandler(OnFocusChangedHandler);
    }

    private void OnFocusChangedHandler(object sender, AutomationFocusChangedEventArgs e)
    {
        if (sender is AutomationElement element && TaskbarService.Status == TaskbarStatus.Hidden)
            TaskbarService.HideAllTaskbars();
    }

    public new static App Current => (App)Application.Current;

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<ITaskbarService, TaskbarService>();;
        services.AddSingleton<IActionHook, KeyDownActionHook>();
        services.AddSingleton<IActionHook, WinKeyActionHook>();
        services.AddSingleton<IActionHook, MouseClickUpActionHook>();

        return services.BuildServiceProvider();
    }

    private static void SetHooks()
    {
        var keyDownActionHook = Hooks.First(h => h.GetType() == typeof(KeyDownActionHook));
        var winKeyActionHook = Hooks.First(h => h.GetType() == typeof(WinKeyActionHook));
        var mouseClickUpActionHook = Hooks.First(h => h.GetType() == typeof(MouseClickUpActionHook));

        keyDownActionHook.OnAction += HideVisibleTaskbar.Action;
        winKeyActionHook.OnAction += ToggleTaskbarVisibility.Action;
        mouseClickUpActionHook.OnAction += HideVisibleTaskbar.Action;

        foreach (var hook in Hooks) hook.Start();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        SetHooks();
        TaskbarService.HideAllTaskbars();
        
    }

    protected override void OnExit(ExitEventArgs e)
    {
        foreach (var hook in Hooks) hook.Dispose();
        TaskbarService.ShowAllTaskbars();
        
        base.OnExit(e);
    }
}