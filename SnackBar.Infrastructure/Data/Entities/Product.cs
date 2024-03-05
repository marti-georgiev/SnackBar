using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SnackBar.Infrastructure.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        
        public string? Name { get; set; } = string.Empty;

        [Required]
        public int Total { get; set; } = 0;

        [Required]
        public decimal Price { get; set; } = decimal.Zero;
        
        [Required]
        public int TotalReserved { get; set; }= 0;
        
        public string Image { get; set; } = "Hi";

        public string Description { get; set; } = "Hi";

        //public DateTime? LastOrder { get; set; }
        public Product ()
        {
           //LastOrder = DateTime.Now;
        }

        public override string ToString()
        {
            return "Name: " + Name + " Total: " + Total + " Price: " + Price + " TotalReserved: " + TotalReserved + " Description: " + Description;
        }


    }
}
