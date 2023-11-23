using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SDC_ALTEN_API.DTO;
using SDC_ALTEN_API.Model;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SDC_ALTEN_API.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {


        [HttpGet]
        public IActionResult getAllProducts()
        {
            try
            {
                List<Product> productList = GetProductsFromJsonFile();
                return Ok(productList);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occured while fetching the data");
            }
        }

        [HttpPost("create")]
        public IActionResult CreateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Model State is invalid");
            }

            if (product == null)
            {
                return BadRequest("Product is null");
            }

            try
            {

                List<Product> productList = GetProductsFromJsonFile();
                productList.Add(product);
                UpdateJsonFile(productList);

                return Ok("Product Successfully created.");

            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetProductDetails(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id is null or invalid.");
            }

            try
            {
                var productList = GetProductsFromJsonFile();
                var product = GetProductById(productList, id);
                if (product == null)
                {
                    return NotFound($"There is no product with the id : {id}");
                }

                return Ok(product);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error ocured while retrieving the product details");
            }
        }

        [HttpPatch("update/{id}")]
        public IActionResult UpdateProduct(int id, ProductDTO productDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("Model State is invalid");
            }

            if (id <= 0 || productDTO == null)
            {
                return BadRequest("Id or product is null or invalid.");
            }

            try
            {
                var productList = GetProductsFromJsonFile();
                var productToUpdate = GetProductById(productList, id);
                if (productToUpdate == null)
                {
                    return NotFound($"There is no product with the id : {id}");
                }

                productToUpdate.code = productDTO.code;
                productToUpdate.name = productDTO.name;
                productToUpdate.description = productDTO.description;
                productToUpdate.price = productDTO.price;
                productToUpdate.quantity = productDTO.quantity;
                productToUpdate.inventoryStatus = productDTO.inventoryStatus;
                productToUpdate.category = productDTO.category;
                productToUpdate.image = productDTO.image;
                productToUpdate.rating = productDTO.rating;

                UpdateJsonFile(productList);

                return Ok("Product Successfully updated.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error ocured while updating the product details");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id or product is null or invalid.");
            }

            try
            {
                var productList = GetProductsFromJsonFile();
                var productToDelete = GetProductById(productList, id);

                if (productToDelete == null)
                {
                    return NotFound($"There is no product with the id : {id}");
                }

                productList.Remove(productToDelete);
                UpdateJsonFile(productList);

                return Ok("Product Successfully deleted.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error ocured while deleting the product details");
            }

        }

        private List<Product> GetProductsFromJsonFile()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "product.json");
            string jsonContent = System.IO.File.ReadAllText(filePath);

            var result = JsonConvert.DeserializeObject<JsonData>(jsonContent);

            return result.Data == null ? new List<Product>() : result.Data;
        }

        private Product GetProductById(List<Product> productList, int id)
        {
            return productList.FirstOrDefault(i => i.Id == id);
        }

        private void UpdateJsonFile(List<Product> products)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "product.json");

            var jsonData = new JsonData { Data = products };
            string updatedJsonContent = JsonConvert.SerializeObject(jsonData, Formatting.Indented);
            System.IO.File.WriteAllText(filePath, updatedJsonContent);
        }


    }
}