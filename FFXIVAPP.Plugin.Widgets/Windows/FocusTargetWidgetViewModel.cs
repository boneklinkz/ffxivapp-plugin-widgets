// FFXIVAPP.Plugin.Widgets
// FocusTargetWidgetViewModel.cs
// 
// © 2013 ZAM Network LLC

using System.ComponentModel;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Core.Memory;

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    internal sealed class FocusTargetWidgetViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static FocusTargetWidgetViewModel _instance;
        private double _focusTargetHPPercent;
        private bool _focusTargetIsValid;
        private TargetEntity _targetEntity;

        public static FocusTargetWidgetViewModel Instance
        {
            get { return _instance ?? (_instance = new FocusTargetWidgetViewModel()); }
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

        public bool FocusTargetIsValid
        {
            get { return _focusTargetIsValid; }
            set
            {
                _focusTargetIsValid = value;
                RaisePropertyChanged();
            }
        }

        public double FocusTargetHPPercent
        {
            get { return _focusTargetHPPercent; }
            set
            {
                _focusTargetHPPercent = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Declarations

        #endregion

        public FocusTargetWidgetViewModel()
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
