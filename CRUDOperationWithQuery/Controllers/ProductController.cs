using CRUDOperationWithQuery.Models;
using CRUDOperationWithQuery.Service;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace CRUDOperationWithQuery.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;

        public ProductController(IConfiguration configuration, IProductService productService)
        {
            _productService = productService;
        }
        //Add Product View Controller
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View(new Product());
        }

        //Adding product in DB Controller
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product product)
        {

            if (ModelState.IsValid)
            {
                _productService.AddProduct(product);
                return RedirectToAction("GetAllProducts");
            }
            return View();
        }

        //Getting all products from DB Controller
        public IActionResult GetAllProducts()
        {
            var listOfProducts = _productService.GetAllProducts();
            return View(listOfProducts);
        }

        //Update Product View Controller
        [HttpGet]
        public IActionResult UpdateProduct(int id)
        {
            var product = _productService.GetProductById(id);
            if(product == null)
            {
                return BadRequest();
            }
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction("GetAllProducts");
            }
            return View();

        }

        [HttpGet]
        public IActionResult DeleteProduct(Product product)
        {
            var deleteProduct = _productService.GetProductById(product.ID);
            if(deleteProduct == null)
            {
                return BadRequest();
            }
            return View(deleteProduct);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {
                _productService.DeleteProduct(id);
                return RedirectToAction("GetAllProducts");
            }
            return View();

        }

    }
}
