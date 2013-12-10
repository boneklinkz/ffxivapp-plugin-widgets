// FFXIVAPP.Plugin.Widgets
// DPSWidgetViewModel.cs
// 
// © 2013 ZAM Network LLC

using System.ComponentModel;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Core.Parse;

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    internal sealed class DPSWidgetViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static DPSWidgetViewModel _instance;
        private ParseEntity _parseEntity;

        public static DPSWidgetViewModel Instance
        {
            get { return _instance ?? (_instance = new DPSWidgetViewModel()); }
        }

        public ParseEntity ParseEntity
        {
            get { return _parseEntity ?? (_parseEntity = new ParseEntity()); }
            set
            {
                _parseEntity = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Declarations

        #endregion

        public DPSWidgetViewModel()
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
