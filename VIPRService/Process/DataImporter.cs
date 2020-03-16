using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VIPRService.Enums;
using VIPRService.Helpers;
using VIPRService.Models;

namespace VIPRService.Process
{
    public interface IDataImporter
    {
        bool Import(CSVModel csvModel, EnumImportType enumImportType);
    }
    public class DataImporter : IDataImporter
    {
        IWebHostEnvironment _env;
        IOptionsSnapshot<AppSettings> _appSettings;
        public DataImporter(IWebHostEnvironment env, IOptionsSnapshot<AppSettings> appSettings)
        {
            _env = env;
            _appSettings = appSettings;
        }
        public bool Import(CSVModel csvModel, EnumImportType enumImportType)
        {
            //create file
            var key = $"{csvModel.LegalName}_{csvModel.InternalRef}";

            var currentFolderPath = Path.Combine(_env.ContentRootPath, _appSettings.Value.CSVFolderName, key);
            if (!Directory.Exists(currentFolderPath))
                Directory.CreateDirectory(currentFolderPath);

            //create CSV file
            var csvFilePath = FileHelper.CreateCSV(currentFolderPath, csvModel, key);
            if (string.IsNullOrEmpty(csvFilePath))
                throw new Exception($"{enumImportType.ToString()} csv file not created for {key}");

            //create config template file
            var configTemplateFileName = ImportType.GetConfigTemplate(enumImportType);
            if (string.IsNullOrEmpty(configTemplateFileName))
                throw new Exception($"{enumImportType.ToString()} config template not configured.");

            var configTemplatePath = ConfigTemplateHelper.Create(_env.ContentRootPath, currentFolderPath, _appSettings.Value.ConfigTemplateFolderName, configTemplateFileName, csvFilePath);
            if (string.IsNullOrEmpty(configTemplatePath))
                throw new Exception($"{enumImportType.ToString()} config template not created for {key}");

            //create batch file
            var batchFileTemplateName = ImportType.GetBatchFileTemplate(enumImportType);
            if (string.IsNullOrEmpty(batchFileTemplateName))
                throw new Exception($"{enumImportType.ToString()} batch file template not configured.");

            var templateFilePath = Path.Combine(_env.ContentRootPath, _appSettings.Value.BatchFileTemplateFolderName, batchFileTemplateName);
            if (!File.Exists(templateFilePath))
                throw new Exception($"Batch file template {batchFileTemplateName} does not exists.");

            var batchFilePath = BatchFileTemplateHelper.Create(currentFolderPath, templateFilePath, enumImportType.ToString(), _appSettings.Value.TaskRunnerPath, configTemplatePath);
            if (string.IsNullOrEmpty(configTemplatePath))
                throw new Exception($"{enumImportType.ToString()} batch file template not created for {key}");

            //Execute batch file

            return true;
        }        
    }
}
