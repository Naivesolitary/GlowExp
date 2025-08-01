// File: TagInputHelper.cs
using Microsoft.AspNetCore.Components;

namespace GlowExp.Components.Helpers
{
    public static class TagInputHelper
    {
        public static void HandleTagChange<T>(ChangeEventArgs e,
            T transaction,
            Action<bool> setCustomInputVisible,
            Action<string> setTagValue)
        {
            var selected = e.Value?.ToString();

            if (selected == "custom")
            {
                setCustomInputVisible(true);
                setTagValue("");
            }
            else
            {
                setCustomInputVisible(false);
                setTagValue(selected);
            }
        }

        public static void UpdateCustomTag<T>(
            ChangeEventArgs e,
            T transaction,
            Action<string> setTagValue)
        {
            var input = e.Value?.ToString();
            setTagValue(input);
        }
    }
}
