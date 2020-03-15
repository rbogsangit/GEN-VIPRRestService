using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VIPRService.Models
{
    public class CSVModel
    {
        [Required]
        public string LegalName { get; set; }
        [Required]
        public string InternalRef { get; set; }
        public bool UseParentFinancials { get; set; }
    }
}
