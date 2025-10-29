using WorkoutTracker.DTOs;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Repositories.Interfaces
{
    public interface IExercisesRepository
    {
        /// <summary>
        /// Get Exercise by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<ExerciseDto>> Get(int Id);

        /// <summary>
        /// Get all Exercises.
        /// </summary>
        /// <returns></returns>
        Task<DataResponse<List<ExerciseDto>>> GetAll();

        /// <summary>
        /// Create an Exercise.
        /// </summary>
        /// <param name="exerciseDto"></param>
        /// <returns></returns>
        Task<DataResponse<int>> Create(ExerciseDto exerciseDto);

        /// <summary>
        /// Update an existing Exercise.
        /// </summary>
        /// <param name="exerciseDto"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Update(ExerciseDto exerciseDto);

        /// <summary>
        /// Delete an Exercise by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Delete(int Id);
    }
}
