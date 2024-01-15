using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
//using System.Timers;
using ChargesWFM.UI.Services;
using Microsoft.AspNetCore.Components;

namespace ChargesWFM.UI.Components.Controls.Toasts;

public partial class ToastContainer : ComponentBase, IDisposable
{
    private readonly System.Timers.Timer timer = new System.Timers.Timer(1000);
    private readonly List<ToastDetails> toasts = new List<ToastDetails>();
    private bool disposedValue;

    [Inject]
    public IToasterService? ToasterService { get; set; }

    private string style => toasts.Any() ? "z-index: 1056 !important;" : string.Empty;

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected override Task OnInitializedAsync()
    {
        timer.Elapsed += TimerElapsed;
        ToasterService.AddToast += AddToast;
        return Task.CompletedTask;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                if (timer.Enabled)
                {
                    timer.Stop();
                }

                timer.Elapsed -= TimerElapsed;
                ToasterService.AddToast -= AddToast;
                
                timer.Close();
                timer.Dispose();
            }

            disposedValue = true;
        }
    }

    private static string GetTimeDisplayText(DateTime dateTimeUtc)
    {
        var timespan = DateTime.UtcNow - dateTimeUtc;
        if (timespan.TotalMinutes <= 1.0)
        {
            return "Few seconds ago";
        }

        return $"{(int)timespan.TotalMinutes} minutes ago";
    }

    private static string GetToastTypeIcon(ToastType toastType) => toastType switch
    {
        ToastType.Warning => "fa-triangle-exclamation text-warning",
        ToastType.Danger => "fa-triangle-exclamation text-danger",
        ToastType.Success => "fa-circle-info text-success",
        ToastType.Info => "fa-circle-info text-info",
        ToastType.Primary => "fa-circle-info text-primary",
        ToastType.Secondary => "fa-circle-info text-secondary",
        _ => "fa-circle-info",
    };

    private static string GetBackgroundColorClass(ToastType toastType) => toastType switch
    {
        ToastType.Secondary => "bg-secondary",
        ToastType.Success => "bg-success",
        ToastType.Warning => "bg-warning",
        ToastType.Danger => "bg-danger",
        ToastType.Info => "bg-info",
        _ => "bg-primary",
    };

    private static string GetBorderCss(ToastType toastType) => toastType switch
    {
        ToastType.Secondary => "border-left-color: var(--secondary) !important;",
        ToastType.Success => "border-left-color: var(--success) !important;",
        ToastType.Warning => "border-left-color: var(--warning) !important;",
        ToastType.Danger => "border-left-color: var(--danger) !important;",
        ToastType.Info => "border-left-color: var(--info) !important;",
        _ => "border-left-color: var(--primary) !important;",
    };

    private void AddToast(object? sender, ToastDetails toastDetails)
    {
        toasts.Add(toastDetails);
        timer.Enabled = true;
        StateHasChanged();
    }

    private void RemoveToast(ToastDetails toastDetails)
    {
        toasts.Remove(toastDetails);
        StateHasChanged();
    }

    private void TimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
    {
        if (toasts.Count == 0)
        {
            timer.Enabled = false;
            return;
        }

        var removeToasts = new List<ToastDetails>();
        foreach (var toast in toasts.Where(toast => toast.AutoClose))
        {
            var timespan = DateTime.UtcNow - toast.CreatedTimeUtc;
            if (timespan.TotalSeconds > 5)
            {
                removeToasts.Add(toast);
            }
        }

        foreach (var toast in removeToasts)
        {
            toasts.Remove(toast);
        }

        if (toasts.Count == 0)
        {
            timer.Enabled = false;
        }

        StateHasChanged();
    }
}