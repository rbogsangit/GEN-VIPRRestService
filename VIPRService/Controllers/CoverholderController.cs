﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VIPRService.Models;
using VIPRService.Extensions;
using VIPRService.Helpers;
using VIPRService.Constants;
using Microsoft.AspNetCore.Hosting;
using VIPRService.Process;
using VIPRService.Enums;

namespace VIPRService.Controllers
{
    [ApiVersion("1.0")]
    public class CoverholderController : ApiBaseController
    {
        IDataImporter _dataImporter;
        public CoverholderController(IDataImporter dataImporter)
        {
            _dataImporter = dataImporter;
        }

        [HttpPost]
        public IActionResult Post([FromBody]CSVModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dataImporter.Import(model, EnumImportType.Coverholder);
                    return Ok();
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState.GetErrors());
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error(ex);
                return StatusCode(StatusCodes.Status400BadRequest, Messages.Unexpected_Error_Occurred);
            }
        }
    }
}