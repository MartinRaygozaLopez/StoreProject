using Microsoft.Data.SqlClient;
using StoreProject.Services.Interfaces;
using StoreProject.Models.DTOs;
using StoreProject.DataAccess;
using StoreProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace StoreProject.Services
{
    public class StoreService : IStoreService
    {
        private readonly AppDbContext _context;

        public StoreService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Store> GetStoreByID(int IDStore)
        {
            return await _context.Stores.FindAsync(IDStore);
        }

        public async Task<List<StoresDropDownModel>> GetStoresByAvailable(bool available)
        {
           return await _context.Stores.Where(x => x.IsAvailable == available).Select(c => new StoresDropDownModel
            {
                IDStore = c.IDStore,
                Subsidiary = c.Subsidiary
            }).ToListAsync();
        }
        public async Task<List<Store>> GetAllStores()
        {
            return await _context.Stores.ToListAsync();
        }

        public async Task<string> CreateStore(Store storeData)
        {
            var store = new Store
            {
                Subsidiary = storeData.Subsidiary,
                Address = storeData.Address,
                IsAvailable = storeData.IsAvailable
            };

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return "Successful Creation";
        }

        public async Task<string> UpdateStore(Store storeData)
        {
            Store store = await GetStoreByID(storeData.IDStore);
            store.Subsidiary = storeData.Subsidiary;
            store.Address = storeData.Address;
            store.IsAvailable = storeData.IsAvailable;

            await _context.SaveChangesAsync();

            return "Successful Update";
        }
    }
}
