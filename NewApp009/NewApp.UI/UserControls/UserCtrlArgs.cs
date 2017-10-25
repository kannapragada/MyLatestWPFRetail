namespace NewApp.UI.UserControls
{
    public class UserCtrlArgs
    {
        private string _targetCtrlName;
        private string _targetCtrlMode;
        private string _invokingCtrlName;
        private string _invokingCtrlMode;
        private string _previnvokingCtrlName;
        private string _previnvokingCtrlMode;
        private string _prevtargetCtrlName;
        private string _prevtargetCtrlMode;
        private string _CtrlTabName;

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

        public string PrevTargetControlName
        {
            get
            {
                return _prevtargetCtrlName;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _prevtargetCtrlName = value;
            }
        }

        public string PrevTargetControlMode
        {
            get
            {
                return _prevtargetCtrlMode;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _prevtargetCtrlMode = value;
            }
        }

        public string PrevInvokingControlName
        {
            get
            {
                return _previnvokingCtrlName;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _previnvokingCtrlName = value;
            }
        }

        public string PrevInvokingControlMode
        {
            get
            {
                return _previnvokingCtrlMode;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _previnvokingCtrlMode = value;
            }
        }

        public string ControlTabName
        {
            get
            {
                return _CtrlTabName;
            }
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                    _CtrlTabName = value;
            }
        }
    }
}
