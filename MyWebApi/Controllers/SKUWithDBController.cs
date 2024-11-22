using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebApi.Database;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SKUWithDBController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SKUWithDBController> _logger;

        public SKUWithDBController(AppDbContext context, ILogger<SKUWithDBController> logger)
        {
            _context = context;
            _logger = logger;
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
            _logger.LogInformation("Successfully fetched data.");

            var sku = _context.SKUs.Find(id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }
            return Ok(sku);
        }

        /// <summary>
        /// Create a SKU by providing name , quantity and price
        /// </summary>
        /// <param name="newSku"></param>
        /// <returns>Item that created</returns>
        // POST: api/SKU/create
        [HttpPost("CreateSKU")]
        public IActionResult CreateSKU([FromBody] SKU newSku)
        {
            // assign new feature here
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
           
            var existingSku =   _context.SKUs.Find(id);
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

        [HttpGet("GetError1")]
        public IActionResult GetError1()
        {
           
            throw new KeyNotFoundException("SKU not found!");
             
        }

        [HttpGet("GetError2")]
        public IActionResult GetError2()
        {
            throw new MyCustomException("My Custom Exception!");

        }

        [HttpGet("GetError3")]
        public IActionResult GetError3()
        {
            return NotFound(new APIResponse<object>
            {
                Success = false,
                Message = "This is customize error response",
                Data = null
            });

        }

        [Authorize]
        [HttpGet("Secure")]
        public IActionResult GetSecureData()
        {
            return Ok("This is a secure endpoint.");
        }


    }
}
