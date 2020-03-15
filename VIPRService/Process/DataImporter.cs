using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VIPRService.Helpers;
using VIPRService.Models;

namespace VIPRService.Process
{
    public interface IDataImporter
    {
        bool Import(CSVModel csvModel);
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
        public bool Import(CSVModel csvModel)
        {
            //create file

            var baseFolderPath = Path.Combine(_env.ContentRootPath, _appSettings.Value.CSVFolderName, $"{csvModel.LegalName}_{csvModel.InternalRef}");
            if (!Directory.Exists(baseFolderPath))
                Directory.CreateDirectory(baseFolderPath);

            var csvFilePath = FileHelper.CreateCSV(baseFolderPath, csvModel);

            //create config template file

            //create batch file

            return true;
        }
    }
}
