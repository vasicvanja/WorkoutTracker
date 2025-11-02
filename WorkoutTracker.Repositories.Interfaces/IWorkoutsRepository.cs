using WorkoutTracker.DTOs;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Repositories.Interfaces
{
    public interface IWorkoutsRepository
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
        Task<DataResponse<int>> Create(WorkoutDto workoutDto);

        /// <summary>
        /// Update an existing Workout.
        /// </summary>
        /// <param name="updateWorkoutDto"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Update(WorkoutDto updateWorkoutDto);

        /// <summary>
        /// Add Exercises to an existing Workout.
        /// </summary>
        /// <param name="workoutId"></param>
        /// <param name="exerciseIds"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> AddExercisesToWorkout(int workoutId, List<AddExerciseToWorkoutDto> exercises);

        /// <summary>
        /// Delete a Workout by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Delete(int Id);
    }
}
