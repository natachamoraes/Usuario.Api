using Microsoft.AspNetCore.Mvc;
using Usuario.Api.Data.Repository; 
using Usuario.Api.Entity; 
using System; 
using System.Linq; 
using System.Threading.Tasks;
using Usuario.Api.Model;

namespace Usuario.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
                return BadRequest("Usuário inválido");

            await _repository.InsertAsync(user); 
            return Ok("Usuário criado com sucesso");
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery]Guid id)
        {
            var user = await _repository.GetByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado");

            return Ok(user);
        }
        
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserRequest request)
        {
            var users = await _repository.GetAllAsync(request.PageNumber, request.PageSize);

            if (!users.Any())
                return NotFound("Nenhum usuário encontrado");

            return Ok(users);
        }

    }
}