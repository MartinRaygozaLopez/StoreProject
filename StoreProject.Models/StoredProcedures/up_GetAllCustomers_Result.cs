using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Models.StoredProcedures
{
    public class up_GetAllCustomers_Result
    {
        [Required]
        [Key]
        public int IDCustomer { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime LastUpdated { get; set; }
        public bool IsAvailable { get; set; }
    }
}
