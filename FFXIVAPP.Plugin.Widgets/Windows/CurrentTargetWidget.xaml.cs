// FFXIVAPP.Plugin.Widgets
// CurrentTargetWidget.xaml.cs
// 
// © 2013 ZAM Network LLC

using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    /// <summary>
    ///     Interaction logic for CurrentTargetWidget.xaml
    /// </summary>
    public partial class CurrentTargetWidget
    {
        public static CurrentTargetWidget View;

        public CurrentTargetWidget()
        {
            View = this;
            InitializeComponent();
        }

        private void UIElement_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
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

        private void CurrentTargetWidget_OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
