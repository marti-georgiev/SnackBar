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
            ProductServices = new ProductServices(dbContext);            
        }

        private ProductServices productServices;

        public SnackBarDbContext DbContext => _dbContext;

        public int CartLifeLen { get => cartLifeLen; set => cartLifeLen = value; }
        public ProductServices ProductServices { get => productServices; set => productServices = value; }

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

            List<Requests> allRequests = DbContext.Requests.ToList();

            foreach(var request in allRequests)

            {
                request.Product = DbContext.Products.FirstOrDefault(x => x.Id == request.ProductId);
            }

            var requests = allRequests.FindAll(x => x.ShoppingCartId == cartId);

            return requests;
        }

        public async Task<object> CreateCart(string productNameр, int orderedCount)
        {
            try
            {
                ShoppingCart newCart = new ShoppingCart();
                Product product = ProductServices.GetProductByType(productNameр);
                Requests request = CreateRequest(product, orderedCount);

                if ((product.Total - product.TotalReserved) < orderedCount)
                {
                    await Console.Out.WriteLineAsync("There was not enough quantity!");
                    return -1; // Return some indication of failure
                }
                else
                {
                    product.TotalReserved += orderedCount;
                    product.Total -= orderedCount;
                    request.ShoppingCartId = newCart.Id;
                    newCart.Requests.Add(request);

                    await DbContext.Requests.AddAsync(request);
                    await DbContext.ShoppingCarts.AddAsync(newCart);
                    await DbContext.SaveChangesAsync();

                    newCart.Requests = KeepAllRequestsInCollection(newCart.Id);
                    return newCart.Id; // Return the ID of the created cart
                }
            }
            catch
            {
                await Console.Out.WriteLineAsync("Invalid operation!");
                return -1; // Return some indication of failure
            }
        }

        public async Task<bool> Sync()
        {

            try
            {

				var carts = DbContext.ShoppingCarts.ToList();

				if (carts != null)
				{

					List<int> cartsToDelete = new();

					
					await Console.Out.WriteLineAsync(carts.Count + "");

					foreach (var cart in carts)
					{

						cart.Requests = KeepAllRequestsInCollection(cart.Id);

						if (cart.GeneratedWhen.AddMinutes(CartLifeLen) < DateTime.Now || cart.Requests.Any(x => x.Product == null))
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

            ShoppingCart cart = await DbContext.ShoppingCarts.FindAsync(cartId);
            cart.Requests = KeepAllRequestsInCollection(cartId);

            try
            {

                foreach (var request in cart.Requests)
                {
                    DbContext.Products.First(x => x.Id == request.ProductId).Total += request.OrderedCount;
                    DbContext.Products.First(x => x.Id == request.ProductId).TotalReserved -= request.OrderedCount;
                }
                DbContext.ShoppingCarts.Remove(cart);
                await DbContext.SaveChangesAsync();
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

			ShoppingCart cart = await DbContext.ShoppingCarts.FindAsync(cartId);
			cart.Requests = KeepAllRequestsInCollection(cartId);

			try
			{

				foreach (var request in cart.Requests)
				{
					DbContext.Products.First(x => x.Id == request.ProductId).Total += request.OrderedCount;
					DbContext.Products.First(x => x.Id == request.ProductId).TotalReserved -= request.OrderedCount;
				}
				DbContext.ShoppingCarts.Remove(cart);
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

                ShoppingCart cart = await DbContext.ShoppingCarts.FindAsync(cardId);

                if (cart != null)
                {
                    CartDisplayModel cartDisplayModel = new CartDisplayModel();
                    cartDisplayModel.Id = cardId;
                    cartDisplayModel.createdWhen = cart.GeneratedWhen;

                    List<ProductInCartModel> productsInCart = new List<ProductInCartModel>();

                    var requests = KeepAllRequestsInCollection(cardId);
                    await Console.Out.WriteLineAsync("Got all requests");
                    foreach (var request in requests)
                    {
                        var product = ProductServices.GetProductById(request.ProductId);
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
               
                var cart = DbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
                //
                cart.Requests = KeepAllRequestsInCollection(cartId);
                var request = cart.Requests.FirstOrDefault(x => x.Product.Name.Equals(productName));
                int productId = request.ProductId;
                var product = ProductServices.GetProductById(productId);
                if (request != null && cart != null)
                {
                    //var product = request.Product;
                    if (product.Total - product.TotalReserved >= newCount) //?
                    {
                        DbContext.Products.First(x => x == product).Total += request.OrderedCount - newCount;
                        DbContext.Products.First(x => x == product).TotalReserved += newCount - request.OrderedCount;
                        DbContext.Requests.First(x => x == request).OrderedCount = newCount;
                        DbContext.ShoppingCarts.First(x => x.Id == cartId).GeneratedWhen = DateTime.Now;
                        await DbContext.SaveChangesAsync();
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

				var cart = DbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);
                cart.Requests = KeepAllRequestsInCollection(cart.Id);
                Product product = ProductServices.GetProductByType(productName);
                if (cart != null)
                {
                    Requests request = cart.Requests.FirstOrDefault(x => x.Product.Name == productName);
                    cart.Requests.Remove(request);
                    DbContext.Requests.Remove(request);

                    DbContext.Products.First(x => x.Id == product.Id).TotalReserved = product.TotalReserved - request.OrderedCount;
                    DbContext.Products.First(x => x.Id == product.Id).Total = product.Total + request.OrderedCount;


                    await DbContext.SaveChangesAsync();
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
                var cart = DbContext.ShoppingCarts.FirstOrDefault(x => x.Id == cartId);

                Product product = ProductServices.GetProductByType(productName);
                if (cart != null)
                {
                    cart.Requests = KeepAllRequestsInCollection(cartId);
                    var request = new Requests();

                    request.OrderedCount = count;
                    request.ShoppingCartId = cartId;
                    request.ProductId = product.Id;
                    request.Product = product;

                    if (!cart.Requests.Any(x => x.ProductId == product.Id))
                    {
                        if (count <= product.Total - product.TotalReserved)
                        {
                            request.OrderedCount = count;
                            product.TotalReserved += count;
                            product.Total = product.Total - count;
                            cart.Requests.Add(request);
                            DbContext.ShoppingCarts.First(x => x.Id == cartId).GeneratedWhen = DateTime.Now;
                            await DbContext.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            await Console.Out.WriteLineAsync("Not enough quantity available for " + productName);
                            return false;
                        }
                    }
                    else
                    {
                        int foreignCount = KeepAllRequestsInCollection(cartId).Where(x => x.ProductId == request.ProductId).Sum(x => x.OrderedCount);
                        if (await ChangeProductCount(product.Name, request.OrderedCount + foreignCount, cart.Id))
                        {
                            await DbContext.SaveChangesAsync();
                            return true;
                        }
                        else
                        {
                            await Console.Out.WriteLineAsync("Failed to update product count for " + productName);
                            return false;
                        }
                    }
                }
                else
                {
                    await Console.Out.WriteLineAsync("Shopping cart not found with ID: " + cartId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync("Error adding product to cart: " + ex.Message);
                return false;
            }
        }


        public async Task<bool> MarkAsDone(int cartId)
        {
            try
            {
                var cart = await DbContext.ShoppingCarts.FindAsync(cartId);

                if (cart != null)
                {
                    // Изтриване на заявките, свързани с количката
                    foreach (var request in cart.Requests)
                    {
                        DbContext.Requests.Remove(request);
                    }

                    // Изтриване на количката
                    DbContext.ShoppingCarts.Remove(cart);

                    // Запазване на промените в базата данни
                    await DbContext.SaveChangesAsync();

                    return true;
                }
                else
                {
                    // Обработка на случая, когато количката не е намерена
                    await Console.Out.WriteLineAsync("Cart with ID " + cartId + " was not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Обработка на грешката и връщане на false
                await Console.Out.WriteLineAsync("Error while marking cart as done: " + ex.Message);
                return false;
            }
        }

    }
}
