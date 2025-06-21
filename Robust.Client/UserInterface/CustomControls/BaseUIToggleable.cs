using System;
using Robust.Client.UserInterface.Controls;

namespace Robust.Client.UserInterface.CustomControls;

public abstract class BaseUIToggleable : Container
{
    public bool IsOpen => Parent is not null;

    public event Action? OnOpen;
    public event Action? OnClose;

    public void Toggle()
    {
        if (IsOpen)
            Close();
        else
            Open();
    }

    public virtual void Close()
    {
        if (!IsOpen)
            return;

        Parent!.RemoveChild(this);
        OnClose?.Invoke();
    }

    public virtual void Open(LayoutContainer? OverrideOpener = null)
    {
        if (IsOpen)
            return;

        (OverrideOpener ?? UserInterfaceManager.WindowRoot).AddChild(this);
        OnOpen?.Invoke();
    }
}
