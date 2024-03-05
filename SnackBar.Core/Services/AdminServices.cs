using Microsoft.EntityFrameworkCore;
using SnackBar.Core.Contracts;
using SnackBar.Infrastructure.Data.Entities;
using SnackBar.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Core.Services
{
    public class AdminServices:IAdmin
    {
        private readonly SnackBarDbContext _dbContext;
        public AdminServices(SnackBarDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddNewKey(string key)
        {
            try
            {
                Admin admin = new Admin()
                {
                    AdminKey = key
                };
                _dbContext.Add(admin);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
            public async Task<bool> IsKeyValid(string key_attempt)
        {
            if(await _dbContext.Admins.AnyAsync(x => x.AdminKey == key_attempt))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
