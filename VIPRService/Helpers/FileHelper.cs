using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VIPRService.Models;

namespace VIPRService.Helpers
{
    public static class FileHelper
    {
        public static string CreateCSV(string currentFolderPath, CSVModel model, string fileName)
        {
            var csvFilePath = Path.Combine(currentFolderPath, $"{fileName}.csv");

            using (var writer = new StreamWriter(csvFilePath, false))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteHeader<CSVModel>();
                    csv.NextRecord();
                    csv.WriteRecord(model);
                }
            }
            return csvFilePath;
        }
    }
}
