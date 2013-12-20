// FFXIVAPP.Plugin.Widgets
// EnmityWidget.xaml.cs
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
    ///     Interaction logic for EnmityWidget.xaml
    /// </summary>
    public partial class EnmityWidget
    {
        public static EnmityWidget View;

        public EnmityWidget()
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
            Settings.Default.ShowEnmityWidgetOnLoad = false;
            Close();
        }

        private void Widget_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
