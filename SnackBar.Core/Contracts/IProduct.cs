using SnackBar.Core.Models;
using SnackBar.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Core.Contracts
{
    public interface IProduct
    {

        //public Task AddAsync (Product model);

        //public Task DeleteAsync (string name);

        //public Task EditAsync (Product model);

        //public Task <List<Product>> GetAllAsync ();


        public Task<bool> AddAsync (Product model);
        public Task<bool> EditAsync (Product model);
        public Task<bool> DeleteAsync (string name);
        //public Task<bool> GetProductByType(string type);
        public Product GetProductByType(string type);
        public Task<List<Product>> GetAllAsync();

    }
}
