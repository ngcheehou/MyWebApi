using Microsoft.AspNetCore.Mvc;
using MyWebApi.Database;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]  // This can support multiple versions
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SKUWithVersioningController : Controller
    {
        private readonly AppDbContext _context;

        public SKUWithVersioningController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("GetAllSKU")]
        public IActionResult GetAllSKU()
        {
            return Ok(_context.SKUs.ToList());
        }

        // GET: api/SKU/get/{id}
        [HttpGet("GetSKUById/{id}")]
        public IActionResult GetSKUById(int id)
        {
            var sku = _context.SKUs.Find(id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }
            return Ok(sku);
        }


       
        [HttpPost("CreateSKU")]
        [MapToApiVersion("1.0")]  // This method will be mapped to version 1.0
        public IActionResult CreateSKU_v1([FromBody] SKU newSku)
        {
            
            if (newSku == null || string.IsNullOrWhiteSpace(newSku.SKUName))
            {
                return BadRequest("Invalid SKU data");
            }

            _context.SKUs.Add(newSku);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSKUById), new { id = newSku.SKUId }, newSku);
        }


        [HttpPost("CreateSKU")]
        [MapToApiVersion("2.0")]  // This method will be mapped to version 2.0
        public IActionResult CreateSKU_v2([FromBody] SKU newSku)
        {
            // assign new feature here

            newSku.NewFeature = "This has a new feature!";
            if (newSku == null || string.IsNullOrWhiteSpace(newSku.SKUName))
            {
                return BadRequest("Invalid SKU data");
            }


            _context.SKUs.Add(newSku);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSKUById), new { id = newSku.SKUId }, newSku);
        }

        // PUT: api/SKU/update/{id}
        [HttpPut("UpdateSKU/{id}")]
        public IActionResult UpdateSKU(int id, [FromBody] SKU updatedSku)
        {

            var existingSku = _context.SKUs.Find(id);
            if (existingSku == null) return NotFound();

            existingSku.SKUName = updatedSku.SKUName;
            existingSku.SKUQuantity = updatedSku.SKUQuantity;
            existingSku.Price = updatedSku.Price;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE: api/SKU/delete/{id}
        [HttpDelete("DeleteSKU/{id}")]
        public IActionResult DeleteSKU(int id)
        {
            var sku = _context.SKUs.Find(id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }

            _context.SKUs.Remove(sku);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
