using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Infrastructure.Data.Entities
{
    public class Requests
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public int OrderedCount { get; set; } = 1;

        [ForeignKey("ShoppingCart")]
        public int ShoppingCartId { get; set; }
        
        //[ForeignKey(nameof(ProductId))]
        public Product? Product { get; set; } 





    }
}
