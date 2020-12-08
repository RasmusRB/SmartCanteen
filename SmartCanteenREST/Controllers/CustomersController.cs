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
    public class CustomersController : ControllerBase
    {
        CustomerManager mng = new CustomerManager();

        // GET: api/<CustomersController>
        [HttpGet]
        public IEnumerable<Customers> Get()
        {
            return mng.GetCustomerData();
        }

        // GET: api/<CustomersController>/today
        [HttpGet("today")]
        public Customers GetToday()
        {
            return mng.GetCustomerDataForDay(DateTime.Now.Date) ?? new Customers(0, DateTime.Now.Date);
        }

        // POST api/<CustomersController>
        [HttpPost]
        public bool Post([FromBody] Customers value)
        {
            return mng.CreateCustomerData(value);
        }
    }
}
