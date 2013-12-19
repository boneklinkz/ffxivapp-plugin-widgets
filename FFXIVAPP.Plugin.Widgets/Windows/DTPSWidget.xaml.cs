// FFXIVAPP.Plugin.Widgets
// DTPSWidget.xaml.cs
// 
// © 2013 ZAM Network LLC

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FFXIVAPP.Plugin.Widgets.Interop;

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    /// <summary>
    ///     Interaction logic for DTPSWidget.xaml
    /// </summary>
    public partial class DTPSWidget
    {
        public static DTPSWidget View;

        public DTPSWidget()
        {
            View = this;
            InitializeComponent();
            View.SourceInitialized += delegate { WinAPI.ToggleClickThrough(this); };
        }

        private void TitleBar_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void WidgetClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Widget_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
