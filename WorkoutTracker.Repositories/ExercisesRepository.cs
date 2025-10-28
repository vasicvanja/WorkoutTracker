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
    public class ExercisesRepository : IExercisesRepository
    {
        #region Declarations

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="applicationDbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContextAccessor"></param>
        public ExercisesRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get Exercise by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<ExerciseDto>> Get(int Id)
        {
            var result = new DataResponse<ExerciseDto>() { Data = null, ErrorMessage = null, Succeeded = false };

            try
            {
                var exercise = await _applicationDbContext.Exercises.FirstOrDefaultAsync(x => x.Id == Id);

                if (exercise == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Exercise), Id);
                    result.Data = null;

                    return result;
                }

                var exerciseDto = _mapper.Map<Exercise, ExerciseDto>(exercise);
                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = exerciseDto;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GetEntityFailed, nameof(Exercise), Id);
                result.Data = null;

                return result;
            }
        }

        /// <summary>
        /// Get all Exercises.
        /// </summary>
        /// <returns></returns>
        public async Task<DataResponse<List<ExerciseDto>>> GetAll()
        {
            var result = new DataResponse<List<ExerciseDto>> { Data = null, ErrorMessage = null, Succeeded = false };

            try
            {
                var exercises = await _applicationDbContext.Exercises.ToListAsync();

                if (exercises == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = ResponseMessages.NoDataFound;

                    return result;
                }

                var exerciseDtos = _mapper.Map<List<Exercise>, List<ExerciseDto>>(exercises);

                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = exerciseDtos;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GettingEntitiesFailed, nameof(Exercise));
                result.Data = null;

                return result;
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create Exercise.
        /// </summary>
        /// <param name="exerciseDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(ExerciseDto exerciseDto)
        {
            var result = new DataResponse<int>();

            try
            {
                if (exerciseDto == null)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Exercise));

                    return result;
                }

                var existingExercise = await _applicationDbContext.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseDto.Id);

                if (existingExercise != null)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = string.Format(ResponseMessages.EntityAlreadyExists, nameof(Exercise), exerciseDto.Id);

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                exerciseDto.CreatedBy = user;
                exerciseDto.ModifiedBy = user;

                var exercise = new Exercise
                {
                    Name = exerciseDto.Name,
                    Description = exerciseDto.Description,
                    MuscleGroup = exerciseDto.MuscleGroup,
                    CreatedBy = exerciseDto.CreatedBy,
                    ModifiedBy = exerciseDto.ModifiedBy,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                };

                await _applicationDbContext.Exercises.AddAsync(exercise);
                await _applicationDbContext.SaveChangesAsync();

                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;
                result.Data = exercise.Id;

                return result;

            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.UnsuccessfulCreationOfEntity, nameof(Exercise));

                return result;
            }
        }

        #endregion

        #region UPDATE

        /// <summary>
        /// Update an existing Exercise.
        /// </summary>
        /// <param name="exerciseDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Update(ExerciseDto exerciseDto)
        {
            var result = new DataResponse<bool>() { Data = false, Succeeded = false };

            if (exerciseDto == null)
            {
                result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Exercise));

                return result;
            }

            try
            {
                var existingExercise = await _applicationDbContext.Exercises.FirstOrDefaultAsync(x => x.Id == exerciseDto.Id);

                if (existingExercise == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Exercise), exerciseDto.Id);
                    result.Data = false;

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                exerciseDto.ModifiedBy = user;
                exerciseDto.DateModified = DateTime.UtcNow;

                _mapper.Map(exerciseDto, existingExercise);

                await _applicationDbContext.SaveChangesAsync();

                result.Data = true;
                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.UnsuccessfulUpdateOfEntity, nameof(Exercise), exerciseDto.Id);
                result.Data = false;

                return result;
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Delete Exercise by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Delete(int Id)
        {
            var result = new DataResponse<bool>() { Data = false, ErrorMessage = null, Succeeded = false };

            try
            {
                var exercise = await _applicationDbContext.Exercises.FirstOrDefaultAsync(x => x.Id == Id);

                if (exercise == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Exercise), Id);
                    result.Data = false;

                    return result;
                }
                _applicationDbContext.Exercises.Remove(exercise);
                await _applicationDbContext.SaveChangesAsync();

                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;
                result.Data = true;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.DeletionFailed, nameof(Exercise), Id);
                result.Data = false;
                return result;
            }
        }

        #endregion
    }
}