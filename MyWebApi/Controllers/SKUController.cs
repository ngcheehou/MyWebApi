using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;

namespace MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class SKUController : Controller
    {

        private static List<SKU> _skuList = new List<SKU>
        {
            new SKU { SKUId = 1, SKUName = "Item1", SKUQuantity = 10, Price = 99.99 },
            new SKU { SKUId = 2, SKUName = "Item2", SKUQuantity = 5, Price = 49.99 }
        };

        [HttpGet]
        public IActionResult GetAll()
        {
           
            return Ok(_skuList);
        }

        // GET: api/sku/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var sku = _skuList.FirstOrDefault(s => s.SKUId == id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }
            return Ok(sku);
        }

        // POST: api/sku
        [HttpPost]
        public IActionResult Create([FromBody] SKU newSku)
        {
            if (newSku == null || string.IsNullOrWhiteSpace(newSku.SKUName))
            {
                return BadRequest("Invalid SKU data");
            }

            newSku.SKUId = _skuList.Count > 0 ? _skuList.Max(s => s.SKUId) + 1 : 1;
            _skuList.Add(newSku);
            return CreatedAtAction(nameof(GetById), new { id = newSku.SKUId }, newSku);
        }

        // PUT: api/sku/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SKU updatedSku)
        {
            var sku = _skuList.FirstOrDefault(s => s.SKUId == id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }

            if (updatedSku == null || string.IsNullOrWhiteSpace(updatedSku.SKUName))
            {
                return BadRequest("Invalid SKU data");
            }

            sku.SKUName = updatedSku.SKUName;
            sku.SKUQuantity = updatedSku.SKUQuantity;
            sku.Price = updatedSku.Price;

            return NoContent();
        }

        // DELETE: api/sku/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var sku = _skuList.FirstOrDefault(s => s.SKUId == id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }

            _skuList.Remove(sku);
            return NoContent();
        }


        //

        [HttpGet("GetAllSKU")]
        public IActionResult GetAllSKUSomeSpecialName()
        {
            return Ok(_skuList);
        }

        // GET: api/SKU/get/{id}
        [HttpGet("GetSKUById/{id}")]
        public IActionResult GetSKUById(int id)
        {
            var sku = _skuList.FirstOrDefault(s => s.SKUId == id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }
            return Ok(sku);
        }

        // POST: api/SKU/create
        [HttpPost("CreateSKU")]
        public IActionResult CreateSKU([FromBody] SKU newSku)
        {
            if (newSku == null || string.IsNullOrWhiteSpace(newSku.SKUName))
            {
                return BadRequest("Invalid SKU data");
            }

            newSku.SKUId = _skuList.Count > 0 ? _skuList.Max(s => s.SKUId) + 1 : 1;
            _skuList.Add(newSku);
            return CreatedAtAction(nameof(GetById), new { id = newSku.SKUId }, newSku);
        }

        // PUT: api/SKU/update/{id}
        [HttpPut("UpdateSKU/{id}")]
        public IActionResult UpdateSKU(int id, [FromBody] SKU updatedSku)
        {
            var sku = _skuList.FirstOrDefault(s => s.SKUId == id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }

            if (updatedSku == null || string.IsNullOrWhiteSpace(updatedSku.SKUName))
            {
                return BadRequest("Invalid SKU data");
            }

            sku.SKUName = updatedSku.SKUName;
            sku.SKUQuantity = updatedSku.SKUQuantity;
            sku.Price = updatedSku.Price;

            return NoContent();
        }

        // DELETE: api/SKU/delete/{id}
        [HttpDelete("DeleteSKU/{id}")]
        public IActionResult DeleteSKU(int id)
        {
            var sku = _skuList.FirstOrDefault(s => s.SKUId == id);
            if (sku == null)
            {
                return NotFound("SKU not found");
            }

            _skuList.Remove(sku);
            return NoContent();
        }
    }
}
