using ComputersClient.Windows;
using ComputersFactory.Logic;
using ComputersFactory.Models;
using ComputersFactory.Models.Migrations;
using System.Windows;
using System.Data.Entity;
using System.ComponentModel;
using System.Threading.Tasks;

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

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var browseZipWindow = new SelectFile();
            browseZipWindow.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult creatingDB = MessageBox.Show("Creating Database. Please wait.");

            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private readonly BackgroundWorker worker = new BackgroundWorker();

        private async void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());

            var context = new ComputersFactoryContext();
            await context.CreateDB();

        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBoxResult createDBmsg = MessageBox.Show("Database created successfully");
        }

        private void button4_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
