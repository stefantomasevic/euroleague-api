using Euroleague.Models;
using Microsoft.AspNetCore.Identity;

namespace Euroleague.Authorization
{
    public interface IAuthorizationRepository
    {
        Task<bool> RegisterUserAsync(Register model);
        Task<Admin> ValidateUserAsync(Login model);
    }
}
