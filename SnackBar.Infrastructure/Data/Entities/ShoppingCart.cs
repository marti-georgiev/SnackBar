using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Infrastructure.Data.Entities
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }

        public ICollection<Requests> Requests { get; set; }

        public DateTime GeneratedWhen { get; set; }


        public ShoppingCart() {
        
            Requests = new List<Requests>();
            GeneratedWhen = DateTime.Now;
        }

    }
}
