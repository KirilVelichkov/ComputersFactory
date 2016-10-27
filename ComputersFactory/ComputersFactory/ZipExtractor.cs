using Ionic.Zip;
using System;
using System.IO;
using System.Linq;

namespace ComputersFactory
{
    public static class ZipHanlder
    {

        public static void ExtractExcelFiles(string zipFilePath, string[] fileNames)
        {
            var filePathAsArray = zipFilePath.Split('\\');
            var pathToExtract = String.Join("\\", filePathAsArray.Take(filePathAsArray.Length - 1));

            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                foreach (var file in fileNames)
                {
                    ZipEntry entry = zip[file];

                    using (FileStream outputStream = new FileStream(
                        pathToExtract + @"\" + file, FileMode.OpenOrCreate))
                    {
                        entry.Extract(outputStream);
                    }
                }
            }
        }
    }
}
