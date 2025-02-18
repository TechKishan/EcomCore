using EcomCore.Interface;
using EcomCore.Models;
using EcomCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Controllers
{
   // [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
       // [AllowAnonymous]
        [HttpPost]
        [Route("api/User/RegisterUser")]

        public async Task<ActionResult<MessageFor>> RegisterUser([FromBody] User user)
        {
            return await _user.AddUserInfo(user);

        }

        [HttpPost("encrypt")]
        public IActionResult EncryptData([FromBody] string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
            {
                return BadRequest("Input cannot be empty");
            }

            string encryptedText = UserService.Encrypt(plainText);
            return Ok(new { EncryptedText = encryptedText });
        }

        [HttpPost("decrypt")]
        [Route("api/User/decrypt")]
        public IActionResult DecryptData([FromBody] string encryptedText)
        {
            if (string.IsNullOrEmpty(encryptedText))
            {
                return BadRequest("Input cannot be empty");
            }

            string decryptedText = UserService.Decrypt(encryptedText);
            return Ok(new { DecryptedText = decryptedText });
        }

    }
}
