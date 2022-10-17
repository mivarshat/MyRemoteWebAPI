using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("controller")]
    public class AuthController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly IGenerateToken generateToken;

        public AuthController(IUserRepository userRepository,IGenerateToken generateToken)
        {
            this.userRepository = userRepository;
            this.generateToken = generateToken;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsynch(Models.DTO.loginRequest loginRequest)
        {
            //Validate the incoming request

            //Check if user is authenticated
            var user= await userRepository.AuthenticateUser(loginRequest.UserName, loginRequest.Password);

            if(user != null)
            {
                //Generate JWT Token
                var token=await generateToken.GenerateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("User Name or Password is incorrect");
        }
    }
}
