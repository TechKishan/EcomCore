using EcomCore.Interface;
using EcomCore.Models;
using EcomCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EcomCore.Services.DBConnection;

namespace EcomCore.Controllers
{

    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        public UserController(IUser user)
        {
            _user = user;
        }
        [HttpPost]
        [Route("api/User/RegisterUser")]

        public async Task<ActionResult<MessageFor>> RegisterUser([FromBody] User user)
        {
            return await _user.AddUserInfo(user);

        }

    }
}
