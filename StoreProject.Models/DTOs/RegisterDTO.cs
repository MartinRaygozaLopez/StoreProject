using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Models.DTOs
{
    public class RegisterDTO
    {
        public int IDCustomer { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }

        public string User { get; set; }
        public int FKRole { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }

    }
}