using CodeFirst;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputersFactory.Reports
{
    public class PdfGenerator
    {
        public void CreatePdf(string path, ComputersFactoryContext context)
        {
            FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

            Document doc = new Document();

            PdfWriter writer = PdfWriter.GetInstance(doc, stream);
            doc.Open();

            var columns = 4;
            var table = new PdfPTable(columns);
            PdfPCell cell = new PdfPCell(new Phrase("Processors report"));
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);

            var tableHeaders = new List<string>() { "Manufacturer", "Model", "MHz", "Price" };
            for (int i = 0; i < columns; i++)
            {
                var headerCell = new PdfPCell(new Phrase(tableHeaders[i]));
                headerCell.HorizontalAlignment = 1;
                table.AddCell(headerCell);
            }
            
            foreach (var processor in context.Processors)
            {
                var manufacturerName = new PdfPCell();
                var processorModel = new PdfPCell();
                var processorMHz = new PdfPCell();
                var processorPrice = new PdfPCell();

                manufacturerName.AddElement(new Phrase(processor.Manufacturer.Name));
                manufacturerName.HorizontalAlignment = 1;
                processorModel.AddElement(new Phrase(processor.Model));
                processorModel.HorizontalAlignment = 1;
                processorMHz.AddElement(new Phrase(processor.MhZ.ToString()));
                processorMHz.HorizontalAlignment = 1;
                processorPrice.AddElement(new Phrase(processor.Price.ToString()));
                processorPrice.HorizontalAlignment = 1;

                table.AddCell(manufacturerName);
                table.AddCell(processorModel);
                table.AddCell(processorMHz);
                table.AddCell(processorPrice);
            }

            doc.Add(table);
            doc.Close();
        }
    }
}
