using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Common;
using api.Dtos.Auth;

namespace api.Interfaces.Services
{
    public interface IAuthService
    {
        Task<ServiceResult> Login(LoginDto loginDto);
        Task<ServiceResult> Register(RegisterDto registerDto);
    }
}