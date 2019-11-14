using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DatingApp.API.Dtos;
namespace DatingApp.API.Controllers
{

    //[ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
            
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate the request
            userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

            if (await _repo.UserExists(userForRegisterDto.UserName))
                return BadRequest("User already exists");
            var userToCreate = new User
            {
                UserName = userForRegisterDto.UserName
            };

            var createdUser =await  _repo.Register(userToCreate, userForRegisterDto.Password);


            return StatusCode(201);
        }

    }
}