using Microsoft.AspNetCore.Mvc;
using web.Core.DTOs;
using web.Core.Services;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<string>> RegisterAsync([FromBody] UserRegisterDTO userDto)
    {
        if (userDto == null)
            return BadRequest("Invalid user data.");

        var token = await _authService.RegisterAsync(userDto);
        if (string.IsNullOrEmpty(token))
            return BadRequest("User registration failed.");

        return Ok(new { Token = token, Message = "User registered successfully." }); // מחזירים את הטוקן
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginAsync([FromBody] UserLoginDTO userDto)
    {
        if (userDto == null)
            return BadRequest("Invalid login data.");

        var token = await _authService.LoginAsync(userDto);
        if (token == null)
            return Unauthorized("Invalid credentials.");

        return Ok(new { Token = token, Message = "User login successfully." }); // מחזירים את הטוקן
    }


}











////using Microsoft.AspNetCore.Mvc;

////// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

////namespace Web.Api.Controllers
////{
////    [Route("api/[controller]")]
////    [ApiController]
////    public class AuthController : ControllerBase
////    {
////        // GET: api/<AuthController>
////        [HttpGet]
////        public IEnumerable<string> Get()
////        {
////            return new string[] { "value1", "value2" };
////        }

////        // GET api/<AuthController>/5
////        [HttpGet("{id}")]
////        public string Get(int id)
////        {
////            return "value";
////        }

////        // POST api/<AuthController>
////        [HttpPost]
////        public void Post([FromBody] string value)
////        {
////        }

////        // PUT api/<AuthController>/5
////        [HttpPut("{id}")]
////        public void Put(int id, [FromBody] string value)
////        {
////        }

////        // DELETE api/<AuthController>/5
////        [HttpDelete("{id}")]
////        public void Delete(int id)
////        {
////        }
////    }
////}
