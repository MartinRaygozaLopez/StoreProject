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
        private readonly ITokenService _tokenService;

        public AccountController(AppDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO userDTO)
        {
            if (await UserExist(userDTO.User)) return BadRequest("The username has already been registered.");

            using var hmac = new HMACSHA512();

            var user = new Customer
            {
                User = userDTO.User.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password)),
                PasswordSalt = hmac.Key,
                FKRole = userDTO.FKRole,
                Address = userDTO.Address,
                IsAvailable = userDTO.IsAvailable,
                Name = userDTO.Name,
                LastName = userDTO.LastName
            };

            _context.Customers.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new UserDTO
            {
                Username = user.User,
                Token = _tokenService.CreateToken(user),
                FKRole = userDTO.FKRole
            });
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            try
            {
                // Validate if the user exist
                var user = await _context.Customers.SingleOrDefaultAsync(x => x.User == login.User.ToLower());

                if (user == null) return Unauthorized("Invalid username");

                // Validate the passowrd
                using var hmac = new HMACSHA512(user.PasswordSalt);

                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(login.Password));

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
                }

                return Ok(new UserDTO
                {
                    Username = user.User,
                    Token = _tokenService.CreateToken(user),
                    FKRole = user.FKRole
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message.ToString());
            }
        }

        private async Task<bool> UserExist(string user)
        {
            return await _context.Customers.AnyAsync(c => c.User == user.ToLower());
        }

        //[HttpPost("CreateUserProduct")]
        //public async Task<IActionResult> CreateUserProduct(CustomerProduct userProductData)
        //{
        //    try
        //    {
        //        var userProduct = new CustomerProduct
        //        {
        //            FKCustomer = userProductData.FKCustomer,
        //            FKProduct = userProductData.FKProduct,
        //            Date = DateTime.Now,
        //            IsAvailable = userProductData.IsAvailable
        //        };

        //        //_context.CustomerProducts.Add(userProduct);
        //        await _context.SaveChangesAsync();

        //        return Ok("ResponseMessages.SuccessfulCreation");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message.ToString());
        //    }
        //}

        //[HttpPut("UpdateUserProduct")]
        //public async Task<IActionResult> UpdateCustomerProduct(CustomerProduct userProductData)
        //{
        //    try
        //    {
        //        CustomerProduct customerProduct = _userService.GetCustomerProductByID(userProductData.IDCustomerProduct);
        //        customerProduct.FKCustomer = userProductData.FKCustomer;
        //        customerProduct.FKProduct = userProductData.FKProduct;
        //        customerProduct.IsAvailable = userProductData.IsAvailable;

        //        await _context.SaveChangesAsync();

        //        return Ok("ResponseMessages.SuccessfulUpdate");
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message.ToString());
        //    }

        //}

    }
}