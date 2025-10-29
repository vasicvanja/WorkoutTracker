using WorkoutTracker.DTOs;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services.Interfaces
{
    public interface IWorkoutsService
    {
        /// <summary>
        /// Get Workout by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<WorkoutDto>> Get(int Id);

        /// <summary>
        /// Get all Workouts for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DataResponse<List<WorkoutDto>>> GetAllByUserId(string userId);

        /// <summary>
        /// Create a Workout.
        /// </summary>
        /// <param name="workoutDto"></param>
        /// <returns></returns>
        Task<DataResponse<int>> Create(CreateWorkoutDto createWorkoutDto);

        /// <summary>
        /// Update an existing Workout.
        /// </summary>
        /// <param name="workoutDto"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Update(CreateWorkoutDto updateWorkoutDto);

        /// <summary>
        /// Delete a Workout by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Delete(int Id);
    }
}
