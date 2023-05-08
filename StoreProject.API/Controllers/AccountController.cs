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
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly AppDbContextForSP _contextSP;
        private readonly IUserService _userService;

        public AccountController(AppDbContext context, AppDbContextForSP contextSP, IUserService userService)
        {
            _context = context;
            _contextSP = contextSP;
            _userService = userService;
        }

        [HttpGet("GetCustomersByAvailable/{available}")]
        public async Task<ActionResult<CustomerDropDownModel>> GetCustomersByAvailable(bool available)
        {
            try
            {
                var customers = await _context.Customers.Where(x => x.IsAvailable == available).Select(c => new CustomerDropDownModel
                {
                    IDCustomer = c.IDCustomer,
                    Name = c.Name + ' ' + c.LastName
                }).ToListAsync();

                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpGet("GetAllCustomers")]
        public async Task<ActionResult<CustomerDTO>> GetAllCustomers()
        {
            try
            {
                var customers = await _contextSP.up_GetAllCustomers.FromSqlRaw<up_GetAllCustomers_Result>("up_GetAllCustomers").ToListAsync();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerDTO customer)
        {
            try
            {
                if (await _userService.UserExist(customer.User)) return BadRequest("The username has already been registered.");

                List<SqlParameter> parms = _userService.GetParemeterForCustomer(customer, false);

                var results = await _context.Database.ExecuteSqlRawAsync("up_AddCustomer @Name, @LastName, @Address, @User, @Password, @LastUpdated, @IsAvailable", parms.ToArray());

                if (results == 1)
                {
                    return Ok("ResponseMessages.SuccessfulCreation");
                }
                else
                {
                    return BadRequest("ResponseMessages.IDNotFound");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpPut("updateCustomer")]
        public async Task<IActionResult> UpdateCustomer(CustomerDTO customer)
        {
            try
            {
                List<SqlParameter> parms = _userService.GetParemeterForCustomer(customer, true);

                var results = await _context.Database.ExecuteSqlRawAsync("up_ChgCustomerById @IDCustomer, @Name, @LastName, @Address, @User, @Password, @LastUpdated, @Available", parms.ToArray());

                if (results == 1)
                {
                    return Ok("ResponseMessages.SuccessfulUpdate");
                }
                else
                {
                    return BadRequest("ResponseMessages.IDNotFound");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            try
            {
                // Validate if the user exist
                if (await _userService.UserExist(login.User))
                    return Unauthorized("Invalid username");

                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter { ParameterName = "User", Value = login.User.ToLower() });
                parms.Add(new SqlParameter { ParameterName = "Password", Value = login.Password });

                var user = await _contextSP.up_Login.FromSqlRaw<up_Login_Result>("up_Login @User, @Password", parms.ToArray()).AsNoTracking().ToListAsync();

                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpPost("CreateUserProduct")]
        public async Task<IActionResult> CreateUserProduct(CustomerProduct userProductData)
        {
            try
            {
                var userProduct = new CustomerProduct
                {
                    FKCustomer = userProductData.FKCustomer,
                    FKProduct = userProductData.FKProduct,
                    Date = DateTime.Now,
                    IsAvailable = userProductData.IsAvailable
                };

                //_context.CustomerProducts.Add(userProduct);
                await _context.SaveChangesAsync();

                return Ok("ResponseMessages.SuccessfulCreation");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        [HttpPut("UpdateUserProduct")]
        public async Task<IActionResult> UpdateCustomerProduct(CustomerProduct userProductData)
        {
            try
            {
                CustomerProduct customerProduct = _userService.GetCustomerProductByPK(userProductData.IDCustomerProduct);
                customerProduct.FKCustomer = userProductData.FKCustomer;
                customerProduct.FKProduct = userProductData.FKProduct;
                customerProduct.IsAvailable = userProductData.IsAvailable;

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