using Api.ViewModels;
using AutoMapper;
using Domain.Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthorizationController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Authorization :: accessing in : "
                + DateTime.Now.ToLongTimeString();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserDTO model)
        {
            //check if the model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _signInManager.SignInAsync(user, false);
            return Ok(GenerateToken(model));
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserDTO model)
        {
            //check if the model is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));

            //checks the user credentials and returns a value
            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                isPersistent: false,
                lockoutOnFailure: false);

            if (result.Succeeded)
                return Ok(GenerateToken(model));
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Login");
                return BadRequest(ModelState);
            }
        }
        private UserTokenViewModel GenerateToken(UserDTO model)
        {
            //user declarations
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email),
                new Claim("user", "myUser"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            //generates a key based on symmetric algorithm
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));

            //generates the user token signature using the algorithm Hmac and the private key
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            //espiration token
            var expiration = _configuration["Jwt:ExpireHours"];
            var expiresOn = DateTime.UtcNow.AddHours(double.Parse(expiration));

            //token generation
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: expiresOn,
                claims: claims,
                signingCredentials: credentials
                );

            //reurn token data
            var userToken = new UserTokenViewModel
            {
                IsAuthenticated = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expiresOn,
                Message = "Token generated successful"
            };

            return userToken;
        }

    }
}
