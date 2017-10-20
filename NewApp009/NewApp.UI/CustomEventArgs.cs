using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace NewApp
{
    public class CustomEventArgs : RoutedEventArgs
    {
        private string _targetCtrlName;
        private string _targetCtrlMode;
        private string _invokingCtrlName;
        private string _invokingCtrlMode;

        public string TargetControlName
        {
            get
            {
                return _targetCtrlName;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _targetCtrlName = value;
            }
        }

        public string TargetControlMode
        {
            get
            {
                return _targetCtrlMode;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _targetCtrlMode = value;
            }
        }

        public string InvokingControlName
        {
            get
            {
                return _invokingCtrlName;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _invokingCtrlName = value;
            }
        }

        public string InvokingControlMode
        {
            get
            {
                return _invokingCtrlMode;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _invokingCtrlMode = value;
            }
        }

        public CustomEventArgs(RoutedEvent routedEvent, string InvokingControlName, string InvokingControlMode, string TargetControlName, string TargetControlMode)
            : base(routedEvent)
        {
            this.InvokingControlName = InvokingControlName;
            this.InvokingControlMode = InvokingControlMode;
            this.TargetControlName = TargetControlName;
            this.TargetControlMode = TargetControlMode;
        }
    }
}
