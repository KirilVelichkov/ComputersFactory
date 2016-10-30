using Ionic.Zip;
using System;
using System.IO;
using System.Linq;

namespace ComputersFactory.Logic
{
    public static class ZipHanlder
    {
      public static void ExtractExcelFiles(string zipFilePath)
        {
            var filePathAsArray = zipFilePath.Split('\\');
            var pathToExtract = String.Join("\\", filePathAsArray.Take(filePathAsArray.Length - 1));

            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                foreach (ZipEntry entry in zip)
                {
                    entry.Extract(pathToExtract, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}
