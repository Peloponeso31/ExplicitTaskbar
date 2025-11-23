namespace ExplicitTaskbar.Services.Hooks;

public interface IActionHook
{
    public event Action OnAction;
    public void Start();
    public void Dispose();
}