using Microsoft.AspNetCore.Identity;
using WorkoutTracker.DTOs;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterDto registerDto);
        Task<string> Login(LoginDto loginDto);
        Task Logout();
        Task<DataResponse<bool>> SendForgotPasswordEmail(string email);
        Task<DataResponse<bool>> ResetPassword(ResetPasswordDto resetPassword);
    }
}
