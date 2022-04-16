using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SuitcaseApi.DTO.Requests;
using SuitcaseApi.DTO.Responses;
using SuitcaseApi.Repositories.Interfaces;

namespace SuitcaseApi.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/users")]
    public class UsersController: ControllerBase
    {
        private readonly IUserDbRepository _userDbRepository;
        private readonly IConfiguration _configuration;

        public UsersController(IUserDbRepository userDbRepository, IConfiguration configuration)
        {
            _userDbRepository = userDbRepository;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var res = await _userDbRepository.RegisterUser(registerRequestDto);
            if (!res)
                return BadRequest("User with given data already exists!");

            return Ok("New user has been registered");
        }

        [HttpPut("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userDbRepository.GetUserFromDb(loginRequestDto);
            if (user is null)
                return NotFound("User with given data doesn't exists!");

            return Ok(new
            {
                user.User,
                Token = GenerateToken(user),
                user.RefreshToken
            });
        }

        [HttpPut("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var user = await _userDbRepository.RefreshToken(refreshToken);
            if (user is null)
                return NotFound("Account with given refresh token was not found!");
            if (user.RefreshToken is null)
                return BadRequest("Token expired, please log in again.");

            return Ok(new
            {
                user.User,
                Token = GenerateToken(user),
                user.RefreshToken
            });
        }


        private string GenerateToken(UserResponseDto userResponseDto)
        {
            Claim[] userClaims =
            {
                new(ClaimTypes.NameIdentifier, userResponseDto.User.IdUser.ToString()),
                new(ClaimTypes.Name, userResponseDto.User.Login)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Secret"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "http://localhost:5001",
                audience: "http://localhost:5001",
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        
    }
}