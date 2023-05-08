using Microsoft.Data.SqlClient;
using StoreProject.Services.Interfaces;
using StoreProject.Models.DTOs;
using StoreProject.DataAccess;
using StoreProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services
{
    public class StoreService : IStoreService
    {
        private readonly AppDbContext _context;

        public StoreService(AppDbContext context)
        {
            _context = context;
        }

        public List<SqlParameter> GetParemeterForStore(StoreDTO store, bool isUpdate)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            if (isUpdate)
                parms.Add(new SqlParameter { ParameterName = "IDStore", Value = store.IDStore });

            parms.Add(new SqlParameter { ParameterName = "Subsidiary", Value = store.Subsidiary });
            parms.Add(new SqlParameter { ParameterName = "Address", Value = store.Address });
            parms.Add(new SqlParameter { ParameterName = "LastUpdated", Value = DateTime.Now });
            parms.Add(new SqlParameter { ParameterName = "IsAvailable", Value = store.IsAvailable });

            return parms;
        }

        public Store GetStoreByPK(int IDStore)
        {
            return _context.Stores.Find(IDStore);
        }
    }
}
