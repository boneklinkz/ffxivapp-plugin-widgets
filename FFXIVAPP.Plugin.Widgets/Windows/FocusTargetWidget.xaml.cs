// FFXIVAPP.Plugin.Widgets
// FocusTargetWidget.xaml.cs
// 
// © 2013 ZAM Network LLC

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using FFXIVAPP.Plugin.Widgets.Interop;
using FFXIVAPP.Plugin.Widgets.Properties;

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    /// <summary>
    ///     Interaction logic for FocusTargetWidget.xaml
    /// </summary>
    public partial class FocusTargetWidget
    {
        public static FocusTargetWidget View;

        public FocusTargetWidget()
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
            Settings.Default.ShowFocusTargetWidgetOnLoad = false;
            Close();
        }

        private void Widget_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
