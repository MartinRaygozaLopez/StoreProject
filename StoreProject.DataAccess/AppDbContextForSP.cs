using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreProject.Models.StoredProcedures;

#nullable disable

namespace StoreProject.DataAccess
{
    public partial class AppDbContextForSP : DbContext
    {
        public AppDbContextForSP()
        {
        }

        public AppDbContextForSP(DbContextOptions<AppDbContextForSP> options)
            : base(options)
        {
        }

        //====================================== SP ======================================
        public virtual DbSet<up_Login_Result> up_Login { get; set; }
        public virtual DbSet<up_GetAllCustomers_Result> up_GetAllCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Do nothing
        }
    }
}
