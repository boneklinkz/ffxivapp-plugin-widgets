// FFXIVAPP.Plugin.Widgets
// DPSWidgetViewModel.cs
// 
// © 2013 ZAM Network LLC

#region Usings

using System.ComponentModel;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Core.Memory;
using FFXIVAPP.Common.Core.Parse;

#endregion

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    internal sealed class EnmityWidgetViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static EnmityWidgetViewModel _instance;
        private TargetEntity _targetEntity;
        private bool _currentTargetIsValid;
        private double _currentTargetHPPercent;

        public static EnmityWidgetViewModel Instance
        {
            get { return _instance ?? (_instance = new EnmityWidgetViewModel()); }
        }

        public TargetEntity TargetEntity
        {
            get { return _targetEntity ?? (_targetEntity = new TargetEntity()); }
            set
            {
                _targetEntity = value;
                RaisePropertyChanged();
            }
        }

        public bool CurrentTargetIsValid
        {
            get { return _currentTargetIsValid; }
            set
            {
                _currentTargetIsValid = value;
                RaisePropertyChanged();
            }
        }

        public double CurrentTargetHPPercent
        {
            get { return _currentTargetHPPercent; }
            set
            {
                _currentTargetHPPercent = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Declarations

        #endregion

        public EnmityWidgetViewModel()
        {
        }

        #region Loading Functions

        #endregion

        #region Utility Functions

        #endregion

        #region Command Bindings

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
