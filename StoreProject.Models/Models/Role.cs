using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StoreProject.Models.Models
{
    public partial class Role
    {
        public Role()
        {
            Customers = new HashSet<Customer>();
        }

        [Key]
        public int IDRole { get; set; }
        [Required]
        [StringLength(100)]
        public string RoleName { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }

        [InverseProperty(nameof(Customer.FKRoleNavigation))]
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
