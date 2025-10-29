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
    public class GoalsRepository : IGoalsRepository
    {
        #region Declarations

        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="applicationDbContext"></param>
        /// <param name="mapper"></param>
        /// <param name="httpContextAccessor"></param>
        public GoalsRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get Goal by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<GoalDto>> Get(int Id)
        {
            var result = new DataResponse<GoalDto>() { Data = null, ErrorMessage = null, Succeeded = false };

            try
            {
                var goal = await _applicationDbContext.Goals.FirstOrDefaultAsync(x => x.Id == Id);

                if (goal == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Goal), Id);
                    result.Data = null;

                    return result;
                }

                var goalDto = _mapper.Map<Goal, GoalDto>(goal);
                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = goalDto;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GetEntityFailed, nameof(Goal), Id);
                result.Data = null;

                return result;
            }
        }

        /// <summary>
        /// Get all Goals for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<DataResponse<List<GoalDto>>> GetAllByUserId(string userId)
        {
            var result = new DataResponse<List<GoalDto>>() { Data = null, ErrorMessage = null, Succeeded = false };

            try
            {
                var goals = await _applicationDbContext
                    .Goals
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

                if (goals == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = ResponseMessages.NoDataFound;
                    result.Succeeded = false;
                    result.Data = null;

                    return result;
                }

                var goalDtos = _mapper.Map<List<Goal>, List<GoalDto>>(goals);
                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = goalDtos;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GettingEntitiesFailed, nameof(Goal));
                result.Succeeded = false;
                result.Data = null;

                return result;
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create Goal.
        /// </summary>
        /// <param name="goalDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(GoalDto goalDto)
        {
            var result = new DataResponse<int>();

            try
            {
                if (goalDto == null)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Goal));

                    return result;
                }

                var existingGoal = await _applicationDbContext.Goals.FirstOrDefaultAsync(x => x.Id == goalDto.Id);

                if (existingGoal != null)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = string.Format(ResponseMessages.EntityAlreadyExists, nameof(Goal), goalDto.Id);

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                goalDto.CreatedBy = user;
                goalDto.ModifiedBy = user;

                var goal = new Goal
                {
                    Name = goalDto.Name,
                    CurrentValue = goalDto.CurrentValue,
                    TargetValue = goalDto.TargetValue,
                    UserId = goalDto.UserId,
                    IsCompleted = goalDto.IsCompleted,
                    CreatedBy = goalDto.CreatedBy,
                    ModifiedBy = goalDto.ModifiedBy,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                };

                await _applicationDbContext.Goals.AddAsync(goal);
                await _applicationDbContext.SaveChangesAsync();

                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;
                result.Data = goal.Id;

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
        /// Update an existing Goal.
        /// </summary>
        /// <param name="goalDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Update(GoalDto goalDto)
        {
            var result = new DataResponse<bool>() { Data = false, Succeeded = false };

            if (goalDto == null)
            {
                result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Goal));

                return result;
            }

            try
            {
                var existingGoal = await _applicationDbContext.Goals.FirstOrDefaultAsync(x => x.Id == goalDto.Id);

                if (existingGoal == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Goal), goalDto.Id);
                    result.Data = false;

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                goalDto.ModifiedBy = user;
                goalDto.DateModified = DateTime.UtcNow;

                _mapper.Map(goalDto, existingGoal);

                await _applicationDbContext.SaveChangesAsync();

                result.Data = true;
                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.UnsuccessfulUpdateOfEntity, nameof(Goal), goalDto.Id);
                result.Data = false;

                return result;
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Delete Goal by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Delete(int Id)
        {
            var result = new DataResponse<bool>() { Data = false, ErrorMessage = null, Succeeded = false };

            try
            {
                var goal = await _applicationDbContext.Goals.FirstOrDefaultAsync(x => x.Id == Id);

                if (goal == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Goal), Id);
                    result.Data = false;

                    return result;
                }
                _applicationDbContext.Goals.Remove(goal);
                await _applicationDbContext.SaveChangesAsync();

                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;
                result.Data = true;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.DeletionFailed, nameof(Goal), Id);
                result.Data = false;
                return result;
            }
        }

        #endregion
    }
}
