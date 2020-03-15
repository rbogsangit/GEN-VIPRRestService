using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VIPRService.Extensions
{
    public static class ModelStateExtension 
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var errorList = new List<string>();
            modelState.ToList().ForEach(err =>
            {
                var errList = err.Value.Errors.Select(e => e.ErrorMessage).ToList();
                errList.ForEach(x => errorList.Add(x));
            });

            return errorList;
        }
    }
}
