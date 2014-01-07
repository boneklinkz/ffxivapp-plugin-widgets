// FFXIVAPP.Plugin.Widgets
// MainViewModel.cs
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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FFXIVAPP.Common.ViewModelBase;
using FFXIVAPP.Plugin.Widgets.Properties;

namespace FFXIVAPP.Plugin.Widgets.ViewModels
{
    internal sealed class MainViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static MainViewModel _instance;

        public static MainViewModel Instance
        {
            get { return _instance ?? (_instance = new MainViewModel()); }
        }

        #endregion

        #region Declarations

        public ICommand ResetDPSWidgetCommand { get; private set; }
        public ICommand OpenDPSWidgetCommand { get; private set; }
        public ICommand ResetDTPSWidgetCommand { get; private set; }
        public ICommand OpenDTPSWidgetCommand { get; private set; }
        public ICommand ResetHPSWidgetCommand { get; private set; }
        public ICommand OpenHPSWidgetCommand { get; private set; }
        public ICommand ResetEnmityWidgetCommand { get; private set; }
        public ICommand OpenEnmityWidgetCommand { get; private set; }
        public ICommand ResetFocusTargetWidgetCommand { get; private set; }
        public ICommand OpenFocusTargetWidgetCommand { get; private set; }
        public ICommand ResetCurrentTargetWidgetCommand { get; private set; }
        public ICommand OpenCurrentTargetWidgetCommand { get; private set; }

        #endregion

        #region Loading Functions

        #endregion

        public MainViewModel()
        {
            ResetDPSWidgetCommand = new DelegateCommand(ResetDPSWidget);
            OpenDPSWidgetCommand = new DelegateCommand(OpenDPSWidget);
            ResetDTPSWidgetCommand = new DelegateCommand(ResetDTPSWidget);
            OpenDTPSWidgetCommand = new DelegateCommand(OpenDTPSWidget);
            ResetHPSWidgetCommand = new DelegateCommand(ResetHPSWidget);
            OpenHPSWidgetCommand = new DelegateCommand(OpenHPSWidget);
            ResetEnmityWidgetCommand = new DelegateCommand(ResetEnmityWidget);
            OpenEnmityWidgetCommand = new DelegateCommand(OpenEnmityWidget);
            ResetFocusTargetWidgetCommand = new DelegateCommand(ResetFocusTargetWidget);
            OpenFocusTargetWidgetCommand = new DelegateCommand(OpenFocusTargetWidget);
            ResetCurrentTargetWidgetCommand = new DelegateCommand(ResetCurrentTargetWidget);
            OpenCurrentTargetWidgetCommand = new DelegateCommand(OpenCurrentTargetWidget);
        }

        #region Utility Functions

        #endregion

        #region Command Bindings

        public void ResetDPSWidget()
        {
            Settings.Default.DPSWidgetUIScale = Settings.Default.Properties["DPSWidgetUIScale"].DefaultValue.ToString();
            Settings.Default.DPSWidgetTop = Int32.Parse(Settings.Default.Properties["DPSWidgetTop"].DefaultValue.ToString());
            Settings.Default.DPSWidgetLeft = Int32.Parse(Settings.Default.Properties["DPSWidgetLeft"].DefaultValue.ToString());
            Settings.Default.DPSWidgetHeight = Int32.Parse(Settings.Default.Properties["DPSWidgetHeight"].DefaultValue.ToString());
            Settings.Default.DPSWidgetWidth = Int32.Parse(Settings.Default.Properties["DPSWidgetWidth"].DefaultValue.ToString());
            Settings.Default.DPSVisibility = Settings.Default.Properties["DPSVisibility"].DefaultValue.ToString();
            Settings.Default.DPSWidgetSortDirection = Settings.Default.Properties["DPSWidgetSortDirection"].DefaultValue.ToString();
            Settings.Default.DPSWidgetSortProperty = Settings.Default.Properties["DPSWidgetSortProperty"].DefaultValue.ToString();
        }

        public void OpenDPSWidget()
        {
            Settings.Default.ShowDPSWidgetOnLoad = true;
            Widgets.Instance.ShowDPSWidget();
        }

        public void ResetDTPSWidget()
        {
            Settings.Default.DTPSWidgetUIScale = Settings.Default.Properties["DTPSWidgetUIScale"].DefaultValue.ToString();
            Settings.Default.DTPSWidgetTop = Int32.Parse(Settings.Default.Properties["DTPSWidgetTop"].DefaultValue.ToString());
            Settings.Default.DTPSWidgetLeft = Int32.Parse(Settings.Default.Properties["DTPSWidgetLeft"].DefaultValue.ToString());
            Settings.Default.DTPSWidgetHeight = Int32.Parse(Settings.Default.Properties["DTPSWidgetHeight"].DefaultValue.ToString());
            Settings.Default.DTPSWidgetWidth = Int32.Parse(Settings.Default.Properties["DTPSWidgetWidth"].DefaultValue.ToString());
            Settings.Default.DTPSVisibility = Settings.Default.Properties["DTPSVisibility"].DefaultValue.ToString();
            Settings.Default.DTPSWidgetSortDirection = Settings.Default.Properties["DTPSWidgetSortDirection"].DefaultValue.ToString();
            Settings.Default.DTPSWidgetSortProperty = Settings.Default.Properties["DTPSWidgetSortProperty"].DefaultValue.ToString();
        }

        public void OpenDTPSWidget()
        {
            Settings.Default.ShowDTPSWidgetOnLoad = true;
            Widgets.Instance.ShowDTPSWidget();
        }

        public void ResetHPSWidget()
        {
            Settings.Default.HPSWidgetUIScale = Settings.Default.Properties["HPSWidgetUIScale"].DefaultValue.ToString();
            Settings.Default.HPSWidgetTop = Int32.Parse(Settings.Default.Properties["HPSWidgetTop"].DefaultValue.ToString());
            Settings.Default.HPSWidgetLeft = Int32.Parse(Settings.Default.Properties["HPSWidgetLeft"].DefaultValue.ToString());
            Settings.Default.HPSWidgetHeight = Int32.Parse(Settings.Default.Properties["HPSWidgetHeight"].DefaultValue.ToString());
            Settings.Default.HPSWidgetWidth = Int32.Parse(Settings.Default.Properties["HPSWidgetWidth"].DefaultValue.ToString());
            Settings.Default.HPSVisibility = Settings.Default.Properties["HPSVisibility"].DefaultValue.ToString();
            Settings.Default.HPSWidgetSortDirection = Settings.Default.Properties["HPSWidgetSortDirection"].DefaultValue.ToString();
            Settings.Default.HPSWidgetSortProperty = Settings.Default.Properties["HPSWidgetSortProperty"].DefaultValue.ToString();
        }

        public void OpenHPSWidget()
        {
            Settings.Default.ShowHPSWidgetOnLoad = true;
            Widgets.Instance.ShowHPSWidget();
        }

        public void ResetEnmityWidget()
        {
            Settings.Default.EnmityWidgetUIScale = Settings.Default.Properties["EnmityWidgetUIScale"].DefaultValue.ToString();
            Settings.Default.EnmityWidgetTop = Int32.Parse(Settings.Default.Properties["EnmityWidgetTop"].DefaultValue.ToString());
            Settings.Default.EnmityWidgetLeft = Int32.Parse(Settings.Default.Properties["EnmityWidgetLeft"].DefaultValue.ToString());
            Settings.Default.EnmityWidgetHeight = Int32.Parse(Settings.Default.Properties["EnmityWidgetHeight"].DefaultValue.ToString());
            Settings.Default.EnmityWidgetWidth = Int32.Parse(Settings.Default.Properties["EnmityWidgetWidth"].DefaultValue.ToString());
        }

        public void OpenEnmityWidget()
        {
            Settings.Default.ShowEnmityWidgetOnLoad = true;
            Widgets.Instance.ShowEnmityWidget();
        }

        public void ResetFocusTargetWidget()
        {
            Settings.Default.FocusTargetWidgetUIScale = Settings.Default.Properties["FocusTargetWidgetUIScale"].DefaultValue.ToString();
            Settings.Default.FocusTargetWidgetTop = Int32.Parse(Settings.Default.Properties["FocusTargetWidgetTop"].DefaultValue.ToString());
            Settings.Default.FocusTargetWidgetLeft = Int32.Parse(Settings.Default.Properties["FocusTargetWidgetLeft"].DefaultValue.ToString());
            Settings.Default.FocusTargetWidgetHeight = Int32.Parse(Settings.Default.Properties["FocusTargetWidgetHeight"].DefaultValue.ToString());
            Settings.Default.FocusTargetWidgetWidth = Int32.Parse(Settings.Default.Properties["FocusTargetWidgetWidth"].DefaultValue.ToString());
        }

        public void OpenFocusTargetWidget()
        {
            Settings.Default.ShowFocusTargetWidgetOnLoad = true;
            Widgets.Instance.ShowFocusTargetWidget();
        }

        public void ResetCurrentTargetWidget()
        {
            Settings.Default.CurrentTargetWidgetUIScale = Settings.Default.Properties["CurrentTargetWidgetUIScale"].DefaultValue.ToString();
            Settings.Default.CurrentTargetWidgetTop = Int32.Parse(Settings.Default.Properties["CurrentTargetWidgetTop"].DefaultValue.ToString());
            Settings.Default.CurrentTargetWidgetLeft = Int32.Parse(Settings.Default.Properties["CurrentTargetWidgetLeft"].DefaultValue.ToString());
            Settings.Default.CurrentTargetWidgetHeight = Int32.Parse(Settings.Default.Properties["CurrentTargetWidgetHeight"].DefaultValue.ToString());
            Settings.Default.CurrentTargetWidgetWidth = Int32.Parse(Settings.Default.Properties["CurrentTargetWidgetWidth"].DefaultValue.ToString());
        }

        public void OpenCurrentTargetWidget()
        {
            Settings.Default.ShowCurrentTargetWidgetOnLoad = true;
            Widgets.Instance.ShowCurrentTargetWidget();
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        #endregion
    }
}
