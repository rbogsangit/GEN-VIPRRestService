using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VIPRService.Enums
{
    public enum EnumImportType
    {
        Coverholder,
        Binder
    }

    public static class ImportType
    {
        public static string GetConfigTemplate(EnumImportType enumImportType)
        {
            var configName = string.Empty;
            switch (enumImportType)
            {
                case EnumImportType.Coverholder:
                    configName = "Coverholder.config.template";
                    break;
                case EnumImportType.Binder:
                    configName = "Binder.config.template";
                    break;
                default:
                    break;
            }

            return configName;
        }

        public static string GetBatchFileTemplate(EnumImportType enumImportType)
        {
            var batchFileName = string.Empty;
            switch (enumImportType)
            {
                case EnumImportType.Coverholder:
                case EnumImportType.Binder:
                    batchFileName = "BatchFileTemplate.bat";
                    break;
                default:
                    break;
            }

            return batchFileName;
        }
    }
}
