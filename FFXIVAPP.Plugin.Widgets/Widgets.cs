// FFXIVAPP.Plugin.Widgets
// Widgets.cs
// 
// © 2013 ZAM Network LLC

using System;
using FFXIVAPP.Plugin.Widgets.Windows;

namespace FFXIVAPP.Plugin.Widgets
{
    public class Widgets
    {
        private static Widgets _instance;
        private DPSWidget _dpsWidget;
        private EnmityWidget _enmityWidget;

        public static Widgets Instance
        {
            get { return _instance ?? (_instance = new Widgets()); }
            set { _instance = value; }
        }

        public DPSWidget DPSWidget
        {
            get { return _dpsWidget ?? (_dpsWidget = new DPSWidget()); }
        }

        public EnmityWidget EnmityWidget
        {
            get { return _enmityWidget ?? (_enmityWidget = new EnmityWidget()); }
        }

        public void ShowDPSWidget()
        {
            try
            {
                DPSWidget.Show();
            }
            catch (Exception ex)
            {
            }
        }

        public void ShowEnmityWidget()
        {
            try
            {
                EnmityWidget.Show();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
