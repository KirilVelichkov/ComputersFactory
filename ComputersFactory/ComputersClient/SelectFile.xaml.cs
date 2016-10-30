using ComputersFactory.Logic;
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
using System.Windows.Shapes;


namespace ComputersClient.Windows
{
    /// <summary>
    /// Interaction logic for SelectFile.xaml
    /// </summary>
    public partial class SelectFile : Window
    {
        string filePath;
        public SelectFile()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.DefaultExt = ".zip";
            dialog.Filter = "Zip Files (*.zip)|*.zip";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                filePath = dialog.FileName;
                label1.Content = "FileName: " + filePath;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ZipHanlder.ExtractExcelFiles(filePath);
        }
    }
}
