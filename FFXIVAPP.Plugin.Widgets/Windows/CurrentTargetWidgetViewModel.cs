// FFXIVAPP.Plugin.Widgets
// CurrentTargetWidgetViewModel.cs
// 
// © 2013 ZAM Network LLC

using System.ComponentModel;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Core.Memory;

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    internal sealed class CurrentTargetWidgetViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static CurrentTargetWidgetViewModel _instance;
        private double _currentTargetHPPercent;
        private bool _currentTargetIsValid;
        private TargetEntity _targetEntity;

        public static CurrentTargetWidgetViewModel Instance
        {
            get { return _instance ?? (_instance = new CurrentTargetWidgetViewModel()); }
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

        public CurrentTargetWidgetViewModel()
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
