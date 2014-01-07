// FFXIVAPP.Plugin.Widgets
// Widgets.cs
// 
// Copyright © 2013 ZAM Network LLC
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the 
// following conditions are met: 
// 
//  * Redistributions of source code must retain the above copyright notice, this list of conditions and the following 
//    disclaimer. 
//  * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the 
//    following disclaimer in the documentation and/or other materials provided with the distribution. 
//  * Neither the name of ZAM Network LLC nor the names of its contributors may be used to endorse or promote products 
//    derived from this software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
// SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE 
// USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 

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
