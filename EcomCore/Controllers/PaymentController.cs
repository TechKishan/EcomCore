using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Razorpay.Api;

namespace EcomCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly string _key = "rzp_test_7psh54C7OEP0XY";
        private readonly string _secret = "iUqbhYUxtf5mIZLAwuy1tDdF";

        [HttpPost("create-order")]
        public IActionResult CreateOrder([FromBody] PaymentRequest request)
        {
            try
            {
                // Initialize Razorpay Client
                RazorpayClient client = new RazorpayClient(_key, _secret);

                // Order details
                Dictionary<string, object> options = new Dictionary<string, object>
                {
                    { "amount", request.Amount * 100 },  // Convert to paise
                    { "currency", "INR" },
                    { "receipt", $"order_{Guid.NewGuid()}" },
                    { "payment_capture", 1 }  // Auto capture payment
                };

                // Create order in Razorpay
                Order order = client.Order.Create(options);

                return Ok(new
                {
                    orderId = order["id"].ToString(),
                    amount = order["amount"].ToString()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class PaymentRequest
    {
        public int Amount { get; set; }
    }
}

