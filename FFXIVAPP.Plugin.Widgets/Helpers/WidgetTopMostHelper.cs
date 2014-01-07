// FFXIVAPP.Plugin.Widgets
// WidgetTopMostHelper.cs
// 
// Copyright © 2013 ZAM Network LLC
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
// following conditions are met: 
// 
//  * Redistributions of source code must retain the above copyright notice, this list of conditions and the following 
//    disclaimer. 
//  * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the 
//    following disclaimer in the documentation and/or other materials provided with the distribution. 
//  * Neither the name of ZAM Network LLC nor the names of its contributors may be used to endorse or promote products 
//    derived from this software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using FFXIVAPP.Common.Helpers;
using FFXIVAPP.Common.RegularExpressions;
using FFXIVAPP.Plugin.Widgets.Interop;
using FFXIVAPP.Plugin.Widgets.Properties;

namespace FFXIVAPP.Plugin.Widgets.Helpers
{
    public static class WidgetTopMostHelper
    {
        private static WinAPI.WinEventDelegate _delegate;
        private static IntPtr _mainHandleHook;

        private static WindowInteropHelper _dpsWidgetInteropHelper;
        private static WindowInteropHelper _enmityWidgetInteropHelper;
        private static WindowInteropHelper _currentTargetWidgetInteropHelper;
        private static WindowInteropHelper _dtpsWidgetInteropHelper;
        private static WindowInteropHelper _focusTargetWidgetInteropHelper;
        private static WindowInteropHelper _hpsWidgetInteropHelper;

        private static WindowInteropHelper DPSWidgetInteropHelper
        {
            get { return _dpsWidgetInteropHelper ?? (_dpsWidgetInteropHelper = new WindowInteropHelper(Widgets.Instance.DPSWidget)); }
        }

        private static WindowInteropHelper EnmityWidgetInteropHelper
        {
            get { return _enmityWidgetInteropHelper ?? (_enmityWidgetInteropHelper = new WindowInteropHelper(Widgets.Instance.EnmityWidget)); }
        }

        private static WindowInteropHelper CurrentTargetWidgetInteropHelper
        {
            get { return _currentTargetWidgetInteropHelper ?? (_currentTargetWidgetInteropHelper = new WindowInteropHelper(Widgets.Instance.CurrentTargetWidget)); }
        }

        private static WindowInteropHelper DTPSWidgetInteropHelper
        {
            get { return _dtpsWidgetInteropHelper ?? (_dtpsWidgetInteropHelper = new WindowInteropHelper(Widgets.Instance.DTPSWidget)); }
        }

        private static WindowInteropHelper FocusTargetWidgetInteropHelper
        {
            get { return _focusTargetWidgetInteropHelper ?? (_focusTargetWidgetInteropHelper = new WindowInteropHelper(Widgets.Instance.FocusTargetWidget)); }
        }

        private static WindowInteropHelper HPSWidgetInteropHelper
        {
            get { return _hpsWidgetInteropHelper ?? (_hpsWidgetInteropHelper = new WindowInteropHelper(Widgets.Instance.HPSWidget)); }
        }


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
                var activeTitle = WinAPI.GetActiveWindowTitle();

                var stayOnTop = Application.Current.Windows.OfType<Window>()
                                           .Any(w => w.Title == activeTitle) || Regex.IsMatch(activeTitle.ToUpper(), @"^(FINAL FANTASY XIV)", SharedRegEx.DefaultOptions);

                // If any of the widgets are focused, don't try to hide any of them, or it'll prevent us from moving/closing them
                if (handle == DPSWidgetInteropHelper.Handle)
                {
                    return;
                }
                if (handle == EnmityWidgetInteropHelper.Handle)
                {
                    return;
                }
                if (handle == CurrentTargetWidgetInteropHelper.Handle)
                {
                    return;
                }
                if (handle == DTPSWidgetInteropHelper.Handle)
                {
                    return;
                }
                if (handle == FocusTargetWidgetInteropHelper.Handle)
                {
                    return;
                }
                if (handle == HPSWidgetInteropHelper.Handle)
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
