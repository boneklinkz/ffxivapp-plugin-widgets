// FFXIVAPP.Plugin.Widgets
// DTPSWidgetViewModel.cs
// 
// © 2013 ZAM Network LLC

using System.ComponentModel;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Core.Parse;

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    internal sealed class DTPSWidgetViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static DTPSWidgetViewModel _instance;
        private ParseEntity _parseEntity;

        public static DTPSWidgetViewModel Instance
        {
            get { return _instance ?? (_instance = new DTPSWidgetViewModel()); }
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

        public DTPSWidgetViewModel()
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
