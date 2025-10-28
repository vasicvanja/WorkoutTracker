using WorkoutTracker.DTOs;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services
{
    public class WorkoutsService : IWorkoutsService
    {
        #region Declarations

        private readonly IWorkoutsRepository _workoutsRepository;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="workoutsRepository"></param>
        public WorkoutsService(IWorkoutsRepository workoutsRepository)
        {
            _workoutsRepository = workoutsRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Workout by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<WorkoutDto>> Get(int Id) => await _workoutsRepository.Get(Id);

        /// <summary>
        /// Get all Workouts for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<DataResponse<List<WorkoutDto>>> GetAllByUserId(string userId) => await _workoutsRepository.GetAllByUserId(userId);

        /// <summary>
        /// Create a Workout.
        /// </summary>
        /// <param name="goalDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(CreateWorkoutDto createWorkoutDto) => await _workoutsRepository.Create(createWorkoutDto);

        /// <summary>
        /// Update an existing Workout.
        /// </summary>
        /// <param name="goalDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Update(CreateWorkoutDto updateWorkoutDto) => await _workoutsRepository.Update(updateWorkoutDto);

        /// <summary>
        /// Delete Workout by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Delete(int Id) => await _workoutsRepository.Delete(Id);

        #endregion
    }
}
