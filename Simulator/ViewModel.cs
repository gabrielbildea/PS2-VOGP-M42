﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Server;

namespace Simulator
{
    public enum ProcessState
    {
        Station_0=1,
        Station_1=2,
        Station_2=4,
        Station_3=8,
        Station_4=16,
    }

    class ViewModel : INotifyPropertyChanged
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        private BackgroundWorker _worker = new BackgroundWorker();
        private System.Timers.Timer _timer = new System.Timers.Timer();
       // private Comm.Sender _sender;
        private Server.Server _server;
        private ProcessState _theStateOfTheProcess = ProcessState.Station_0;
        private ProcessState _nextState = ProcessState.Station_0;
        private ProcessState _nextState2;
        private bool _isEventRaised = false;

        private bool _isAtStation_0, _isAtStation_1, _isAtStation_2, _isAtStation_3, _isAtStation_4;

        public ViewModel() { }

        void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Init()
        {
            IPHostEntry iphostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAdress = iphostInfo.AddressList[0];
           // _sender = new Comm.Sender("127.0.0.1", 3000);
            _server = new Server.Server(ipAdress);
            _timer.Elapsed += _timer_Elapsed;
            _worker.DoWork += _worker_DoWork;
            _worker.RunWorkerAsync();
            _nextState2 = ProcessState.Station_0;
        }

        private void _worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                ProcessNextState(TheStateOfTheProcess);
                System.Threading.Thread.Sleep(100);
            }
        }

        public ProcessState TheStateOfTheProcess
        {
            get => _theStateOfTheProcess;
            set
            {
                _theStateOfTheProcess = value;
               // _sender.Send(Convert.ToByte(_theStateOfTheProcess));
                _server.sendData(Convert.ToByte(_theStateOfTheProcess));
            }
        }

        public ProcessState TheNextStateOfTheProcess
        {
            get => _nextState2;
            set
            {
                _nextState2 = value;
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
        
        public void ProcessNextState(ProcessState CurrentState)
        {
            
            ProcessState myNextState = TheNextStateOfTheProcess;
            //int dif = (int)CurrentState - (int)myNextState;
            //int timeInterval = Math.Abs(dif)  * 1000; //liftului ii va lua 1s pt a se deplasa intre etaje cu diferenta 1 (2s pt diferenta 2 etc)
            switch (CurrentState)
            {
                case ProcessState.Station_0:
                    IsAtStation_0 = true;
                    IsAtStation_1 = false;
                    IsAtStation_2 = false;
                    IsAtStation_3 = false;
                    IsAtStation_4 = false;
                    RaiseTimerEvent(myNextState, 2000);
                    IsAtStation_0 = false;
                    break;

                case ProcessState.Station_1:
                    IsAtStation_0 = false;
                    IsAtStation_1 = true;
                    IsAtStation_2 = false;
                    IsAtStation_3 = false;
                    IsAtStation_4 = false;
                    RaiseTimerEvent(myNextState, 2000);
                    IsAtStation_1 = false;
                    break;

                case ProcessState.Station_2:
                    IsAtStation_0 = false;
                    IsAtStation_1 = false;
                    IsAtStation_2 = true;
                    IsAtStation_3 = false;
                    IsAtStation_4 = false;
                    RaiseTimerEvent(myNextState, 2000);
                    IsAtStation_2 = false;
                    break;

                case ProcessState.Station_3:
                    IsAtStation_0 = false;
                    IsAtStation_1 = false;
                    IsAtStation_2 = false;
                    IsAtStation_3 = true;
                    IsAtStation_4 = false;
                    RaiseTimerEvent(myNextState, 2000);
                    IsAtStation_3 = false;
                    break;

                case ProcessState.Station_4:
                    IsAtStation_0 = false;
                    IsAtStation_1 = false;
                    IsAtStation_2 = false;
                    IsAtStation_3 = false;
                    IsAtStation_4 = true;
                    RaiseTimerEvent(myNextState, 2000);
                    IsAtStation_4 = false;
                    break;                            
            }

            if (_nextState == ProcessState.Station_0) IsAtStation_0 = true;
            if (_nextState == ProcessState.Station_1) IsAtStation_1 = true;
            if (_nextState == ProcessState.Station_2) IsAtStation_2 = true;
            if (_nextState == ProcessState.Station_3) IsAtStation_3 = true;
            if (_nextState == ProcessState.Station_4) IsAtStation_4 = true;
        }

        public void ForceNextState(ProcessState NextState)
        {
            _isEventRaised = false;
            _timer.Stop();
            //int timeInterval = (Math.Abs((int)TheStateOfTheProcess - (int)NextState)) * 1000;
            //RaiseTimerEvent(NextState, timeInterval);
            TheStateOfTheProcess = NextState;
            IsAtStation_0 = true;
            IsAtStation_1 = false;
            IsAtStation_2 = false;
            IsAtStation_3 = false;
            IsAtStation_4 = false;
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
