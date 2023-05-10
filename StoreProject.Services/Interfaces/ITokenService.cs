using Microsoft.Data.SqlClient;
using StoreProject.Models.DTOs;
using StoreProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreProject.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Customer user);
    }
}
