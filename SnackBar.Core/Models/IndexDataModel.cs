using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackBar.Infrastructure.Data.Entities;
using SnackBar.Core.Services;
namespace SnackBar.Core.Models
{
    public class IndexDataModel
    {

        //public List<ProductViewModel> products { get; set; }
        public List<Product> products { get; set; } = new List<Product>();
        public string adminKey { get; set; } = string.Empty;
    }
}
