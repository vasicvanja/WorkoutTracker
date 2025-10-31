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
                var workout = await _applicationDbContext.Workouts
                    .Include(we => we.WorkoutExercises)
                    .ThenInclude(e => e.Exercise)
                    .FirstOrDefaultAsync(x => x.Id == Id);

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
                    .Include(we => we.WorkoutExercises)
                    .ThenInclude(e => e.Exercise)
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
        /// <param name="workoutDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(WorkoutDto workoutDto)
        {
            var result = new DataResponse<int>();

            try
            {
                if (workoutDto == null)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Workout));

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                workoutDto.CreatedBy = user;
                workoutDto.ModifiedBy = user;

                var workout = new Workout
                {
                    Name = workoutDto.Name,
                    Volume = workoutDto.Volume,
                    Duration = workoutDto.Duration,
                    UserId = workoutDto.UserId,
                    CreatedBy = workoutDto.CreatedBy,
                    ModifiedBy = workoutDto.ModifiedBy,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                    WorkoutExercises = new List<WorkoutExercise>()
                };

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
        public async Task<DataResponse<bool>> Update(WorkoutDto updateWorkoutDto)
        {
            var result = new DataResponse<bool>() { Data = false, Succeeded = false };

            if (updateWorkoutDto == null)
            {
                result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Workout));

                return result;
            }

            await using var transacton = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                var existingWorkout = await _applicationDbContext.Workouts
                    .Include(we => we.WorkoutExercises)
                    .FirstOrDefaultAsync(x => x.Id == updateWorkoutDto.Id);

                if (existingWorkout == null)
                {
                    await transacton.RollbackAsync();
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Workout), updateWorkoutDto.Id);

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                updateWorkoutDto.ModifiedBy = user;
                updateWorkoutDto.DateModified = DateTime.UtcNow;

                _mapper.Map(updateWorkoutDto, existingWorkout);

                // Exercises sync logic
                var updatedExercises = updateWorkoutDto.WorkoutExercises ?? new List<WorkoutExerciseDto>();
                var updatedExercisesIds = updatedExercises.Select(e => e.ExerciseId).ToList();

                // Remove deleted exercises
                var toRemove = existingWorkout.WorkoutExercises
                    .Where(we => !updatedExercisesIds.Contains(we.ExerciseId))
                    .ToList();

                if (toRemove.Any())
                {
                   _applicationDbContext.WorkoutExercises.RemoveRange(toRemove);
                }

                // Add or update exercises
                foreach (var exercise in updatedExercises)
                {
                    var existingExercise = existingWorkout.WorkoutExercises
                        .FirstOrDefault(we => we.ExerciseId == exercise.ExerciseId);

                    if (existingExercise == null)
                    {
                        // Add new
                        existingWorkout.WorkoutExercises.Add(new WorkoutExercise
                        {
                            WorkoutId = existingWorkout.Id,
                            ExerciseId = exercise.ExerciseId,
                            Sets = exercise.Sets,
                            Repetitions = exercise.Repetitions,
                            Weight = exercise.Weight
                        });
                    }
                    else
                    {
                        // Update existing
                        existingExercise.Sets = exercise.Sets;
                        existingExercise.Repetitions = exercise.Repetitions;
                        existingExercise.Weight = exercise.Weight;
                    }
                }

                await _applicationDbContext.SaveChangesAsync();
                await transacton.CommitAsync();

                result.Data = true;
                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;

                return result;
            }
            catch (Exception)
            {
                await transacton.RollbackAsync();
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.UnsuccessfulUpdateOfEntity, nameof(Workout));

                return result;
            }
        }

        /// <summary>
        /// Add Exercises to an existing Workout.
        /// </summary>
        /// <param name="workoutId"></param>
        /// <param name="exerciseIds"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> AddExercisesToWorkout(int workoutId, List<AddExerciseToWorkoutDto> exercises)
        {
            var result = new DataResponse<bool>() { Data = false, Succeeded = false };

            await using var transaction = await _applicationDbContext.Database.BeginTransactionAsync();

            try
            {
                if (exercises == null || exercises.Count == 0)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = ResponseMessages.ExercisesMustBeProvided;

                    return result;
                }

                // Fetch the target workout
                var workout = await _applicationDbContext.Workouts
                    .Include(we => we.WorkoutExercises)
                    .FirstOrDefaultAsync(x => x.Id == workoutId);

                if (workout == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = ResponseMessages.NoDataFound;

                    return result;
                }

                // Extract exercise IDs
                var exerciseIds = exercises.Select(e => e.ExerciseId).ToList();

                // Fetch valid exercises
                var validExercises = await _applicationDbContext.Exercises
                    .Where(e => exerciseIds.Contains(e.Id))
                    .ToListAsync();

                if (!validExercises.Any())
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = ResponseMessages.NoValidExercisesProvided;

                    return result;
                }

                // Add exercises to the workout
                foreach (var exercise in exercises)
                {
                    workout.WorkoutExercises.Add(new WorkoutExercise
                    {
                        WorkoutId = workout.Id,
                        ExerciseId = exercise.ExerciseId,
                        Sets = exercise.Sets,
                        Repetitions = exercise.Repetitions,
                        Weight = exercise.Weight
                    });
                }

                await _applicationDbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                result.Data = true;
                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;

                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.FailedToAddExercisesToWorkout, workoutId);

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
