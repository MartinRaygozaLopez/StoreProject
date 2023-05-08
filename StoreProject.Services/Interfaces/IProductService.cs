using Microsoft.Data.SqlClient;
using StoreProject.Models.DTOs;
using StoreProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Interfaces
{
    public interface IProductService
    {
        public Product GetProductByID(int IDProduct);

        public Productstore GetProductStoreByID(int IDProductStore);

        public Task<List<Product>> GetAllProducts();

        public Task<List<ProductDropDownModel>> GetProductsByAvailable(bool available);

        public Task<string> CreateProduct(Product ProductData);

        public Task<string> UpdateProduct(Product ProductData);
    }
}
