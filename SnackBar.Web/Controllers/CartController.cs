using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SnackBar.Core.Models;
using SnackBar.Core.Services;
using SnackBar.Infrastructure.Data.Entities;
using SnackBar.Web.Data;
using System.CodeDom;

namespace SnackBar.Web.Controllers
{
	public class CartController : Controller
	{


		private const string HomePage = "/Home/Index";
		private const string CartPage = "/Cart/Cart";
		private const string CartInSession = "cart";
		private const string NoCartPage = "/Cart/NoCartInSession";
		

		private readonly ILogger<CartController> _logger;
		private readonly ProductServices productServices;
		private readonly AdminServices adminServices;
		private readonly ShoppingCartServices cartServices;
		public CartController(ILogger<CartController> logger, SnackBarDbContext context)
		{
			_logger = logger;
			productServices = new ProductServices(context);
			adminServices = new AdminServices(context);
			cartServices = new ShoppingCartServices(context);
		}


		public async Task<IActionResult> Cart(string cartId)
		{
			ISession session = HttpContext.Session;

			cartId = session.GetString(CartInSession);

			await cartServices.Sync();

			try
			{
                await Console.Out.WriteLineAsync("Cart page loading wtih ID: " + cartId);

				//get cartData from db
				var cartData = await cartServices.LoadCart(int.Parse(cartId));

				if(cartData == null)
				{
					session.Remove(CartInSession);

					Redirect(HomePage);
				}

                await Console.Out.WriteLineAsync(cartData.ToString());

				if(cartData.products.Count <= 0)
				{
					session.Remove(CartInSession);
					return Redirect(HomePage);
				}

                return View(cartData);

            }
			catch (Exception ex)
			{
                await Console.Out.WriteLineAsync("Cart couldn't load");
                _logger.LogError(ex.Message);
				return Redirect(HomePage);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CartRedirectTry(string cartId)
		{		

			if (cartId == null || cartId.Trim().Length == 0)
			{
                await Console.Out.WriteLineAsync("Nothing was passed to Redirect");
                return Redirect(HomePage);
			}

			bool cartExists = (await cartServices.LoadCart(int.Parse(cartId)) != null);

			if (cartExists)
			{
                await Console.Out.WriteLineAsync("Cart with ID " + cartId + "exists");
                HttpContext.Session.SetString(CartInSession, cartId);
				return RedirectToAction("Cart", new{cartId = cartId});
			}

            await Console.Out.WriteLineAsync("Cart with ID " + cartId + "doesnt exist");
            return Redirect(HomePage) ;
		}

		[HttpPost]
		public async Task<IActionResult> IsCartInSession()
		{
            ISession session = HttpContext.Session;

            await Console.Out.WriteLineAsync("cart in session?: " + session.Keys.Contains(CartInSession));

			await cartServices.Sync();

			if (session.Keys.Contains(CartInSession))
            {
                await Console.Out.WriteLineAsync("Redirecting to CartPage");
				return Json(new { show_popup = false });
			}
			else
			{
                return Json(new { show_popup = true });
            }

			
        }


		[HttpPost]
		public async Task<IActionResult> LoadCart(string cartId)
		{

			ISession session = HttpContext.Session;
			//gets if cart with id cartId exists from database
			bool cartExists = true;

			await cartServices.Sync();

			if (cartExists)
			{
				session.SetString(CartInSession, cartId);
			}
			else
			{
				return Json(new { exists = false });
			}

			//Directs to cart page
			return RedirectToAction(CartPage);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteCart()
		{

			await cartServices.Sync();

			ISession session = HttpContext.Session;

			if (session.Keys.Contains(CartInSession))
			{
				string? cartId = session.GetString(CartInSession);
				//Send request for cart to be deleted to database
				await cartServices.DeleteCart(int.Parse(cartId));

				session.Remove(CartInSession);
			}

			//redirect to home
			return Redirect(HomePage);
		}

		[HttpPost]
		public async Task<IActionResult> MarkCartAsDone()
		{

			await cartServices.Sync();

			ISession session = HttpContext.Session;

			if (session.Keys.Contains(CartInSession))
			{
				string? cartId = session.GetString(CartInSession);

				//Send request for cart to be marked as done to database
				await cartServices.MarkAsDone(int.Parse(cartId));

				session.Remove(CartInSession);
			}

			//redirect to home
			return Redirect(HomePage);
		}

		[HttpPost]
		public async Task<IActionResult> ChangeProductCount(string productName, int Count)
		{
			await cartServices.Sync();

			ISession session = HttpContext.Session;

			if (session.Keys.Contains(CartInSession))
			{
				string? cartId = session.GetString(CartInSession);
                //send request to db to change product count

                await Console.Out.WriteLineAsync("Changing " + productName + " to " + Count + " on cart: " + cartId);

                await cartServices.ChangeProductCount(productName, Count, int.Parse(cartId));
			}

			return Redirect(CartPage);
		}

		[HttpPost]
		public async Task<IActionResult> RemoveProduct(string productName)
		{
			await cartServices.Sync();

			ISession session = HttpContext.Session;

			if (session.Keys.Contains(CartInSession))
			{
				string? cartId = session.GetString(CartInSession);
                //call db function to remove productName from cart with cartId id

                await Console.Out.WriteLineAsync("Removing product with name " + productName + " from cart: " + cartId);

                
                await Console.Out.WriteLineAsync(await cartServices.RemoveProduct(productName, int.Parse(cartId)) ? "true" : "false");
            }

			return Redirect(CartPage);
		}

		//serves purpose of new cart
		[HttpPost]
		public async Task<IActionResult> AddProduct(string Name, int Count)
		{
			await cartServices.Sync();

			string productName = Name;
			int productCount = Count;

			await Console.Out.WriteLineAsync("Request for add product: " + Name + " " + Count);

			ISession session = HttpContext.Session;

			if (session.Keys.Contains(CartInSession))
			{
				string? cartId = session.GetString(CartInSession);

				//send request to databse to create new product in cart
				await cartServices.AddProduct(productName, productCount, int.Parse(cartId));
			}
			else
			{
				//send request to db to create new cart with this product and this count in it
				int cartId = (int)await cartServices.CreateCart(productName, productCount);

				await Console.Out.WriteLineAsync("New cart id: " + cartId);
				//creates new cart if trying to add product but no cart is present in session with cart id got from service
				session.SetString(CartInSession, cartId.ToString());

			}

			return Redirect(HomePage);

		}
	}
}
