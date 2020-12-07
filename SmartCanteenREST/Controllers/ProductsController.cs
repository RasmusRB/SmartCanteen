using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLib.Model;
using SmartCanteenREST.Managers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartCanteenREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ProductManager mng = new ProductManager();

        // GET: api/<ProductsController>
        [HttpGet]
        public IList<Products> Get()
        {
            return mng.GetProductInfo();
        }

        // GET: api/<ProductsController>/<category>
        // Select product based on Category FK
        [HttpGet]
        [Route("{category}/{id}")]
        public IList<Products> GetByKey(int id)
        {
            return mng.GetProductByCategory(id);
        }

        // GET: api/<ProductsController>
        [HttpGet]
        [Route("{isHot}")]
        public IList<Products> GetByBool(bool isHot)
        {
            return mng.GetProductsFromBool(isHot);
        }

        // POST
        [HttpPost]
        public bool Post([FromBody] Products value)
        {
            return mng.CreateProduct(value);
        }

        // DELETE
        [HttpDelete]
        public Products Delete(int id)
        {
            return mng.DeleteProduct(id);
        }
    }
}
