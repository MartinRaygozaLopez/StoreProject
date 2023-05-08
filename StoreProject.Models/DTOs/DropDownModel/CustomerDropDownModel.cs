using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Models.DTOs
{
    public class CustomerDropDownModel
    {
        [Required]
        public int IDCustomer { get; set; }
        [Required]
        public string Name { get; set; }
    }
}