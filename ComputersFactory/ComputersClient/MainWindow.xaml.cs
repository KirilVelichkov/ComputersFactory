using ComputersClient.Windows;
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

namespace ComputersClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var addVideoCardWindow = new addVideoCard();
            addVideoCardWindow.Show();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var addMemoryWindow = new addMemory();
            addMemoryWindow.Show();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var addProcesorWindow = new addProcesor();
            addProcesorWindow.Show();
        }
    }
}
