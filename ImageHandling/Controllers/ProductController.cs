using ImageHandling.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageHandling.Controllers
{
    
    public class ProductController : Controller
    {
        private readonly ImageDbContext context;
        private readonly IWebHostEnvironment env;
        public ProductController(ImageDbContext context , IWebHostEnvironment env)
        {
            this.context = context;
            this.env = env;
        }
        public IActionResult Index()
        {
            return View(context.Products.ToList());
        }


        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductViewModel prod)
        {
            string fileName = "";
            if(prod.photo != null)
            {

                var ext = Path.GetExtension(prod.photo.FileName);
                var size = prod.photo.Length;

                if (ext.Equals(".png") || ext.Equals(".jpg") || ext.Equals(".jpeg"))
                {
                    if (size <= 1000000)
                    {
                        string folder = Path.Combine(env.WebRootPath, "images");
                        fileName = Guid.NewGuid().ToString() + "_" + prod.photo.FileName;
                        string filePath = Path.Combine(folder, fileName);
                        prod.photo.CopyTo(new FileStream(filePath, FileMode.Create));

                        Product p = new Product()
                        {
                            Name = prod.Name,
                            Price = prod.Price,
                            ImagePath = fileName,
                        };

                        context.Products.Add(p);
                        context.SaveChanges();
                        TempData["success"] = "Product Added...!!";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Size_Error"] = "Image must be less than or equal 1 MB";
                    }
                }
                else
                {
                    TempData["Ext_Error"] = "Only PNG, JPG, JPEG images allowed";
                }
            }
            return View();
        }
    }
}
