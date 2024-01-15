using System;
using System.Threading.Tasks;
using ChargesWFM.UI.Models;
using Microsoft.AspNetCore.Components.Authorization;

namespace ChargesWFM.UI.Services
{
    public enum Digits
    {
        Digit1 = 1,
        Digit2 = 2,
        Digit3 = 3,
        Digit4 = 4,
        Digit5 = 5,
        Digit6 = 6,
        Digit7 = 7,
        Digit8 = 8,
        Digit9 = 9,
    }

    public enum Numpad
    {
        Numpad1 = 1,
        Numpad2 = 2,
        Numpad3 = 3,
        Numpad4 = 4,
        Numpad5 = 5,
        Numpad6 = 6,
        Numpad7 = 7,
        Numpad8 = 8,
        Numpad9 = 9,
    }

    public interface IHotKeyService
    {

        event EventHandler OpenCompletedAction;
        event EventHandler OpenPendededAction;
        event EventHandler OpenFetchAction;
        event EventHandler SkipAction;

        void OnKeyDown(bool ctrlKey, bool altKey, bool shiftKey, string key);
    }

    public class HotKeyService : IHotKeyService
    {
        public HotKeyService()
        {
        }

        

        public event EventHandler OpenCompletedAction;
        public event EventHandler OpenPendededAction;
        public event EventHandler OpenFetchAction;
        public event EventHandler SkipAction;

        

        public void OnKeyDown(bool ctrlKey, bool altKey, bool shiftKey, string key)
        {
            var isValid = GetKeyNumberValue(key, out int retValue);
            if (altKey && shiftKey && string.Equals(key, "keyf", StringComparison.CurrentCultureIgnoreCase))
            {
                OpenFetchAction?.Invoke(this, EventArgs.Empty);
            }
            else if (altKey && shiftKey && string.Equals(key, "keyc", StringComparison.CurrentCultureIgnoreCase))
            {
                OpenCompletedAction?.Invoke(this, EventArgs.Empty);
            }
            else if (altKey && shiftKey && string.Equals(key, "keyp", StringComparison.CurrentCultureIgnoreCase))
            {
                OpenPendededAction?.Invoke(this, EventArgs.Empty);
            }
            else if (altKey && shiftKey && string.Equals(key, "keys", StringComparison.CurrentCultureIgnoreCase))
            {
                SkipAction?.Invoke(this, EventArgs.Empty);
            }
            else if (altKey && string.Equals(key, "keyc", StringComparison.CurrentCultureIgnoreCase))
            {
                OpenCompletedAction?.Invoke(this, EventArgs.Empty);
            }
            else if (altKey && string.Equals(key, "keyp", StringComparison.CurrentCultureIgnoreCase))
            {
                OpenPendededAction?.Invoke(this, EventArgs.Empty);
            }
            else if (altKey && string.Equals(key, "keys", StringComparison.CurrentCultureIgnoreCase))
            {
                SkipAction?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool GetKeyNumberValue(string key, out int retValue)
        {
            retValue = 0;
            var isValid = Enum.TryParse<Digits>(key, ignoreCase: true, out var digitValue);
            if (!isValid)
            {
                isValid = Enum.TryParse<Numpad>(key, ignoreCase: true, out var numValue);
                if (isValid)
                {
                    retValue = (int)numValue;
                }
            }
            else
            {
                retValue = (int)digitValue;
            }

            return isValid;
        }
    }

    public class AccountTapIndexEventArgs : EventArgs
    {
        public int TapIndex { get; set; }
    }
}
