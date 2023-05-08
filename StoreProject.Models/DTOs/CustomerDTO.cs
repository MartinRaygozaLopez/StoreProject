using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Models.DTOs
{
    public class CustomerDTO
    {
        [Required]
        public int IDCustomer { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int FKRole { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsAvailable { get; set; }
    }
}