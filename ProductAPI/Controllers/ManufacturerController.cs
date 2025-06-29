using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.ApiDbContextFile;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        public readonly InterviewDbContext db;
        public ManufacturerController(InterviewDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetAllManufacturer")]
        public IActionResult GetManufacturers()
        {
            var manufacturers = db.ProductManufacturers.ToList();
            if (manufacturers == null || manufacturers.Count == 0)
            {
                return NotFound("No manufacturers found.");
            }
            return Ok(manufacturers);
        }
        [HttpGet]
        [Route("GetAllManufacturerById/{id}")]
        public IActionResult GetManufacturerById(int id)
        {
            var manufacturer = db.ProductManufacturers.Find(id);
            //var manufacturer = db.ProductManufacturers.FirstOrDefault(m => m.ManufacturerId == id);
            if (manufacturer == null)
            {
                return NotFound($"Manufacturer with ID {id} not found.");
            }
            return Ok(manufacturer);
        }
        [HttpPost]
        [Route("InsertManufacturer")]
        public IActionResult AddManufacturer([FromBody] ProductManufacturer manufacturer)
        {
            if (ModelState.IsValid && manufacturer is not null)
            {
                db.ProductManufacturers.Add(manufacturer);
                db.SaveChanges();
                return Ok("Manufacturer added successfully");
            }
            return BadRequest("Manufacturer data is null.");
        }
        [HttpPut]
        [Route("UpdateManufacturer/{id}")]
        public IActionResult UpdateManufacturer([FromRoute] int id, [FromBody] ProductManufacturer manufacturer)
        {
            if (db.ProductManufacturers.Find(id) == null)
            {
                return BadRequest("Manufacturer ID mismatch.");
            }
            if (ModelState.IsValid && manufacturer is not null)
            {
                var toUpdateManuFacturer = db.ProductManufacturers.FirstOrDefault(m => m.ManufacturerId == id);

                toUpdateManuFacturer.ManufacturerName = manufacturer.ManufacturerName != null ? manufacturer.ManufacturerName : "";
                db.SaveChanges();
                return Ok("Manufacturer updated successfully");
            }
            return BadRequest("Invalid manufacturer data.");
        }
        [HttpDelete]
        [Route("DeleteManufacturer/{id}")]
        public IActionResult DeleteManufacturer(int id)
        {
            var manufacturer = db.ProductManufacturers.Find(id);
            if (manufacturer == null)
            {
                return NotFound($"Manufacturer with ID {id} not found.");
            }
            db.ProductManufacturers.Remove(manufacturer);
            db.SaveChanges();
            return Ok("Manufacturer deleted successfully");
        }
    }
}
