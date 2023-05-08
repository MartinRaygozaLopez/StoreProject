using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Models.DTOs
{
    public class StoresDropDownModel
    {
        [Required]
        public int IDStore { get; set; }
        [Required]
        public string Subsidiary { get; set; }
    }
}