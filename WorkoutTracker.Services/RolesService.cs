using Microsoft.AspNetCore.Identity;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services
{
    public class RolesService : IRolesService
    {
        #region Declarations

        private readonly IRolesRepository _rolesRepository;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="rolesRepository"></param>
        public RolesService(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        #endregion

        #region Methods

        public async Task<DataResponse<List<IdentityRole>>> GetAll() => await _rolesRepository.GetAll();

        #endregion
    }
}
