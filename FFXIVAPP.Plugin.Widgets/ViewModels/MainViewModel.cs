// FFXIVAPP.Plugin.Widgets
// MainViewModel.cs
// 
// © 2013 ZAM Network LLC

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
            Settings.Default.DPSWidgetTop = 100;
            Settings.Default.DPSWidgetLeft = 100;
        }

        public void OpenDPSWidget()
        {
            Settings.Default.ShowDPSWidgetOnLoad = true;
            Widgets.Instance.ShowDPSWidget();
        }

        public void ResetDTPSWidget()
        {
            Settings.Default.DTPSWidgetTop = 200;
            Settings.Default.DTPSWidgetLeft = 100;
        }

        public void OpenDTPSWidget()
        {
            Settings.Default.ShowDTPSWidgetOnLoad = true;
            Widgets.Instance.ShowDTPSWidget();
        }

        public void ResetHPSWidget()
        {
            Settings.Default.HPSWidgetTop = 300;
            Settings.Default.HPSWidgetLeft = 100;
        }

        public void OpenHPSWidget()
        {
            Settings.Default.ShowHPSWidgetOnLoad = true;
            Widgets.Instance.ShowHPSWidget();
        }

        public void ResetEnmityWidget()
        {
            Settings.Default.EnmityWidgetTop = 100;
            Settings.Default.EnmityWidgetLeft = 400;
        }

        public void OpenEnmityWidget()
        {
            Settings.Default.ShowEnmityWidgetOnLoad = true;
            Widgets.Instance.ShowEnmityWidget();
        }

        public void ResetFocusTargetWidget()
        {
            Settings.Default.FocusTargetWidgetTop = 200;
            Settings.Default.FocusTargetWidgetLeft = 400;
        }

        public void OpenFocusTargetWidget()
        {
            Settings.Default.ShowFocusTargetWidgetOnLoad = true;
            Widgets.Instance.ShowFocusTargetWidget();
        }

        public void ResetCurrentTargetWidget()
        {
            Settings.Default.CurrentTargetWidgetTop = 300;
            Settings.Default.CurrentTargetWidgetLeft = 400;
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
