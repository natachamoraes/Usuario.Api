using Microsoft.AspNetCore.Mvc;
using Usuario.Api.Data.Repository; 
using Usuario.Api.Entity; 
using System; 
using System.Linq; 
using System.Threading.Tasks;
using Usuario.Api.Model;
using Usuario.Api.Service;

namespace Usuario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService service)
        {
            _userService = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto user)
        {
            try
            {
                var createUser = await _userService.CreateUser(user);

                var response = new ApiResponse<User>()
                {
                    Success = true,
                    Message = "Usuario criado com sucesso",
                    Data = createUser
                };
            
                return CreatedAtAction(nameof(CreateUser), response);
            }
            catch (Exception e)
            {
                var response = new ApiResponse<User>(false, e.Message, null);
                return StatusCode(500, response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery]int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
                return NotFound("Usuário não encontrado");

            return Ok(user);
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserRequestDto requestDto)
        {
            var users = await _userService.GetAllUsers(requestDto);

            if (!users.Any())
                return NotFound("Nenhum usuário encontrado");

            return Ok(users);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequestDto user)
        {
            try
            {
                var userUpdate = await _userService.UpdateUser(user);
                var response = new ApiResponse<User>()
                {
                    Success = true,
                    Message = "Usuario atualizado com sucesso",
                    Data = userUpdate
                };
                
                
                return  Ok(response);
            }
            catch (Exception e)
            {
                var response = new ApiResponse<User>(false, e.Message, null);
                return StatusCode(500, response);
            }
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromQuery] int id)
        {
            try
            {
                await _userService.DeleteUser(id);
                var response = new ApiResponse<User>()
                {
                    Success = true,
                    Message = "Usuario Deletado com sucesso"
                };
                    
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}