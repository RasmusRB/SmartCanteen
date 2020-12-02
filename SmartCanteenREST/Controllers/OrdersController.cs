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
    public class OrdersController : ControllerBase
    {
        OrderManager mng = new OrderManager();

        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Orders> Get()
        {
            return mng.GetAllOrders();
        }

        [HttpGet]
        [Route("{date}")]
        public Orders GetByDate(DateTime date)
        {
            return mng.GetOrderByDate(date);
        }
    }
}
