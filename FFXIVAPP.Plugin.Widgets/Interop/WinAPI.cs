// FFXIVAPP.Plugin.Widgets
// WinAPI.cs
// 
// © 2013 ZAM Network LLC

using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using FFXIVAPP.Plugin.Widgets.Properties;

namespace FFXIVAPP.Plugin.Widgets.Interop
{
    public static class WinAPI
    {
        private const int WS_EX_TRANSPARENT = 0x00000020;
        private const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

        private static void SetWindowTransparent(IntPtr hwnd)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
        }

        private static void SetWindowLayered(IntPtr hwnd)
        {
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            extendedStyle &= ~WS_EX_TRANSPARENT;
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle);
        }

        public static void ToggleClickThrough(Window window)
        {
            try
            {
                var hWnd = new WindowInteropHelper(window).Handle;
                if (Settings.Default.WidgetClickThroughEnabled)
                {
                    SetWindowTransparent(hWnd);
                }
                else
                {
                    SetWindowLayered(hWnd);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
