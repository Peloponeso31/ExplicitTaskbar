using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;

namespace ExplicitTaskbar.Services.Hooks;

public class KeyDownActionHook : IActionHook
{
    public event Action? OnAction = () => { };
    private HHOOK _hook;
    private HOOKPROC _hookProc;

    public void Start()
    {
        _hookProc = Callback;

        _hook = PInvoke.SetWindowsHookEx(
            WINDOWS_HOOK_ID.WH_KEYBOARD_LL,
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
            var info = Marshal.PtrToStructure<KBDLLHOOKSTRUCT>(lParam);

            // https://learn.microsoft.com/en-us/windows/win32/inputdev/keyboard-input-notifications
            var isKeyDown = msg is 0x0100 /*WM_KEYDOWN*/ or 0x0104 /*WM_SYSKEYDOWN*/;
            var isEscapeOrEnter = info.vkCode is (uint)VIRTUAL_KEY.VK_ESCAPE or (uint)VIRTUAL_KEY.VK_RETURN;

            if (isKeyDown && isEscapeOrEnter) OnAction?.Invoke();
        }

        return PInvoke.CallNextHookEx(_hook, nCode, wParam, lParam);
    }
}