using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.ApiDbContextFile;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly InterviewDbContext db;
        public ProductController(InterviewDbContext _db)
        {
            this.db = _db;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var ProductList = db.Products.ToList();
            return Ok(ProductList);
        }
        [HttpGet]
        [Route("GetAllProductById/{id}")]
        public IActionResult GetProductById([FromRoute] int id)
        {
            var Product = db.Products.FirstOrDefault(p => p.ProductId == id);
            if(Product is not null)
            {
                return Ok(Product);
            }
            return NotFound($"Product with id {id} not found");
        }
        [HttpPost]
        [Route("CreateProduct")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            if (ModelState.IsValid && product is not null)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return Ok("Product Created Successfully");
            }
            return BadRequest("Invalid Product Data or Product Already Exists");
        }

        [HttpPut]
        [Route("UpdateProduct/{id}")]
        public IActionResult UpdateProduct([FromRoute] int id, [FromBody] Product product)
        {
            var ExistProduct = db.Products.FirstOrDefault(p => p.ProductId == id);
            if (ExistProduct is not null)
            {
                ExistProduct.ProductName = product.ProductName != null ? product.ProductName : ExistProduct.ProductName;
                ExistProduct.Description = product.Description != null ? product.Description : ExistProduct.Description;
                ExistProduct.Price = product.Price != 0 ? product.Price : ExistProduct.Price;
                ExistProduct.StockQnty = product.StockQnty != 0 ? product.StockQnty : ExistProduct.StockQnty;
                ExistProduct.Category = product.Category != null ? product.Category : ExistProduct.Category;
                ExistProduct.Manufacturer = product.Manufacturer != null ? product.Manufacturer : ExistProduct.Manufacturer;
                ExistProduct.ProductAvailability = product.ProductAvailability != null ? product.ProductAvailability : ExistProduct.ProductAvailability;
                ExistProduct.IsOnSale = product.IsOnSale;
                ExistProduct.IsNewArrival = product.IsNewArrival;
                ExistProduct.CreatedDate = product.CreatedDate != null ? product.CreatedDate : ExistProduct.CreatedDate;
                ExistProduct.ModifiedDate = product.ModifiedDate != null ? product.ModifiedDate : ExistProduct.ModifiedDate;

                db.SaveChanges();
                return Ok("Product Updated Successfully");
            }
            return BadRequest($"Cannot find product with id {id}");
        }

        [HttpDelete]
        [Route("DeleteProduct/{id}")]
        public IActionResult DeleteProduct([FromRoute] int id)
        {
            var ExistProduct = db.Products.FirstOrDefault(p => p.ProductId == id);
            if (ExistProduct is not null)
            {
                db.Products.Remove(ExistProduct);
                db.SaveChanges();
                return Ok("Product Deleted Successfully");
            }
            return BadRequest($"Cannot find product with id {id}");
        }
    }
}
