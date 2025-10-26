using Microsoft.AspNetCore.Identity;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Repositories.Interfaces
{
    public interface IRolesRepository
    {
        /// <summary>
        /// Get all Roles.
        /// </summary>
        /// <returns></returns>
        Task<DataResponse<List<IdentityRole>>> GetAll();
    }
}
