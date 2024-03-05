using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackBar.Infrastructure.Data.Entities;

namespace SnackBar.Core.Models
{
    class CartModel
    {
        public List<Product> Products { get; set; }
        public string key { get; set; }

    }
}
