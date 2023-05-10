using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StoreProject.Models.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CustomerProducts = new HashSet<CustomerProduct>();
        }

        [Key]
        public int IDCustomer { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string User { get; set; }
        public byte[] PasswordHash { get; set; }
        public int FKRole { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }
        public byte[] PasswordSalt { get; set; }

        [ForeignKey(nameof(FKRole))]
        [InverseProperty(nameof(Role.Customers))]
        public virtual Role FKRoleNavigation { get; set; }
        [InverseProperty(nameof(CustomerProduct.FKCustomerNavigation))]
        public virtual ICollection<CustomerProduct> CustomerProducts { get; set; }
    }
}
