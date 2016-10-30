using ComputersFactory.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace ComputersFactory.Logic.Reports
{
    public class PdfExporter
    {
        private ComputersFactoryContext dbContext;

        public PdfExporter(ComputersFactoryContext context)
        {
            this.dbContext = context;
        }

        public void CreatePdf(string path)
        {
            Directory.CreateDirectory(path);

            var filePath = path + "/Computers.pdf";
            using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document doc = new Document();

                PdfWriter writer = PdfWriter.GetInstance(doc, stream);
                doc.Open();

                var columns = 6;
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                var boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                var table = new PdfPTable(columns);
                PdfPCell cell = new PdfPCell(new Phrase("Computers report", titleFont));
                cell.Colspan = 6;
                cell.HorizontalAlignment = 1;
                table.AddCell(cell);

                var tableHeaders = new List<string>() { "ComputerType", "Manufacturer", "Processor", "Memory", "Video card", "Price" };
                for (int i = 0; i < columns; i++)
                {
                    var headerCell = new PdfPCell(new Phrase(tableHeaders[i], boldFont));
                    headerCell.HorizontalAlignment = 1;
                    table.AddCell(headerCell);
                }

                var computersSum = 0M;
                foreach (var computer in this.dbContext.Computers)
                {
                    var computerType = new PdfPCell();
                    var computerManufacturer = new PdfPCell();
                    var computerProcessor = new PdfPCell();
                    var computerMemory = new PdfPCell();
                    var computerVideo = new PdfPCell();
                    var computerPrice = new PdfPCell();

                    var type = computer.ComputerType.Type;
                    computerType.AddElement(new Phrase(type));
                    var manufacturer = computer.Processor.Manufacturer.Name;
                    computerManufacturer.AddElement(new Phrase(manufacturer));
                    var processor = $"{computer.Processor.Model} {computer.Processor.MhZ} MHz";
                    computerProcessor.AddElement(new Phrase(processor));
                    var memory = $"{computer.Memorycard.Capacity} RAM";
                    computerMemory.AddElement(new Phrase(memory));
                    var videoCard = $"{computer.Videocard.Model} {computer.Videocard.Memory}";
                    computerVideo.AddElement(new Phrase(videoCard));
                    var price = computer.Price;
                    computerPrice.AddElement(new Phrase(price.ToString()));

                    table.AddCell(computerType);
                    table.AddCell(computerManufacturer);
                    table.AddCell(computerProcessor);
                    table.AddCell(computerMemory);
                    table.AddCell(computerVideo);
                    table.AddCell(computerPrice);
                    computersSum += computer.Price;
                }

                PdfPCell computersAllPrice = new PdfPCell();

                var phrase = new Phrase();
                phrase.Add(new Chunk("Total price:", boldFont));
                phrase.Add(new Chunk($" {computersSum} $", normalFont));
                computersAllPrice.AddElement(phrase);
                computersAllPrice.Colspan = 6;

                table.AddCell(computersAllPrice);

                doc.Add(table);
                doc.Close();
            }
        }
    }
}
