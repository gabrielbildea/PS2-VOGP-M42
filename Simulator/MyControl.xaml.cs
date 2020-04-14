using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simulator
{

    public partial class MyControl : UserControl
    {
        ViewModel _vm;
        public MyControl()
        {
            InitializeComponent();
            _vm = new ViewModel();
            this.DataContext = _vm;
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            _vm.Init();
            //_vm.ForceNextState(ProcessState.Station_0);
        }

        private void Button_Click_S0(object sender, RoutedEventArgs e) //oprire de urgenta
        {
            _vm.ForceNextState(ProcessState.Station_0);
        }

        private void Button_Click_S1(object sender, RoutedEventArgs e)  //merg in station4
        {
             _vm.ProcessNextState(_vm.TheStateOfTheProcess, ProcessState.Station_4);
        }

        private void Button_Click_S2(object sender, RoutedEventArgs e) //merg in station3
        {
            _vm.ProcessNextState(_vm.TheStateOfTheProcess, ProcessState.Station_3);
        }

        private void Button_Click_S3(object sender, RoutedEventArgs e) //merg in station2
        {
            _vm.ProcessNextState(_vm.TheStateOfTheProcess, ProcessState.Station_2);
        }

        private void Button_Click_S4(object sender, RoutedEventArgs e) //merg in station1
        {
            _vm.ProcessNextState(_vm.TheStateOfTheProcess, ProcessState.Station_1);
        }
        private void Button_Click_S5(object sender, RoutedEventArgs e) //merg in station0
        {
            _vm.ProcessNextState(_vm.TheStateOfTheProcess, ProcessState.Station_0);
        }




    }
}
