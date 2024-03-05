using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SnackBar.Core.Models;
using SnackBar.Core.Services;
using SnackBar.Infrastructure.Data.Entities;
using SnackBar.Web.Data;

namespace SnackBar.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        private readonly ProductServices productService;
        private readonly AdminServices adminServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(ILogger<AdminController> logger, SnackBarDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            productService = new ProductServices(context);
            adminServices = new AdminServices(context);
            _webHostEnvironment = webHostEnvironment;
        }

        private const string homePage = "/Home/Index";
        private const string adminPage = "/Admin/adminHomePage";
        private const string AdminInSession = "administrator";
        private const string ImagesPath = "/Images/ProductImages";


        private bool IsAdmin()
        {
            ISession session = HttpContext.Session;
            Console.WriteLine("IsAdmin?: " + session.Keys.Contains(AdminInSession));
            return true;
        }

        //Populates whole page with all products

        public IActionResult AddEdit(string productToEdit)
        {
			if (!IsAdmin())
				return Redirect(homePage);

			if (productToEdit == null || productToEdit.Trim().Length == 0) {
                return View();
            }
            
            Product toEdit = productService.GetProductByType(productToEdit);
            return View(toEdit);            
        }
        public async Task<IActionResult> adminHomePage()
        {
			if (!IsAdmin())
				return Redirect(homePage);

            var allProducts = await productService.GetAllAsync();
            if (allProducts != null)
                return View(new IndexDataModel { products = allProducts });
            else
                return View(new IndexDataModel ());
        }

        public IActionResult Index()
        {

            
            if (!IsAdmin())
                return Redirect(homePage);

			Console.WriteLine("HELLO ADMIN");

			var product = productService.GetProductByType("Snickers");
            return View(product);
        }

        [HttpPost]
        public IActionResult Logout()
        {
			if (!IsAdmin())
				return Redirect(homePage);

			ISession session = HttpContext.Session;

            session.Remove(AdminInSession);

            return Redirect(homePage);
        }

        [HttpPost]

        public async Task<IActionResult> CreateAdminKey()
        {
            if (!IsAdmin())
                return Redirect(homePage);

            //One liner to create a 8-long admin key
            string newAdminKey = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

            //inserts key into database
            await adminServices.AddNewKey(newAdminKey);

            //returns key to be displayed - Will be in a pop-up
            return Json(new { key = newAdminKey });
        }



        [HttpPost]
        public async Task<IActionResult> EditProduct(String Name, int Count, decimal Price, IFormFile imageFile, String Description)
        {
            await Console.Out.WriteLineAsync("Requests to Edit product");

            if (!IsAdmin())
                return Redirect(homePage);

            try
            {

                await Console.Out.WriteLineAsync("Requests to edit product " + Name + " " + Count + " " + Price + " " + Description + " " + (imageFile != null));

                Product product = new Product
                {
                    Name = Name,
                    Total = Count,
                    Price = Price,
                    Description = Description

                };

                var productBeforeEdit = productService.GetProductByType(Name);

                if (imageFile == null)
                {
                    product.Image = productBeforeEdit.Image;
                }
                else
                {                    
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        string folder = "/Images/ProductImages/";
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";

                        var filePath = Path.Combine(_webHostEnvironment.WebRootPath + folder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        var oldImage = productBeforeEdit.Image;
						var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath + folder, oldImage);

						try
						{
							// Check if file exists with its full path
							if (System.IO.File.Exists(oldImagePath))
							{
								// If file found, delete it
								System.IO.File.Delete(Path.Combine(oldImagePath));
								Console.WriteLine("Old image deleted.");
							}
							else Console.WriteLine("File not found");
						}
						catch (IOException ioExp)
						{
							Console.WriteLine(ioExp.Message);
						}

						product.Image = fileName;
                    }
                }

                if(Description == null || Description.Trim().Length == 0)
                {
                    product.Description = productBeforeEdit.Description;
                }

                await productService.EditAsync(product);

                return Redirect(adminPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json(new { result = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProduct(String Name, int Count, IFormFile imageFile, decimal Price, String Description)
        {

            await Console.Out.WriteLineAsync("request to create new product");

            if (!IsAdmin())
                return Redirect(homePage);

            Product product = null;

            try
            {
                string path = null;
                if (imageFile != null && imageFile.Length > 0)
                {
                    string folder = "/Images/ProductImages/";
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";

                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath + folder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(fileStream);
                    }


                    path = fileName;
                }

                product = new()
                {
                    Name = Name,
                    Total = Count,
                    Price = Price,
                    Image = path,
                    Description = Description,
                    TotalReserved = 0
                };


                await Console.Out.WriteLineAsync("request for new product: " + product.ToString());

                await productService.AddAsync(product);

                return Redirect(adminPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Redirect(adminPage);
            }

        }


        [HttpPost]

        public async Task<IActionResult> DeleteProduct(string productName)
        {

            await Console.Out.WriteLineAsync("Requests to delete product: " + productName);

            if (!IsAdmin())
                return Redirect(homePage);

            try
            {
                Console.WriteLine("Requests to delete product: " + productName);

                var productImage = productService.GetProductByType(productName).Image;

                await productService.DeleteAsync(productName); //trust its not

                var oldImagePath = Path.Combine(_webHostEnvironment + ImagesPath, productImage);

				try
				{
					// Check if file exists with its full path
					if (System.IO.File.Exists(oldImagePath))
					{
						// If file found, delete it
						System.IO.File.Delete(Path.Combine(oldImagePath));
						Console.WriteLine("Old image deleted.");
					}
					else Console.WriteLine("File not found");
				}
				catch (IOException ioExp)
				{
					Console.WriteLine(ioExp.Message);
				}

				return Redirect(adminPage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return Json(new { result = false });
            }

        }

        [HttpPost]

        public IActionResult LoadProduct()
        {
            var product = productService.GetProductByType("Snickers");

            return View(product);
        }
    }
}
