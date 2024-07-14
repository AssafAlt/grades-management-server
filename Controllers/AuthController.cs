using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Auth;
using api.Interfaces;
using api.Interfaces.Services;
using api.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);


            var results = await _authService.Login(loginDto);

            if (results.Data == null) return StatusCode(results.StatusCode, results.Message);

            var userDto = (LoginedUserDto)results.Data;
            var token = userDto.Token;
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = false,
                SameSite = SameSiteMode.Lax,
                Path = "/",
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("jwt_token", token, cookieOptions);


            return StatusCode(results.StatusCode, results.Data);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var results = await _authService.Register(registerDto);

            return StatusCode(results.StatusCode, results.Message);

        }

    }

}