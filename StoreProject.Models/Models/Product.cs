using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StoreProject.Models.Models
{
    public partial class Product
    {
        public Product()
        {
            CustomerProducts = new HashSet<CustomerProduct>();
            Productstores = new HashSet<Productstore>();
        }

        [Key]
        public int IDProduct { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }
        [Required]
        public string Image { get; set; }
        public int Stock { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }

        [InverseProperty(nameof(CustomerProduct.FKProductNavigation))]
        public virtual ICollection<CustomerProduct> CustomerProducts { get; set; }
        [InverseProperty(nameof(Productstore.FKProductNavigation))]
        public virtual ICollection<Productstore> Productstores { get; set; }
    }
}
