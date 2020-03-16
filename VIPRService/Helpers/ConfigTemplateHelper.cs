using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VIPRService.Helpers
{
    public static class ConfigTemplateHelper
    {        
        public static string Create(string rootPath, string currentFolderPath, string templateFolderName, string fileName, string csvFilePath)
        {
            var templateFilePath = Path.Combine(rootPath, templateFolderName, fileName);
            if (!File.Exists(templateFilePath))
                throw new Exception($"Template {fileName} does not exists.");

            var contents = File.ReadAllText(templateFilePath);
            contents = contents.Replace("{CSVFilePath}", csvFilePath);

            var destinationFilePath = Path.Combine(currentFolderPath, fileName);
            if (File.Exists(destinationFilePath))
                File.Delete(destinationFilePath);

            File.WriteAllText(destinationFilePath, contents);

            //return csvFilePath;
            return destinationFilePath;
        }
    }
}
