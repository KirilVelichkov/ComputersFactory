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

       // private void button2_Click(object sender, RoutedEventArgs e)
       // {
       //     var addVideoCardWindow = new addVideoCard();
       //     addVideoCardWindow.Show();
       // }
       //
       // private void button1_Click(object sender, RoutedEventArgs e)
       // {
       //     var addMemoryWindow = new addMemory();
       //     addMemoryWindow.Show();
       // }
       //
       // private void button_Click(object sender, RoutedEventArgs e)
       // {
       //     var addProcesorWindow = new addProcesor();
       //     addProcesorWindow.Show();
       // }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var browseZipWindow = new SelectFile();
            browseZipWindow.Show();
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private readonly BackgroundWorker worker = new BackgroundWorker();

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());

            var context = new ComputersFactoryContext();
            context.Database.CreateIfNotExists();

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
