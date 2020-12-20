using MealOrdering.Server.Services.Infrastruce;
using MealOrdering.Shared.DTO;
using MealOrdering.Shared.ResponseModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MealOrdering.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService UserService)
        {
            userService = UserService;
        }



        [HttpGet("Users")]
        public async Task<ServiceResponse<List<UserDTO>>> GetUsers()
        {
            return new ServiceResponse<List<UserDTO>>()
            {
                Value = await userService.GetUsers()
            };
        }
    }
}
