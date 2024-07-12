using System;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.Dtos.Auth;
using api.Interfaces;
using api.Interfaces.Services;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    public async Task<ServiceResult> Login(LoginDto loginDto)
    {
        try
        {
             var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Email.ToLower());

            if(user == null) return new ServiceResult{StatusCode = StatusCodes.Status401Unauthorized, Message = "Invalid username!"};

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded) return new ServiceResult{StatusCode = StatusCodes.Status401Unauthorized, Message = "Username not found and/or incorrect password"};

            var newLoginedUser = new LoginedUserDto
                {
                    Email = user.Email,
                    FirstName =user.FirstName ,
                    LastName = user.LastName,
                    Token = _tokenService.CreateToken(user)
                };

            return new ServiceResult{
                StatusCode = StatusCodes.Status200OK, Data = newLoginedUser
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
        }
    }

    public async Task<ServiceResult> Register(RegisterDto registerDto)
    {
            try
            {
                var appUser = new AppUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName
                };

                var createdUser = await _userManager.CreateAsync(appUser,registerDto.Password);

                if(createdUser.Succeeded)
                {

                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if(roleResult.Succeeded)
                    {
                       return new ServiceResult {StatusCode = StatusCodes.Status200OK, Message = "User was created successfully"}; 
                    }
                    else{
                        return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = roleResult.Errors.ToString()};
                    }

                }
                else{
                    return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = createdUser.Errors.ToString()};
                }
            }
            catch (Exception ex)
        {
            return new ServiceResult { StatusCode = StatusCodes.Status500InternalServerError, Message = ex.Message };
        }

        }
}
}

