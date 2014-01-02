// FFXIVAPP.Plugin.Widgets
// WidgetTopMostHelper.cs
// 
// © 2013 ZAM Network LLC

using System;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using FFXIVAPP.Common.Helpers;
using FFXIVAPP.Plugin.Widgets.Interop;
using FFXIVAPP.Plugin.Widgets.Properties;

namespace FFXIVAPP.Plugin.Widgets.Helpers
{
    public static class WidgetTopMostHelper
    {
        private static WinAPI.WinEventDelegate _delegate;
        private static IntPtr _mainHandleHook;

        private static Timer SetWindowTimer { get; set; }

        public static void HookWidgetTopMost()
        {
            try
            {
                _delegate = BringWidgetsIntoFocus;
                _mainHandleHook = WinAPI.SetWinEventHook(WinAPI.EVENT_SYSTEM_FOREGROUND, WinAPI.EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, _delegate, 0, 0, WinAPI.WINEVENT_OUTOFCONTEXT);
            }
            catch (Exception e)
            {
            }
            SetWindowTimer = new Timer(1000);
            SetWindowTimer.Elapsed += SetWindowTimerOnElapsed;
            SetWindowTimer.Start();
        }

        private static void SetWindowTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            DispatcherHelper.Invoke(() => BringWidgetsIntoFocus(), DispatcherPriority.Normal);
        }

        private static void BringWidgetsIntoFocus(IntPtr hwineventhook, uint eventtype, IntPtr hwnd, int idobject, int idchild, uint dweventthread, uint dwmseventtime)
        {
            BringWidgetsIntoFocus(true);
        }

        private static void BringWidgetsIntoFocus(bool force = false)
        {
            try
            {
                var handle = WinAPI.GetForegroundWindow();
                var activeTitle = WinAPI.GetActiveWindowTitle()
                                        .ToUpper();
                var stayOnTop = Regex.IsMatch(activeTitle, @"^(FFXIVAPP ~ |FINAL FANTASY XIV)", Common.RegularExpressions.SharedRegEx.DefaultOptions);
                // If any of the widgets are focused, don't try to hide any of them, or it'll prevent us from moving/closing them
                if (handle == new WindowInteropHelper(Widgets.Instance.DPSWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.EnmityWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.CurrentTargetWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.DTPSWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.FocusTargetWidget).Handle)
                {
                    return;
                }
                if (handle == new WindowInteropHelper(Widgets.Instance.HPSWidget).Handle)
                {
                    return;
                }
                if (Settings.Default.ShowDPSWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.DPSWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowEnmityWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.EnmityWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowCurrentTargetWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.CurrentTargetWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowDTPSWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.DTPSWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowFocusTargetWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.FocusTargetWidget, stayOnTop, force);
                }
                if (Settings.Default.ShowHPSWidgetOnLoad)
                {
                    ToggleTopMost(Widgets.Instance.HPSWidget, stayOnTop, force);
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="window"></param>
        /// <param name="stayOnTop"></param>
        /// <param name="force"></param>
        private static void ToggleTopMost(Window window, bool stayOnTop, bool force)
        {
            if (window.Topmost && stayOnTop && !force)
            {
                return;
            }
            window.Topmost = false;
            if (!stayOnTop)
            {
                if (window.IsVisible)
                {
                    window.Hide();
                }
                return;
            }
            window.Topmost = true;
            if (!window.IsVisible)
            {
                window.Show();
            }
        }
    }
}
