using WorkoutTracker.DTOs;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Repositories.Interfaces
{
    public interface IGoalsRepository
    {
        /// <summary>
        /// Get Goal by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<GoalDto>> Get(int Id);

        /// <summary>
        /// Get all Goals for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DataResponse<List<GoalDto>>> GetAllByUserId(string userId);

        /// <summary>
        /// Create a Goal.
        /// </summary>
        /// <param name="goalDto"></param>
        /// <returns></returns>
        Task<DataResponse<int>> Create(GoalDto goalDto);

        /// <summary>
        /// Update an existing Goal.
        /// </summary>
        /// <param name="goalDto"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Update(GoalDto goalDto);

        /// <summary>
        /// Delete a Goal by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Delete(int Id);
    }
}
