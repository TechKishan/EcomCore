using EcomCore.Interface;
using EcomCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Controllers
{
    
    public class OrderController : ControllerBase
    {
        private readonly IOrder _order;
        public OrderController(IOrder order)
        {
            _order = order;
        }

        [HttpPost]
        [Route("api/Orders/Insert")]
        public async Task<ActionResult<MessageFor>> Insert ([FromBody] Orders data)
        {
            return await _order.OrderInsert(data);
        }   

        [HttpPost]
        [Route("api/Orders/Update")]
        public async Task<ActionResult<MessageFor>> Update([FromBody] Orders data)
        {
            return await _order.OrderUpdate(data);
        }

        [HttpPost]
        [Route("api/Orders/Delete")]
        public async Task<ActionResult<MessageFor>> Delete([FromBody] Orders data)
        {
            return await _order.OrderDelete(data);
        }
    }
}
