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
    /// <summary>
    /// Interaction logic for MyControl.xaml
    /// </summary>
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
        }

        private void Button_Click_S0(object sender, RoutedEventArgs e)
        {
            _vm.ForceNextState(ProcessState.Off);
        }

        private void Button_Click_S1(object sender, RoutedEventArgs e)
        {
            _vm.ProcessNextState(ProcessState.Ascend_0_4);
        }

        private void Button_Click_S2(object sender, RoutedEventArgs e)
        {
            _vm.ProcessNextState(ProcessState.Ascend_0_3);
        }

        private void Button_Click_S3(object sender, RoutedEventArgs e)
        {
            _vm.ProcessNextState(ProcessState.Ascend_0_2);
        }

        private void Button_Click_S4(object sender, RoutedEventArgs e)
        {
            _vm.ProcessNextState(ProcessState.Ascend_0_1);
        }
        private void Button_Click_S5(object sender, RoutedEventArgs e)
        {
            _vm.ProcessNextState(ProcessState.Descend);
        }




    }
}
