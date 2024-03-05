using SnackBar.Infrastructure.Data.Entities;
using SnackBar.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnackBar.Core.Services;
using SnackBar.Core.Contracts;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using SnackBar.Core.Models;

namespace SnackBar.Core.Services
{
    public class ShoppingCartServices : ICart
    {
        private readonly SnackBarDbContext _dbContext;

        private int cartLifeLen = 30; //in minutes


        public ShoppingCartServices(SnackBarDbContext dbContext)
        {
            _dbContext = dbContext;
            productServices = new ProductServices(dbContext);            
        }

        private ProductServices productServices;

        public Requests CreateRequest(Product product, int orderedCount)
        {
            Requests request = new Requests();
            request.Product = product;
            request.ProductId = product.Id;
            request.OrderedCount = orderedCount;
            return request;
        }
        public ICollection<Requests> KeepAllRequestsInCollection(int cartId)
        {

            List<Requests> allRequests = _dbContext.Requests.ToList();

            foreach(var request in allRequests)

            {
                request.Product = _dbContext.Products.FirstOrDefault(x => x.Id == request.ProductId);
            }

            var requests = allRequests.FindAll(x => x.ShoppingCartId == cartId);

            return requests;
        }

        public async Task<object> CreateCart(string productName, int orderedCount)
        {
            bool result;
            try
            {
                ShoppingCart newCart = new ShoppingCart();
                Product product = productServices.GetProductByType(productName);
                Requests request = CreateRequest(product, orderedCount);
                if ((product.Total - product.TotalReserved) < orderedCount)
                {
                    result = false;
                    await Console.Out.WriteLineAsync("There was not enough quantity!");
                }
                else
                {
                    product.TotalReserved = product.TotalReserved + orderedCount;
                    product.Total = product.Total - orderedCount;
                    request.ShoppingCartId = newCart.Id;
                    newCart.Requests.Add(request);
                    await _dbContext.Requests.AddAsync(request);
                    await _dbContext.ShoppingCarts.AddAsync(newCart);
                    await _dbContext.SaveChangesAsync();
                    newCart.Requests = KeepAllRequestsInCollection(newCart.Id);
                    result = true;
                    return newCart.Id;
                }
            }
            catch
            {
                await Console.Out.WriteLineAsync("Invalid operation!");
                result = false;
            }
            return result;
        }
        public async Task<bool> Sync()
        {

            try
            {

				var carts = _dbContext.ShoppingCarts.ToList();

				if (carts != null)
				{

					List<int> cartsToDelete = new();

					
					await Console.Out.WriteLineAsync(carts.Count + "");

					foreach (var cart in carts)
					{

						cart.Requests = KeepAllRequestsInCollection(cart.Id);

						if (cart.GeneratedWhen.AddMinutes(cartLifeLen) < DateTime.Now || cart.Requests.Any(x => x.Product == null))
						{
							//await DeleteCartNoSave(cart.Id);
							cartsToDelete.Add(cart.Id);
						}

					}

					try
					{

						for (int i = 0; i < cartsToDelete.Count; i++)
						{
							await DeleteCart(cartsToDelete[i]);
						}

					}
					catch (Exception ex)
					{
						await Console.Out.WriteLineAsync(ex.Message);
					}



					return true;
				}
				else
				{
					return false;
				}
			}catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            return false;
            
        }

        public async Task<bool> DeleteCart(int cartId)
        {

            bool result;

            ShoppingCart cart = await _dbContext.ShoppingCarts.FindAsync(cartId);
            cart.Requests = KeepAllRequestsInCollection(cartId);

            try
            {

                foreach (var request in cart.Requests)
                {
                    _dbContext.Products.First(x => x.Id == request.ProductId).Total += request.OrderedCount;
                    _dbContext.Products.First(x => x.Id == request.ProductId).TotalReserved -= request.OrderedCount;
                }
                _dbContext.ShoppingCarts.Remove(cart);
                await _dbContext.SaveChangesAsync();
                result = true;
            }
            catch
            {
                result = false;
                await Console.Out.WriteLineAsync("Error. You couldn not delete the cart.");
            }
            return result;
        }

		public async Task<bool> DeleteCartNoSave(int cartId)
		{

			bool result;

			ShoppingCart cart = await _dbContext.ShoppingCarts.FindAsync(cartId);
			cart.Requests = KeepAllRequestsInCollection(cartId);

			try
			{

				foreach (var request in cart.Requests)
				{
					_dbContext.Products.First(x => x.Id == request.ProductId).Total += request.OrderedCount;
					_dbContext.Products.First(x => x.Id == request.ProductId).TotalReserved -= request.OrderedCount;
				}
				_dbContext.ShoppingCarts.Remove(cart);
				result = true;
			}
			catch
			{
				result = false;
				await Console.Out.WriteLineAsync("Error. You couldn not delete the cart.");
			}
			return result;
		}

		public async Task<CartDisplayModel> LoadCart(int cardId)
        {
            try
            {

				await Console.Out.WriteLineAsync("ID of cart trying to load: " + cardId);

                ShoppingCart cart = await _dbContext.ShoppingCarts.FindAsync(cardId);

                CartDisplayModel cartDisplayModel = new();

                cartDisplayModel.Id = cardId;

                cartDisplayModel.createdWhen = cart.GeneratedWhen;

                List<ProductInCartModel> productsInCart = new();

                if (cart != null)
                {
                    var requests = KeepAllRequestsInCollection(cardId);
                    await Console.Out.WriteLineAsync("Got all requests");
                    foreach (var request in requests)
                    {

                        var product = productServices.GetProductById(request.ProductId);

                        var productInCart = new ProductInCartModel
                        {
                            product = product,
                            orderedAmount = request.OrderedCount
                        };

                        productsInCart.Add(productInCart);
                    }

                    cartDisplayModel.products = productsInCart;

                    return cartDisplayModel;
                }
                else
                {
                    await Console.Out.WriteLineAsync("no cart with id " + cardId);
                    return null;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Something went wrong with loading cart");
                await Console.Out.WriteLineAsync(ex.Message);
                return null;
            }

        }



        public async Task<bool> ChangeProductCount(string productName, int newCount, int cartId)
        {
            try
            {
               
                var cart = _dbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
                //
                cart.Requests = KeepAllRequestsInCollection(cartId);
                var request = cart.Requests.FirstOrDefault(x => x.Product.Name.Equals(productName));
                int productId = request.ProductId;
                var product = productServices.GetProductById(productId);
                if (request != null && cart != null)
                {
                    //var product = request.Product;
                    if (product.Total - product.TotalReserved >= newCount) //?
                    {
                        _dbContext.Products.First(x => x == product).Total += request.OrderedCount - newCount;
                        _dbContext.Products.First(x => x == product).TotalReserved += newCount - request.OrderedCount;
                        _dbContext.Requests.First(x => x == request).OrderedCount = newCount;
                        _dbContext.ShoppingCarts.First(x => x.Id == cartId).GeneratedWhen = DateTime.Now;
                        await _dbContext.SaveChangesAsync();
                        return true;
                    }
                }
                return false;

			}
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }

        }


        public async Task<bool> RemoveProduct(string productName, int cartId)
        {
            try
            {

				var cart = _dbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
                cart.Requests = KeepAllRequestsInCollection(cart.Id);
                Product product = productServices.GetProductByType(productName);
                if (cart != null)
                {
                    Requests request = cart.Requests.FirstOrDefault(x => x.Product.Name == productName);
                    cart.Requests.Remove(request);
                    _dbContext.Requests.Remove(request);

                    _dbContext.Products.First(x => x.Id == product.Id).TotalReserved = product.TotalReserved - request.OrderedCount;
                    _dbContext.Products.First(x => x.Id == product.Id).Total = product.Total + request.OrderedCount;


                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }

        }

        public async Task<bool> AddProduct(string productName, int count, int cartId)
        {
            try
            {
                var cart = _dbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);

                Product product = productServices.GetProductByType(productName);
                if (cart != null)
                {
                    cart.Requests = KeepAllRequestsInCollection(cartId);
                    var request = new Requests();

                    request.OrderedCount = count;
                    request.ShoppingCartId = cartId;
                    //request.Product = _dbContext.Products.FirstOrDefault(x => x.Name.Trim().Equals(productName.Trim()));
                    request.ProductId = product.Id;
                    request.Product = product;
                    //.Requests = KeepAllRequestsInCollection(cart.Id);
                    if (!cart.Requests.Any(x => x.ProductId == product.Id))
                    {
                        if (count <= product.Total - product.TotalReserved)
                        {
                            //ako ima problem v koito se pravqt promeni no ne se zapazvat e vuzmojno da e ot tuk
                            request.OrderedCount = count;
                            product.TotalReserved += count;
                            product.Total = product.Total - count;
                            //_dbContext.ShoppingCarts.First(x=>x.Id==cartId).Requests.Add(request);
                            cart.Requests.Add(request);
                            _dbContext.ShoppingCarts.First(x => x.Id == cartId).GeneratedWhen = DateTime.Now;
                            await _dbContext.SaveChangesAsync();
                            return true;
                        }
                    }
                    else
                    {
                        int foreignCount = KeepAllRequestsInCollection(cartId).Where(x => x.ProductId == request.ProductId).Sum(x => x.OrderedCount);
                        if (await ChangeProductCount(product.Name, request.OrderedCount + foreignCount, cart.Id))
                        {
                            await _dbContext.SaveChangesAsync();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }

        public async Task<bool> MarkAsDone(int cartId)
        {
            try
            {

                var cart = _dbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);

                if (cart != null)
                {
                    cart.Requests = KeepAllRequestsInCollection(cartId);
                    foreach (var request in cart.Requests)
                    {
                        //int productId = request.ProductId;
                        //int countPr = request.OrderedCount;
                        var productId = request.ProductId;
                        Product product = _dbContext.Products.Find(productId);

                        //_dbContext.Products.First(x => x == product).Total -= request.OrderedCount;
                        //_dbContext.Products.First(x => x == product).TotalReserved -= request.ShoppingCartId;
                        _dbContext.Requests.Remove(request);
                        _dbContext.ShoppingCarts.Remove(cart);
                        await _dbContext.SaveChangesAsync();

                    }
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
        }

    }
}
