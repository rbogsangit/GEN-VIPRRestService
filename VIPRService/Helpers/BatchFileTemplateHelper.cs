using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VIPRService.Helpers
{
    public static class BatchFileTemplateHelper
    {
        public static string Create(string currentFolderPath, string templateFilePath, string fileName, string taskRunnerPath, string configTemplateFilePath)
        {
            //var templateFilePath = Path.Combine(rootPath, templateFolderName, fileName);
            //if (!File.Exists(templateFilePath))
            //    throw new Exception($"Template {fileName} does not exists.");

            var contents = File.ReadAllText(templateFilePath);
            contents = contents.Replace("{TaskRunnerPath}", taskRunnerPath);
            contents = contents.Replace("{ConfigTemplateFilePath}", configTemplateFilePath);

            var destinationFilePath = Path.Combine(currentFolderPath, $"{fileName}.bat");
            if (File.Exists(destinationFilePath))
                File.Delete(destinationFilePath);

            File.WriteAllText(destinationFilePath, contents);

            //return csvFilePath;
            return destinationFilePath;
        }
    }
}
