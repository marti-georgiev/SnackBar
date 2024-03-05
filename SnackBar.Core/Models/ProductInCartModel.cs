using SnackBar.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Core.Models
{
    public class ProductInCartModel
    {
        public Product product { get; set; } = new Product();
        public int orderedAmount { get; set; } = 0;

        public override string ToString()
        {
            return product.ToString() + " Ordered amount: " + orderedAmount;
        }
    }
}
