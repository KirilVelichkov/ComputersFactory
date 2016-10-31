using ComputersClient.Windows;
using ComputersFactory.Logic;
using ComputersFactory.Models;
using ComputersFactory.Models.Migrations;
using System.Windows;
using System.Data.Entity;
using System.ComponentModel;
using System.Threading.Tasks;
using ComputersFactory.Logic.Reports;

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
        private void button_Click(object sender, RoutedEventArgs e)
        {
            var mongo = new MongoDBHanlder("ScrewdriverDB");
            mongo.TransferToMSSQL().Wait();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ExcelHandler.TransfertAllData();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var context = new ComputersFactoryContext();

            var pdf = new PdfExporter(context);
            pdf.CreatePdf("../../../Pdf-Reports");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            var context = new ComputersFactoryContext();
            var path = @"..\..\..\";
            var xmlReporter = new XmlExporter();
            xmlReporter.CreateXmlReport(context, path);
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            var context = new ComputersFactoryContext();
            var mySql = new MySQLHandler(context);

            mySql.LoadReportsInMySql();

            var exporter = new JsonExporter(context);
            exporter.CreateJsonReports("../../../Json-Reports");
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            string path = "../../../../SQLiteDB/ComputersFactory.db";
            var liteInfo = SQLiteHandler.ReadTable(path, "Computers");
        }
    }
}
