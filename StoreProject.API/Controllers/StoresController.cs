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
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet("GetStoresByAvailable/{available}")]
        public async Task<ActionResult<StoresDropDownModel>> GetStoresByAvailable(bool available)
        {
            try
            {
                return Ok(await _storeService.GetStoresByAvailable(available)) ;
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpGet("GetAllStores")]
        public async Task<ActionResult<List<Store>>> GetAllStores()
        {
            try
            {
                return Ok(await _storeService.GetAllStores());
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
                return Ok(await _storeService.CreateStore(storeData));
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
                return Ok(await _storeService.UpdateStore(storeData));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }
    }
}