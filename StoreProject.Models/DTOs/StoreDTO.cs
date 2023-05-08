using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Models.DTOs
{
    public class StoreDTO
    {
        [Required]
        public int IDStore { get; set; }
        [Required]
        public string Subsidiary { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public DateTime LastUpdated { get; set; }
        public bool IsAvailable { get; set; }
    }
}