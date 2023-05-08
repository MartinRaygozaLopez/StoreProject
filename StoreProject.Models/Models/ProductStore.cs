using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StoreProject.Models.Models
{
    [Table("Productstore")]
    public partial class Productstore
    {
        [Key]
        public int IDProductstore { get; set; }
        public int FKProduct { get; set; }
        public int FKStore { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }

        [ForeignKey(nameof(FKProduct))]
        [InverseProperty(nameof(Product.Productstores))]
        public virtual Product FKProductNavigation { get; set; }
        [ForeignKey(nameof(FKStore))]
        [InverseProperty(nameof(Store.Productstores))]
        public virtual Store FKStoreNavigation { get; set; }
    }
}
