using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnackBar.Infrastructure.Data.Entities;

namespace SnackBar.Web.Data
{
    public class SnackBarDbContext : DbContext
    {

        public SnackBarDbContext(DbContextOptions<SnackBarDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Requests> Requests{ get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set;}

        public DbSet<Admin> Admins { get; set; }
    }
}