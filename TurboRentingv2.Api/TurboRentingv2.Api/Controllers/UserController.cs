using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TurboRentingv2.Api.Interfaces.Services;
using TurboRentingv2.Api.Models.EntitiesDtos;
using TurboRentingv2.Api.Models.Enums;
using TurboRentingv2.Requests.Users;

namespace TurboRentingv2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ICollection<UserDto>> Get()
        {
            var users = userService.Get();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetUserById(int id)
        {
            var user = userService.GetById(id);

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserDto> Post([FromBody] CreateUserRequest request)
        {
            var user = mapper.Map<UserDto>(request);

            var addedUser = userService.Add(user);

            return CreatedAtAction(nameof(Get), addedUser);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateUserRequest request)
        {
            var user = mapper.Map<UserDto>(request);

            user.Id = id;

            userService.Update(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            userService.Delete(id);

            return NoContent();
        }

    }
}
