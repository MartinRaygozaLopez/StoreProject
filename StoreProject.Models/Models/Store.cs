using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace StoreProject.Models.Models
{
    public partial class Store
    {
        public Store()
        {
            Productstores = new HashSet<Productstore>();
        }

        [Key]
        public int IDStore { get; set; }
        [Required]
        [StringLength(50)]
        public string Subsidiary { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        public bool? IsAvailable { get; set; }

        [InverseProperty(nameof(Productstore.FKStoreNavigation))]
        public virtual ICollection<Productstore> Productstores { get; set; }
    }
}
