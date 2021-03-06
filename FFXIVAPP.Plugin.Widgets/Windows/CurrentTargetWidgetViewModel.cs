﻿// FFXIVAPP.Plugin.Widgets
// CurrentTargetWidgetViewModel.cs
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

using System.ComponentModel;
using System.Runtime.CompilerServices;
using FFXIVAPP.Common.Core.Memory;

namespace FFXIVAPP.Plugin.Widgets.Windows
{
    internal sealed class CurrentTargetWidgetViewModel : INotifyPropertyChanged
    {
        #region Property Bindings

        private static CurrentTargetWidgetViewModel _instance;
        private float _currentTargetDistance;
        private double _currentTargetHPPercent;
        private bool _currentTargetIsValid;
        private TargetEntity _targetEntity;

        public static CurrentTargetWidgetViewModel Instance
        {
            get { return _instance ?? (_instance = new CurrentTargetWidgetViewModel()); }
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

        public bool CurrentTargetIsValid
        {
            get { return _currentTargetIsValid; }
            set
            {
                _currentTargetIsValid = value;
                RaisePropertyChanged();
            }
        }

        public double CurrentTargetHPPercent
        {
            get { return _currentTargetHPPercent; }
            set
            {
                _currentTargetHPPercent = value;
                RaisePropertyChanged();
            }
        }

        public float CurrentTargetDistance
        {
            get { return _currentTargetDistance; }
            set
            {
                _currentTargetDistance = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region Declarations

        #endregion

        public CurrentTargetWidgetViewModel()
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
