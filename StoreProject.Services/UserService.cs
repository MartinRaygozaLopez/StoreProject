using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> UserExist(string username)
        {
            return await _context.Customers.AnyAsync(c => c.User == username.ToLower());
        }

        public List<SqlParameter> GetParemeterForCustomer(CustomerDTO customer, bool isUpdate)
        {
            List<SqlParameter> parms = new List<SqlParameter>();

            if (isUpdate)
                parms.Add(new SqlParameter { ParameterName = "IDCustomer", Value = customer.IDCustomer });

            parms.Add(new SqlParameter { ParameterName = "Name", Value = customer.Name });
            parms.Add(new SqlParameter { ParameterName = "LastName", Value = customer.LastName });
            parms.Add(new SqlParameter { ParameterName = "Address", Value = customer.Address });
            parms.Add(new SqlParameter { ParameterName = "User", Value = customer.User.ToLower() });
            parms.Add(new SqlParameter { ParameterName = "Password", Value = customer.Password });
            parms.Add(new SqlParameter { ParameterName = "LastUpdated", Value = DateTime.Now });
            parms.Add(new SqlParameter { ParameterName = "IsAvailable", Value = customer.IsAvailable });

            return parms;
        }

        public CustomerProduct GetCustomerProductByPK(int IDCustomerProduct)
        {
            return _context.CustomerProducts.Find(IDCustomerProduct);
        }
    }
}
