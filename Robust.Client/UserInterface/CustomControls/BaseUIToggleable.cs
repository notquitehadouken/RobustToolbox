using System;
using Robust.Client.UserInterface.Controls;

namespace Robust.Client.UserInterface.CustomControls;

public abstract class BaseUIToggleable : Control
{
    public bool IsOpen => Parent is not null;

    public event Action? OnClose;
    public event Action? OnOpen;

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

    public virtual void Open(LayoutContainer? OpenParent = null)
    {
        if (!IsOpen)
            (OpenParent ?? UserInterfaceManager.WindowRoot).AddChild(this);

        Opened();
        OnOpen?.Invoke();
    }

    protected virtual void Opened()
    {

    }
}
