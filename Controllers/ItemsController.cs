using ExportTxt.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExportTxt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        // Sample data
        private static List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Item1" },
            new Item { Id = 2, Name = "Item2" },
            new Item { Id = 3, Name = "Item3" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return Ok(items);
        }

        [HttpGet("export")]
        public IActionResult ExportToTxt()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Items.txt");

            using (var writer = new StreamWriter(filePath))
            {
                foreach (var item in items)
                {
                    writer.WriteLine($"Id: {item.Id}, Name: {item.Name}");
                }
            }

            var bytes = System.IO.File.ReadAllBytes(filePath);
            return File(bytes, "text/plain", "Items.txt");
        }
    }
}

