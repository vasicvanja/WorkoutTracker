using WorkoutTracker.DTOs;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services
{
    public class GoalsService : IGoalsService
    {
        #region Declarations

        private readonly IGoalsRepository _goalsRepository;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="goalsRepository"></param>
        public GoalsService(IGoalsRepository goalsRepository)
        {
            _goalsRepository = goalsRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Goal by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<GoalDto>> Get(int Id) => await _goalsRepository.Get(Id);

        /// <summary>
        /// Get all Goals for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<DataResponse<List<GoalDto>>> GetAllByUserId(string userId) => await _goalsRepository.GetAllByUserId(userId);

        /// <summary>
        /// Create a Goal.
        /// </summary>
        /// <param name="goalDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(GoalDto goalDto) => await _goalsRepository.Create(goalDto);

        /// <summary>
        /// Update an existing Goal.
        /// </summary>
        /// <param name="goalDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Update(GoalDto goalDto) => await _goalsRepository.Update(goalDto);

        /// <summary>
        /// Delete Goal by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Delete(int Id) => await _goalsRepository.Delete(Id);

        #endregion
    }
}
