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
        private CurrentTargetWidget _currentTargetWidget;
        private DPSWidget _dpsWidget;
        private DTPSWidget _dtpsWidget;
        private EnmityWidget _enmityWidget;
        private FocusTargetWidget _focusTargetWidget;
        private HPSWidget _hpsWidget;

        public static Widgets Instance
        {
            get { return _instance ?? (_instance = new Widgets()); }
            set { _instance = value; }
        }

        public DPSWidget DPSWidget
        {
            get { return _dpsWidget ?? (_dpsWidget = new DPSWidget()); }
        }

        public DTPSWidget DTPSWidget
        {
            get { return _dtpsWidget ?? (_dtpsWidget = new DTPSWidget()); }
        }

        public EnmityWidget EnmityWidget
        {
            get { return _enmityWidget ?? (_enmityWidget = new EnmityWidget()); }
        }

        public FocusTargetWidget FocusTargetWidget
        {
            get { return _focusTargetWidget ?? (_focusTargetWidget = new FocusTargetWidget()); }
        }

        public CurrentTargetWidget CurrentTargetWidget
        {
            get { return _currentTargetWidget ?? (_currentTargetWidget = new CurrentTargetWidget()); }
        }

        public HPSWidget HPSWidget
        {
            get { return _hpsWidget ?? (_hpsWidget = new HPSWidget()); }
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

        public void ShowDTPSWidget()
        {
            try
            {
                DTPSWidget.Show();
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

        public void ShowFocusTargetWidget()
        {
            try
            {
                FocusTargetWidget.Show();
            }
            catch (Exception ex)
            {
            }
        }

        public void ShowCurrentTargetWidget()
        {
            try
            {
                CurrentTargetWidget.Show();
            }
            catch (Exception ex)
            {
            }
        }

        public void ShowHPSWidget()
        {
            try
            {
                HPSWidget.Show();
            }
            catch (Exception ex)
            {
            }
        }
    }
}
