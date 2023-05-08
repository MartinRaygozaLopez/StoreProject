using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreProject.DataAccess;
using StoreProject.Models.DTOs;
using StoreProject.Models.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.Data.SqlClient;
using StoreProject.Models.StoredProcedures;
using StoreProject.Services.Interfaces;

namespace StoreProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AppDbContextForSP _contextSP;
        private readonly IStoreService _storeService;

        public StoresController(AppDbContext context, AppDbContextForSP contextSP, IStoreService storeService)
        {
            _context = context;
            _contextSP = contextSP;
            _storeService = storeService;
        }

        [HttpGet("GetStoresByAvailable/{available}")]
        public async Task<ActionResult<StoresDropDownModel>> GetStoresByAvailable(bool available)
        {
            try
            {
                var stores = await _context.Stores.Where(x => x.IsAvailable == available).Select(c => new StoresDropDownModel
                {
                    IDStore = c.IDStore,
                    Subsidiary = c.Subsidiary
                }).ToListAsync();

                return Ok(stores);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpGet("GetAllStores")]
        public async Task<ActionResult<StoreDTO>> GetAllStores()
        {
            try
            {
                var stores = await _context.Stores.Select(c => new StoreDTO
                {
                    IDStore = c.IDStore,
                    Subsidiary = c.Subsidiary,
                    Address = c.Address,
                    IsAvailable = (bool)c.IsAvailable
                }).ToListAsync();

                return Ok(stores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("CreateStore")]
        public async Task<IActionResult> CreateStore(Store storeData)
        {
            try
            {
                var store = new Store
                {
                    Subsidiary = storeData.Subsidiary,
                    Address = storeData.Address,
                    IsAvailable = storeData.IsAvailable
                };

                _context.Stores.Add(store);
                await _context.SaveChangesAsync();

                return Ok("ResponseMessages.SuccessfulCreation");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpPut("UpdateStore")]
        public async Task<IActionResult> UpdateStore(Store storeData)
        {
            try
            {
                Store store = _storeService.GetStoreByPK(storeData.IDStore);
                store.Subsidiary = storeData.Subsidiary;
                store.Address = storeData.Address;
                store.IsAvailable = storeData.IsAvailable;

                await _context.SaveChangesAsync();

                return Ok("ResponseMessages.SuccessfulUpdate");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }
    }
}