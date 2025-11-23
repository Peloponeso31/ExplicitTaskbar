using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.WindowsAndMessaging;

namespace ExplicitTaskbar.Services.Hooks;

public class MouseClickUpActionHook : IActionHook
{
    public event Action? OnAction = () => { };
    private HHOOK _hook;
    private HOOKPROC _hookProc;

    public void Start()
    {
        _hookProc = Callback;

        _hook = PInvoke.SetWindowsHookEx(
            WINDOWS_HOOK_ID.WH_MOUSE_LL,
            _hookProc,
            HINSTANCE.Null,
            0
        );
    }

    public void Dispose()
    {
        if (!_hook.IsNull)
            PInvoke.UnhookWindowsHookEx(_hook);
    }

    private LRESULT Callback(int nCode, WPARAM wParam, LPARAM lParam)
    {
        if (nCode >= 0)
        {
            var msg = (uint)wParam;
            // https://learn.microsoft.com/en-us/windows/win32/inputdev/mouse-input-notifications
            var clickUp = msg is 0x0202 /*WM_LBUTTONUP*/ or 0x0205 /*WM_RBUTTONUP*/;
            if (clickUp) OnAction?.Invoke();
        }

        return PInvoke.CallNextHookEx(_hook, nCode, wParam, lParam);
    }
}