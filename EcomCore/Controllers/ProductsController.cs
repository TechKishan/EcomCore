using EcomCore.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcomCore.Services.DBConnection;
using System.Data;
using EcomCore.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcomCore.Controllers
{
   
    public class ProductsController : ControllerBase
    {
        private readonly IProducts _products;
        public ProductsController(IProducts products)
        {
            _products = products;
        }

        [HttpPost]
        [Route("api/Products/AddProduct")]
        public async Task<ActionResult<MessageFor>> AddProduct([FromBody] Products data)
        {
            
            return await _products.AddProducts(data);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Products/GetProduct")]
        public async Task<ActionResult<DataTable>> GetProduct()
        {
            return await _products.GetProduct();
        }

        [HttpPost]
        [Route("api/Products/UpdateProduct")]
        public async Task<ActionResult<MessageFor>> UpdateProduct([FromBody] Products data)
        {
            return await _products.UpdateProduct(data);
        }
        [HttpPost]
        [Route("api/Products/GetProductByID")]
        public async Task<ActionResult<DataTable>> GetProductByID([FromBody] Products data)
        {
            return await _products.GetProductById(data);
        }
    }

}
