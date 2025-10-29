using WorkoutTracker.DTOs;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services
{
    public class ExercisesService : IExercisesService
    {
        #region Declarations

        private readonly IExercisesRepository _exercisesRepository;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="exercisesRepository"></param>
        public ExercisesService(IExercisesRepository exercisesRepository)
        {
            _exercisesRepository = exercisesRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Exercise by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<ExerciseDto>> Get(int Id) => await _exercisesRepository.Get(Id);

        /// <summary>
        /// Get all Exercises.
        /// </summary>
        /// <returns></returns>
        public async Task<DataResponse<List<ExerciseDto>>> GetAll() => await _exercisesRepository.GetAll();

        /// <summary>
        /// Create an Exercise.
        /// </summary>
        /// <param name="exerciseDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(ExerciseDto exerciseDto) => await _exercisesRepository.Create(exerciseDto);

        /// <summary>
        /// Update an existing Exercise.
        /// </summary>
        /// <param name="exerciseDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Update(ExerciseDto exerciseDto) => await _exercisesRepository.Update(exerciseDto);

        /// <summary>
        /// Delete Exercise by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Delete(int Id) => await _exercisesRepository.Delete(Id);

        #endregion
    }
}
