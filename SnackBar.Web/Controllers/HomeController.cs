using Microsoft.AspNetCore.Mvc;
using SnackBar.Core.Models;
using SnackBar.Web.Models;
using System.Diagnostics;
using SnackBar.Core.Models;
using SnackBar.Core.Services;
using SnackBar.Web.Data;
using System.CodeDom;
using SnackBar.Infrastructure.Data.Entities;

namespace SnackBar.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ProductServices productServices;
        private readonly AdminServices adminServices;

        private const string HomePage = "Index";
        private const string CartPage = "Cart";
        private const string CartInSession = "cart";
        private const string adminInSession = "administrator";
        private const string adminPage = "/Admin/adminHomePage";

        public HomeController(ILogger<HomeController> logger, SnackBarDbContext context)
        {
            _logger = logger;
            productServices = new ProductServices(context);
            adminServices = new AdminServices(context);
        }


        [HttpPost]
        public IActionResult ShowType([FromBody] DemoModel product)
        {
            Console.WriteLine("Post: ");
            Console.WriteLine(product.itemType);

            return Json(new { success = true });
        }

        
        public async Task<IActionResult> Index(string filter)
        {
            
            List<Product> ProductData = new();

            if (filter == null || filter.Trim().Length == 0)
            {
                await Console.Out.WriteLineAsync("Request for all data");
                await Console.Out.WriteLineAsync("Is administrator: " + HttpContext.Session.Keys.Contains(adminInSession));
                ProductData = await productServices.GetAllAsync();
            }
            else
            {

                Product product = productServices.GetProductByType(filter);

                await Console.Out.WriteLineAsync("Request for searched product");

                if (product == null)
                {
                    await Console.Out.WriteLineAsync("No product: " + filter);
                }
                else
                {
                    await Console.Out.WriteLineAsync("Product name: " + filter);
                    ProductData.Add(productServices.GetProductByType(filter));
                }
                
            }
            

            return View(new IndexDataModel { products = ProductData });
        }      
          
        [HttpPost]
        
        public async Task<IActionResult> TryAdminLogin(StringModel keyAttempt)
        {
            await Console.Out.WriteLineAsync("Trying to Log in as admin with key: " + keyAttempt.value);

            ISession session = HttpContext.Session;

            bool isValidKey;

            try
            {                
                isValidKey = await adminServices.IsKeyValid(keyAttempt.value);

                if (isValidKey)
                {
                    
                    session.SetString(adminInSession, keyAttempt.value);
                    await Console.Out.WriteLineAsync("VALIDATION SUCESS");
                    return Redirect(adminPage);
                }
                
            }catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return Redirect(HomePage);
        }

        //Cart
        
        

        /*public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}