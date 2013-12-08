﻿// FFXIVAPP.Plugin.Widgets
// AboutViewModel.cs
// 
// © 2013 ZAM Network LLC

#region Usings

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Core.Memory;
using FFXIVAPP.Common.Models;
using FFXIVAPP.Common.ViewModelBase;

#endregion

namespace FFXIVAPP.Plugin.Widgets.ViewModels
{
    internal sealed class AboutViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static AboutViewModel _instance;

        public static AboutViewModel Instance
        {
            get { return _instance ?? (_instance = new AboutViewModel()); }
        }

        #endregion

        #region Declarations

        public DelegateCommand OpenWebSiteCommand { get; private set; }

        #endregion

        public AboutViewModel()
        {
            OpenWebSiteCommand = new DelegateCommand(OpenWebSite);
        }

        #region Loading Functions

        #endregion

        #region Utility Functions

        #endregion

        #region Command Bindings

        public void OpenWebSite()
        {
            try
            {
                Process.Start("https://github.com/icehunter/ffxivapp-plugin-widgets");
            }
            catch (Exception ex)
            {
                var popupContent = new PopupContent();
                popupContent.Title = PluginViewModel.Instance.Locale["app_WarningMessage"];
                popupContent.Message = ex.Message;
                Plugin.PHost.PopupMessage(Plugin.PName, popupContent);
            }
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
