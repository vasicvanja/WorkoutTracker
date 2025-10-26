using Microsoft.AspNetCore.Identity;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services.Interfaces
{
    public interface IRolesService
    {
        /// <summary>
        /// Get all Roles.
        /// </summary>
        /// <returns></returns>
        Task<DataResponse<List<IdentityRole>>> GetAll();
    }
}
