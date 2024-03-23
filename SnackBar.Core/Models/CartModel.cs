using SnackBar.Infrastructure.Data.Entities;
using System.Collections.Generic;

namespace SnackBar.Core.Models
{
    public class CartModel
    {
        public List<Product> Products { get; set; }
        public string Key { get; set; }
    }
}
