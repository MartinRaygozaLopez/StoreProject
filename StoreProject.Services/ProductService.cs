
using StoreProject.Services.Interfaces;
using StoreProject.DataAccess;
using StoreProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StoreProject.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace StoreProject.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public Product GetProductByID(int IDProduct)
        {
            return _context.Products.Find(IDProduct);
        }

        public async Task<string> UpdateProduct(Product ProductData)
        {
            Product Product = GetProductByID(ProductData.IDProduct);

            Product.Code = ProductData.Code;
            Product.Description = ProductData.Description;
            Product.Price = ProductData.Price;
            Product.Image = ProductData.Image;
            Product.Stock = ProductData.Stock;
            Product.IsAvailable = ProductData.IsAvailable;

            await _context.SaveChangesAsync();

            return "Successful Update";
        }

        public async Task<string> CreateProduct(Product ProductData)
        {
            var Product = new Product
            {
                Code = ProductData.Code,
                Description = ProductData.Description,
                Price = ProductData.Price,
                Image = ProductData.Image,
                Stock = ProductData.Stock,
                IsAvailable = ProductData.IsAvailable
            };

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return "Successful Creation";
        }

        public async Task<List<Product>> GetProductsByAvailable(bool available)
        {
            return await _context.Products.Where(x => x.IsAvailable == available).ToListAsync();
        }
    }
}
