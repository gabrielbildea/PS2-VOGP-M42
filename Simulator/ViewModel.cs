using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Simulator
{
    public enum ButtonState
    { 
        Off,                //oprit (oprire de urgenta)
        Station_4,          //etaj 4
        Station_2,          //etaj 3
        Station_3,          //etaj 2
        Station_1,          //etaj 1
        AbDown,             //apasare continua pt revenirea liftului in pozitia initiala
    }

    public enum ProcessState
    {
        Off,                //oprit
        Ascend_0_1,          //urca
        Ascend_0_2,
        Ascend_0_3,
        Ascend_0_4,
        Descend,         //coboara - revenire la starea initiala
    }
    
    class ViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        private BackgroundWorker _worker = new BackgroundWorker();
        private System.Timers.Timer _timer = new System.Timers.Timer();
        private Comm.Sender _sender;
        private ProcessState _theStateOfTheProcess = ProcessState.Off;
        private bool _isEventRaised = false;
        private ProcessState _nextState;
        private bool _isEmergencyBtnPressed = false;
        private bool _isAtStation_0;
        private bool _isAtStation_1;
        private bool _isAtStation_2;
        private bool _isAtStation_3;
        private bool _isAtStation_4;
        public ViewModel()
        {
        }

        void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Init()
        {
            _sender = new Comm.Sender("127.0.0.1", 3000);
            _timer.Elapsed += _timer_Elapsed;
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerAsync();
        }

        
        public ProcessState TheStateOfTheProcess
        {
            get => _theStateOfTheProcess;
            set
            {
                _theStateOfTheProcess = value;
                _sender.Send(Convert.ToByte(_theStateOfTheProcess));
            }
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _isEventRaised = false;
            TheStateOfTheProcess = _nextState;
            _timer.Stop();
        }

        
        private void RaiseTimerEvent(ProcessState NextProcessState, int TimeInterval)
        {
            if(!_isEventRaised)
            {
                _isEventRaised = true;
                _nextState = NextProcessState;
                _timer.Interval = TimeInterval;
                _timer.Start();
            }
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(true)
            {              
                ProcessNextState(TheStateOfTheProcess);
                System.Threading.Thread.Sleep(100);
            }
        }

        public void ProcessNextState(ProcessState CurrentState)
        {
            switch (CurrentState)
            {
                case ProcessState.Off:
                    IsAtStation_0 = true;
                    IsAtStation_1 = false;
                    IsAtStation_2 = false;
                    IsAtStation_3 = false;
                    IsAtStation_4 = false;

                    RaiseTimerEvent(ProcessState.Off, 2000);
                   
                    break;
                case ProcessState.Ascend_0_1:
                    IsAtStation_0 = true;
                    IsAtStation_1 = false;
                    IsAtStation_2 = false;
                    IsAtStation_3 = false;
                    IsAtStation_4 = false;

                    RaiseTimerEvent(ProcessState.Ascend_0_1, 1000);

                    IsAtStation_0 = false;
                    IsAtStation_1 = true;
                    break;
                case ProcessState.Ascend_0_2:
                    IsAtStation_0 = false;
                    IsAtStation_1 = false;
                    IsAtStation_2 = true;
                    IsAtStation_3 = false;
                    IsAtStation_4 = false;

                    RaiseTimerEvent(ProcessState.Ascend_0_2, 2000);

                    break;
                case ProcessState.Ascend_0_3:
                    IsAtStation_0 = false;
                    IsAtStation_1 = false;
                    IsAtStation_2 = false;
                    IsAtStation_3 = true;
                    IsAtStation_4 = false;

                    RaiseTimerEvent(ProcessState.Ascend_0_3, 3000);

                    break;
                case ProcessState.Ascend_0_4:
                    IsAtStation_0 = true;
                    IsAtStation_1 = false;
                    IsAtStation_2 = false;
                    IsAtStation_3 = false;
                    IsAtStation_4 = false;

                    RaiseTimerEvent(ProcessState.Ascend_0_4, 4000);

                    IsAtStation_0 = false;
                    IsAtStation_4 = true;

                    break;
                case ProcessState.Descend:
                    IsAtStation_0 = true;
                    IsAtStation_1 = false;
                    IsAtStation_2 = false;
                    IsAtStation_3 = false;
                    IsAtStation_4 = false;

                    RaiseTimerEvent(ProcessState.Descend, 5000);
                   
                    break;                                           
            }

        }

        public void ForceNextState(ProcessState NextState)
        {
            _isEventRaised = false;
            _timer.Stop();
            TheStateOfTheProcess = NextState;
            
        }


        public bool IsAtStation_0
        {
            get
            {
                return _isAtStation_0;
            }
            set
            {
                _isAtStation_0 = value;
                OnPropertyChanged(nameof(IsAtStation_0_Visible));
            }
        }

        public System.Windows.Visibility IsAtStation_0_Visible
        {
            get
            {
                if (_isAtStation_0)
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }


        public bool IsAtStation_1
        {
            get
            {
                return _isAtStation_1;
            }
            set
            {
                _isAtStation_1 = value;
                OnPropertyChanged(nameof(IsAtStation_1_Visible));
            }
        }

        public System.Windows.Visibility IsAtStation_1_Visible
        {
            get
            {
                if (_isAtStation_1)
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }

        public bool IsAtStation_2
        {
            get
            {
                return _isAtStation_2;
            }
            set
            {
                _isAtStation_2 = value;
                OnPropertyChanged(nameof(IsAtStation_2_Visible));
            }
        }

        public System.Windows.Visibility IsAtStation_2_Visible
        {
            get
            {
                if (_isAtStation_2)
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }

        public bool IsAtStation_3
        {
            get
            {
                return _isAtStation_3;
            }
            set
            {
                _isAtStation_3 = value;
                OnPropertyChanged(nameof(IsAtStation_3_Visible));
            }
        }

        public System.Windows.Visibility IsAtStation_3_Visible
        {
            get
            {
                if (_isAtStation_3)
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }

        public bool IsAtStation_4
        {
            get
            {
                return _isAtStation_4;
            }
            set
            {
                _isAtStation_4 = value;
                OnPropertyChanged(nameof(IsAtStation_4_Visible));
            }
        }

        public System.Windows.Visibility IsAtStation_4_Visible
        {
            get
            {
                if (_isAtStation_4)
                {
                    return System.Windows.Visibility.Visible;
                }
                else
                {
                    return System.Windows.Visibility.Hidden;
                }
            }
        }



    }
}
