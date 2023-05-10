using Microsoft.Data.SqlClient;
using StoreProject.Models.DTOs;
using StoreProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Interfaces
{
    public interface IStoreService
    {
        public Task<Store> GetStoreByID(int IDStore);

        public Task<List<StoresDropDownModel>> GetStoresByAvailable(bool available);

        public Task<List<Store>> GetAllStores();

        public Task<string> CreateStore(Store storeData);

        public Task<string> UpdateStore(Store storeData);
    }
}
