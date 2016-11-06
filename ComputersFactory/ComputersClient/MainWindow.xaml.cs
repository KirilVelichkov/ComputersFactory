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
        private ComputersFactoryContext dbContext;

        public MainWindow()
        {
            InitializeComponent();
            this.dbContext = new ComputersFactoryContext();
        }
        private void ExtractFromZip_button(object sender, RoutedEventArgs e)
        {
            var browseZipWindow = new SelectFile();
            browseZipWindow.Show();
        }

        private void CreateDatabase_button(object sender, RoutedEventArgs e)
        {
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private readonly BackgroundWorker worker = new BackgroundWorker();

        private async void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ComputersFactoryContext, Configuration>());
            
            await this.dbContext.CreateDB();

        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBoxResult createDBmsg = MessageBox.Show("Database created successfully");
        }
        private async void LoadFromMongoDB_button(object sender, RoutedEventArgs e)
        {
            var mongo = new MongoDBHanlder("ScrewdriverDB");
            await mongo.TransferToMSSQL();
        }

        private void LoadFromExcel_button(object sender, RoutedEventArgs e)
        {
            ExcelHandler.TransferAllData();
        }

        private void CreatePDFReport_button(object sender, RoutedEventArgs e)
        {
            var pdf = new PdfExporter(this.dbContext);
            pdf.GenerateReport("../../../Pdf-Reports");
        }

        private void CreateXMLReport_button(object sender, RoutedEventArgs e)
        {
            var path = @"../../../Xml-Report";
            var xmlReporter = new XmlExporter(this.dbContext);
            xmlReporter.GenerateReport(path);
        }

        private void CreateJSON_and_LoadDataToMSSQL(object sender, RoutedEventArgs e)
        {
            var mySql = new MySQLHandler(this.dbContext);
            var dataGenerator = new DataReportGenerator();
            mySql.LoadReportsInMySql(dataGenerator);

            var exporter = new JsonExporter(this.dbContext);
            exporter.GenerateReport("../../../Json-Reports");
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            string path = "../../../../SQLiteDB/ComputersFactory.db";
            var liteInfo = SQLiteHandler.ReadTable(path, "Computers");
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            ComputersFactoryContext context = new ComputersFactoryContext();
            var rootNode = XMLHandler.ReadXMLFile("../../../../XMLFile/Manufacturers.xml");

            MongoDBHanlder mongoDbHandler = new MongoDBHanlder("ScrewdriverDB");

            XMLHandler.TransferXMLToSQLServer(context, rootNode);
            XMLHandler.TransferXMLToMongoDB(mongoDbHandler, rootNode);
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            SQLiteHandler.TransferSQLiteData();
            var excelHandler = new ExcelHandler();
            var context = new ComputersFactoryContext();
            var mySqlHandler = new MySQLHandler(context);
            var excelExporter = new ExcelExporter(excelHandler, mySqlHandler);
            excelExporter.GenerateReport("../../../Excel-Reports/Reports.xlsx");
        }
    }
}
