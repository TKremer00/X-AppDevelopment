using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace FinalProject.Core.Helpers
{
    internal static class ToasterHelper
    {
        private const ToastDuration DEFAULT_DURATION = ToastDuration.Short;

        public static async Task Show(string message, ToastDuration duration = DEFAULT_DURATION)
        {
            string text = message;
            double fontSize = 14;
            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show();
        }


    }
}
