using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.ApiDbContextFile;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        public readonly InterviewDbContext db;
        public CategoryController(InterviewDbContext _db)
        {
            this.db = _db;
        }

        [HttpGet]
        [Route("GetAllCategory")]
        public IActionResult GetAllCategories()
        {
            var categories = db.ProductCategories.ToList();
            return Ok(categories);
        }
        [HttpGet]
        [Route("GetAllCategoryById/{id}")]
        public IActionResult GetCategory(int id)
        {
            var category = db.ProductCategories.Find(id);
            if (category is not null)
            {
                return Ok(category);
            }
            return NotFound($"Category with ID {id} not found.");
        }

        [HttpPost]
        [Route("InsertCategory")]
        public IActionResult CreateCategory([FromBody]ProductCategory category)
        {
            var ExistCategory = db.ProductCategories.FirstOrDefault(p => p.ProductCategoryName == category.ProductCategoryName);
            if(ModelState.IsValid && ExistCategory is null)
            {
                db.ProductCategories.Add(category);
                db.SaveChanges();
                return Ok("Category Created Successfully");
            }
            return BadRequest("Invalid Category Data or Category Already Exists");
        }
        [HttpPut]
        [Route("UpdateCategory/{id}")]
        public IActionResult UpdateCategory([FromRoute]int id, [FromBody] ProductCategory category)
        {
            var ExistCategory = db.ProductCategories.FirstOrDefault(p => p.ProductCategoryId == id);
            if(ExistCategory is not null)
            {
                ExistCategory.ProductCategoryName = category.ProductCategoryName;
                db.SaveChanges();
                return Ok("Category Updated Successfully");
            }
            return BadRequest($"Cannot find category with id {id}");
        }
        [HttpDelete]
        [Route("DeleteCategory/{id}")]
        public IActionResult DeleteCategory([FromRoute] int id)
        {
            var category = db.ProductCategories.Find(id);
            if(category is not null)
            {
                db.ProductCategories.Remove(category);
                db.SaveChanges();
                return Ok("Category Deleted Successfully");
            }
            return BadRequest($"Cannot find category with id {id}");
        }
    }
}
