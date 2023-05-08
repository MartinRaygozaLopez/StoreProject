using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StoreProject.Models.Models
{
    [Table("CustomerProduct")]
    public partial class CustomerProduct
    {
        [Key]
        public int IDCustomerProduct { get; set; }
        public int FKCustomer { get; set; }
        public int FKProduct { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }

        [ForeignKey(nameof(FKCustomer))]
        [InverseProperty(nameof(Customer.CustomerProducts))]
        public virtual Customer FKCustomerNavigation { get; set; }
        [ForeignKey(nameof(FKProduct))]
        [InverseProperty(nameof(Product.CustomerProducts))]
        public virtual Product FKProductNavigation { get; set; }
    }
}
