using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Models.DTOs
{
    public class ProductDropDownModel
    {
        [Required]
        public int IDProduct { get; set; }
        [Required]
        public string Description { get; set; }
    }
}