using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.EF;
using WorkoutTracker.DomainModels;
using WorkoutTracker.DTOs;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Shared.DataContracts.Enums;
using WorkoutTracker.Shared.DataContracts.Resources;
using WorkoutTracker.Shared.DataContracts.Responses;
#pragma warning disable CS8629

namespace WorkoutTracker.Repositories
{
    public class WorkoutsRepository : IWorkoutsRepository
    {
        #region Declarations

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor.

        public WorkoutsRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get Workout by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<WorkoutDto>> Get(int Id)
        {
            var result = new DataResponse<WorkoutDto>() { Data = null, ErrorMessage = null, Succeeded = false };

            try
            {
                var workout = await _applicationDbContext.Workouts.FirstOrDefaultAsync(x => x.Id == Id);

                if (workout == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Workout), Id);
                    result.Data = null;

                    return result;
                }

                var workoutDto = _mapper.Map<Workout, WorkoutDto>(workout);
                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = workoutDto;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GetEntityFailed, nameof(Workout), Id);
                result.Data = null;

                return result;
            }
        }

        /// <summary>
        /// Get all Workouts for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<DataResponse<List<WorkoutDto>>> GetAllByUserId(string userId)
        {
            var result = new DataResponse<List<WorkoutDto>>() { Data = null, ErrorMessage = null, Succeeded = false };

            try
            {
                var workouts = await _applicationDbContext
                    .Workouts
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

                if (workouts == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = ResponseMessages.NoDataFound;
                    result.Succeeded = false;
                    result.Data = null;

                    return result;
                }

                var workoutDtos = _mapper.Map<List<Workout>, List<WorkoutDto>>(workouts);
                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = workoutDtos;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GettingEntitiesFailed, nameof(Workout));
                result.Succeeded = false;
                result.Data = null;

                return result;
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create Workout.
        /// </summary>
        /// <param name="createWorkoutDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(CreateWorkoutDto createWorkoutDto)
        {
            var result = new DataResponse<int>();

            try
            {
                if (createWorkoutDto == null)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Workout));

                    return result;
                }

                var existingWorkout = await _applicationDbContext.Workouts.FirstOrDefaultAsync(x => x.Id == createWorkoutDto.Id);

                if (existingWorkout != null)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = string.Format(ResponseMessages.EntityAlreadyExists, nameof(Workout), createWorkoutDto.Id);

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                createWorkoutDto.CreatedBy = user;
                createWorkoutDto.ModifiedBy = user;

                var workout = new Workout
                {
                    Name = createWorkoutDto.Name,
                    Volume = createWorkoutDto.Volume,
                    Duration = createWorkoutDto.Duration,
                    UserId = createWorkoutDto.UserId,
                    CreatedBy = createWorkoutDto.CreatedBy,
                    ModifiedBy = createWorkoutDto.ModifiedBy,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                };

                if (createWorkoutDto.WorkoutExercisesIds != null && createWorkoutDto.WorkoutExercisesIds.Any())
                {
                    var workoutExercises = await _applicationDbContext.WorkoutExercises
                        .Where(we => createWorkoutDto.WorkoutExercisesIds.Contains(we.Id))
                        .ToListAsync();
                    workout.WorkoutExercises = workoutExercises;
                }

                await _applicationDbContext.Workouts.AddAsync(workout);
                await _applicationDbContext.SaveChangesAsync();

                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;
                result.Data = workout.Id;

                return result;

            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.UnsuccessfulCreationOfEntity, nameof(Workout));

                return result;
            }
        }

        #endregion

        #region UPDATE

        /// <summary>
        /// Update a Workout.
        /// </summary>
        /// <param name="updateWorkoutDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Update(CreateWorkoutDto updateWorkoutDto)
        {
            var result = new DataResponse<bool>() { Data = false, Succeeded = false };

            if (updateWorkoutDto == null)
            {
                result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Workout));

                return result;
            }

            try
            {
                var existingWorkout = await _applicationDbContext.Workouts.FirstOrDefaultAsync(x => x.Id == updateWorkoutDto.Id);

                if (existingWorkout == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Workout), updateWorkoutDto.Id);

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                updateWorkoutDto.ModifiedBy = user;
                updateWorkoutDto.DateModified = DateTime.UtcNow;

                _mapper.Map(updateWorkoutDto, existingWorkout);

                if (updateWorkoutDto.WorkoutExercisesIds.Any())
                {
                    var workoutExercisesToUpdate = await _applicationDbContext.WorkoutExercises
                        .Where(a => updateWorkoutDto.WorkoutExercisesIds.Contains(a.Id))
                        .ToListAsync();

                    foreach (var workoutExercise in workoutExercisesToUpdate)
                    {
                        workoutExercise.WorkoutId = (int)updateWorkoutDto.Id;
                    }
                }

                await _applicationDbContext.SaveChangesAsync();

                result.Data = true;
                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.UnsuccessfulUpdateOfEntity, nameof(Workout));

                return result;
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Delete Workout by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Delete(int Id)
        {
            var result = new DataResponse<bool>() { Data = false, ErrorMessage = null, Succeeded = false };
            try
            {
                var workout = await _applicationDbContext.Workouts.FirstOrDefaultAsync(x => x.Id == Id);

                if (workout == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Workout), Id);
                    result.Data = false;

                    return result;
                }
                _applicationDbContext.Workouts.Remove(workout);
                await _applicationDbContext.SaveChangesAsync();

                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;
                result.Data = true;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.DeletionFailed, nameof(Workout), Id);
                result.Data = false;
                return result;
            }
        }

        #endregion
    }
}
