using System;
using ComputersFactory.Logic.Reports;

namespace ComputersFactory.Logic
{
    public class ExcelExporter : IExporter
    {
        private readonly ExcelHandler excelHandler;
        private readonly MySQLHandler mySqlHandler;

        public ExcelExporter(ExcelHandler excelHandler, MySQLHandler mySqlHandler)
        {
            this.excelHandler = excelHandler;
            this.mySqlHandler = mySqlHandler;
        }

        public void GenerateReport(string path)
        {
            string tableName = "reports";
            string sheetName = "Sheet1";
            var data = this.mySqlHandler.ReadTable(tableName);
            excelHandler.TransferToExcel(data, path, sheetName);
        }
    }
}
