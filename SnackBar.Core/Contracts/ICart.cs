using SnackBar.Core.Models;
using SnackBar.Infrastructure.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnackBar.Core.Contracts
{
   public interface ICart
    {
        public Task<object> CreateCart(string productNameр, int orderedCount);
        public Task<bool> Sync();
        public Task<bool> DeleteCart(int cardId);
        public Task<CartDisplayModel> LoadCart(int cardId);
        public Task<bool> ChangeProductCount(string productName, int newCount, int cartId);

        public Task<bool> RemoveProduct(string productName, int cartId);

        public Task<bool> AddProduct(string productName, int count, int cartId);

        public Task<bool> MarkAsDone(int cartId);
   }
}
