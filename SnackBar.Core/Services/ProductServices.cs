using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackBar.Core.Contracts;
using SnackBar.Core.Models;
using SnackBar.Infrastructure.Data;
using SnackBar.Infrastructure.Data.Entities;
using SnackBar.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace SnackBar.Core.Services
{
    public class ProductServices : IProduct
    {
        private readonly SnackBarDbContext _dbContext;

        public ProductServices(SnackBarDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _dbContext.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Total = p.Total,
                Price = p.Price,
                TotalReserved = p.TotalReserved,
                Image = p.Image == null ? "" : p.Image,
                Description = p.Description
            }).ToListAsync();
            return products.ToList();

        }
        public async Task<bool> AddAsync(Product model)
        {
            bool result;
            try
            {
                var product = new Product
                {
                    Name = model.Name,
                    Total = model.Total,
                    Price = model.Price,
                    TotalReserved = model.TotalReserved,
                    Image = model.Image,
                    Description = model.Description,
                    Id = new Random().Next()

                };
                await _dbContext.AddAsync(product);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }
            return result;
        }
        public async Task<bool> EditAsync(Product model)
        {
            bool result;         
            try
            {
                Product product = GetProductByType(model.Name);
                product.Name = model.Name;
                product.Total = model.Total;
                product.Price = model.Price;
                product.TotalReserved = model.TotalReserved;
                product.Image = model.Image;
                product.Description = model.Description;
                await _dbContext.SaveChangesAsync();
                result = true;
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                result = false;
            }
            Console.WriteLine(result);
            return result;
        }
        public async Task<bool> DeleteAsync(string name)
        {
            bool result;
            try
            {
                Product product = GetProductByType(name);
                await Console.Out.WriteLineAsync(product.Image);
                _dbContext.Remove(product);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
            }
            Console.WriteLine(result);
            return result;
            
        }
        
        public Product GetProductByType(string type)
        {
            Product product = _dbContext.Products.FirstOrDefault(x => x.Name.ToLower() == type.ToLower().Trim());
            if(product != null)
            {
                return product;
            }
            else
            {
                Console.WriteLine("No such product found"); //a messagebox can be initialized
                return null;
            }
        }

        public Product GetProductById(int Id)
        {
            Product product = _dbContext.Products.FirstOrDefault(x => x.Id == Id);
            if (product != null)
            {
                return product;
            }
            else
            {
                Console.WriteLine("No such product found"); //a messagebox can be initialized
                return null;
            }
        }

    }
}
