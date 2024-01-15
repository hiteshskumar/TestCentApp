using System;
using ChargesWFM.UI.Components.Controls.Toasts;

namespace ChargesWFM.UI.Services;

public interface IToasterService
{
    event EventHandler<ToastDetails> AddToast;

    void Add(string title, string message, ToastType toastType);

    void AddDanger(string title, string message);

    void AddPrimary(string title, string message);

    void AddSecondary(string title, string message);

    void AddSuccess(string title, string message);

    void AddWarning(string title, string message);
}

public class ToasterService : IToasterService
{
    public event EventHandler<ToastDetails> AddToast;

    public void AddPrimary(string title, string message)
    {
        Add(title, message, ToastType.Primary);
    }

    public void AddSecondary(string title, string message)
    {
        Add(title, message, ToastType.Secondary);
    }

    public void AddSuccess(string title, string message)
    {
        Add(title, message, ToastType.Success);
    }

    public void AddWarning(string title, string message)
    {
        Add(title, message, ToastType.Warning);
    }

    public void AddDanger(string title, string message)
    {
        Add(title, message, ToastType.Danger);
    }

    public void Add(string title, string message, ToastType toastType)
    {
        var toastDetails = new ToastDetails
        {
            Message = message,
            Title = title,
            ToastType = toastType,
        };
        AddToast?.Invoke(this, toastDetails);
    }
}