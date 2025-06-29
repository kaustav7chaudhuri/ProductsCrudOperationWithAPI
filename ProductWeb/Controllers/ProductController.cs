using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductWeb.Models;
using ProductWeb.Services;
using System.Reflection;

namespace ProductWeb.Controllers
{
    public class ProductController : Controller
    {
        #region Services
        ProductWebService service = new ProductWebService();
        CategoryService categoryService = new CategoryService();
        ManufactureService manufactureService = new ManufactureService();
        #endregion

        public IActionResult Index()
        {
            IEnumerable<WebProduct> products = (IEnumerable<WebProduct>)service.GetAllProducts().GetAwaiter().GetResult();
            var categories = categoryService.GetAllCategories().GetAwaiter().GetResult();
            var manufacturers = manufactureService.GetAllManufacturers().GetAwaiter().GetResult();

            products = products.Select(p =>
            {
                int categoryId = int.TryParse(p.Category, out int cid) ? cid : 0;
                var matchedCategory = categories.FirstOrDefault(c => c.ProductCategoryId == categoryId);
                p.Category = matchedCategory?.ProductCategoryName ?? "";

                int manufacturerId = int.TryParse(p.Manufacturer, out int mid) ? mid : 0;
                var matchedManufacturer = manufacturers.FirstOrDefault(m => m.ManufacturerId == manufacturerId);
                p.Manufacturer = matchedManufacturer?.ManufacturerName ?? "";

                return p;
            }).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            GetCategories(0);
            GetManufacturers(0);
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(WebProduct products)
        {
            var IsExistProduct = service.GetProductById(products.ProductId).GetAwaiter().GetResult();
            if (IsExistProduct is not null)
            {
                ViewBag.ErrorMessage = "Product already exists with this ID.";
            }
            else
            {
                products.CreatedDate = DateTime.Now;
                bool isInserted = service.InsertProduct(products).GetAwaiter().GetResult();
                if (isInserted)
                {
                    TempData["CreateSuccess"] = "Product Saved Successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to insert product.";
                }
            }
            return View(products);
        }

        [NonAction]
        public void GetCategories(int id)
        {
            var categories = categoryService.GetAllCategories().GetAwaiter().GetResult();
            ViewBag.CategoriesList = new SelectList(categories, "ProductCategoryId", "ProductCategoryName", id);
        }
        [NonAction]
        public void GetManufacturers(int id)
        {
            var manufacturers = manufactureService.GetAllManufacturers().GetAwaiter().GetResult();
           ViewBag.ManufacturersList = new SelectList(manufacturers, "ManufacturerId", "ManufacturerName", id);
        }

        [HttpGet]
        public IActionResult UpdateProduct(string str_PRODUCTID)
        {
            
            var IsExistProduct = service.GetProductById(Convert.ToInt32(str_PRODUCTID)).GetAwaiter().GetResult();
            if (IsExistProduct is null)
            {
                ViewBag.UpdateError = $"Cannot Find Product with Id {str_PRODUCTID}";
            }
            GetCategories(Convert.ToInt32(IsExistProduct.Category));
            GetManufacturers(Convert.ToInt32(IsExistProduct.Manufacturer));
            
            return View(IsExistProduct);
        }

        [HttpPost]
        public IActionResult UpdateProduct(WebProduct product)
        {
            product.ModifiedDate = DateTime.Now;
            bool isUpdated = service.UpdateProduct(product, product.ProductId).GetAwaiter().GetResult();
            if (isUpdated)
            {
                TempData["UpdateSuccess"] = "Product update successfully";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to update product.";
            }

            return View(product);
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var isExistProduct = service.GetProductById(id).GetAwaiter().GetResult();
            if (isExistProduct is null)
            {
                ViewBag.ErrorMessage = "Failed to Delete product.";
                return View();
            }
            bool IsDelete = service.DeleteProduct(id).GetAwaiter().GetResult();
            if (IsDelete)
            {
                TempData["DeleteProduct"] = "Product Deleted";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                TempData["DeleteProductFail"] = "Product Deleted Failed";
                return RedirectToAction("UpdateProduct", "Product");
            }
        }
    }
}
