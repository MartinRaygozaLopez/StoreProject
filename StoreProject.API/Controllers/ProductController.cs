using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreProject.DataAccess;
using StoreProject.Models.DTOs;
using StoreProject.Models.Models;
using Microsoft.EntityFrameworkCore;
using StoreProject.Services.Interfaces;

namespace StoreProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet("GetProductsByAvailable/{available}")]
        public async Task<ActionResult<Product>> GetProductsByAvailable(bool available)
        {
            try
            {
                return Ok(await _ProductService.GetProductsByAvailable(available));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }


        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<Product>> GetAllProducts()
        {
            try
            {
                return Ok(await _ProductService.GetAllProducts());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct(Product ProductData)
        {
            try
            {
                return Ok(await _ProductService.CreateProduct(ProductData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product ProductData)
        {
            try
            {
                return Ok(await _ProductService.UpdateProduct(ProductData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

    }
}