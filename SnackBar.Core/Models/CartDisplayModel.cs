using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Core.Models
{
    public class CartDisplayModel
    {
        public List<ProductInCartModel> products { get; set; } = new List<ProductInCartModel>();
        public System.DateTime createdWhen { get; set; } = DateTime.Now;
        public int Id { get; set; }


        public override string ToString()
        {
            string toReturn = string.Empty;
            toReturn += "Generated: " + createdWhen + "\n";
            toReturn += "Id: " + Id + "\n";
            
            foreach(var item in products)
            {
                toReturn += item.ToString() +"\n";
            }
            return toReturn;
        }
    }
}
