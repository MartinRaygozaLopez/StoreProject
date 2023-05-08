using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreProject.Models.StoredProcedures
{
    public class up_Login_Result
    {
        [Required]
        [Key]
        public int IDCustomer { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public int FKRole { get; set; }
    }

}
