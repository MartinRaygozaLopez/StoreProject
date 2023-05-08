using Microsoft.Data.SqlClient;
using StoreProject.Models.DTOs;
using StoreProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> UserExist(string username);
        List<SqlParameter> GetParemeterForCustomer(CustomerDTO customer, bool isUpdate);
        public CustomerProduct GetCustomerProductByPK(int IDCustomerProduct);
    }
}
