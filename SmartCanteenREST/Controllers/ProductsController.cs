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

        // GET: api/<ProductsController>
        [HttpGet]
        [Route("{isHot}")]
        public IList<Products> GetIsHotList(bool isHot)
        {
            return mng.GetProductsFromIsHot(isHot);
        }
    }
}
