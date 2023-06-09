﻿using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StoreProject.Services.Interfaces;
using StoreProject.Models.DTOs;
using StoreProject.DataAccess;
using StoreProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StoreProject.Services
{
    public class TokenService : ITokenService
    {
        private readonly AppDbContext _context;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(Customer user)
       {
            var claims = new List<Claim> { 
                new Claim("UserName", user.User)
            };

            claims.Add(new Claim("IDCustomer", user.IDCustomer.ToString()));
            claims.Add(new Claim("FKRole", user.FKRole.ToString()));

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}

