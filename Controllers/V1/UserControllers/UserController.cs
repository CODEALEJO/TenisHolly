using Microsoft.AspNetCore.Mvc;
using TenisHolly.Interface;
using TenisHolly.Interfaces;

namespace TenisHolly.Controllers.V1.UserControllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Tags("User")]
    public class UserController : ControllerBase
    {
        protected readonly IUserInterface _userService;

        public UserController(IUserInterface userService)
        {
            _userService = userService;
        }
    }
}
