using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Shared.DataContracts.Enums;
using WorkoutTracker.Shared.DataContracts.Resources;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        #region Declarations

        private readonly RoleManager<IdentityRole> _roleManager;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="roleManager"></param>
        public RolesRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get all Roles.
        /// </summary>
        /// <returns></returns>
        public async Task<DataResponse<List<IdentityRole>>> GetAll()
        {
            var result = new DataResponse<List<IdentityRole>> { Data = null, Succeeded = false };

            try
            {
                var roles = await _roleManager.Roles.ToListAsync();

                if (!roles.Any())
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = ResponseMessages.NoDataFound;

                    return result;
                }

                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = roles;

                return result;
            }
            catch (Exception)
            {
                result.Data = null;
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GettingEntitiesFailed, "Role");

                return result;
            }
        }

        #endregion
    }
}
