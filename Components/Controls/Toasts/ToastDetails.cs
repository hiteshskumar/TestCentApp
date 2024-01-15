using System;

namespace ChargesWFM.UI.Components.Controls.Toasts;

public class ToastDetails
{
    public Guid Id => Guid.NewGuid();

    public bool AutoClose => ToastType != ToastType.Warning && ToastType != ToastType.Danger;

    public string Title { get; set; } = null!;

    public string Message { get; set; } = null!;

    public ToastType ToastType { get; set; }

    public DateTime CreatedTimeUtc { get; set; } = DateTime.UtcNow;
}