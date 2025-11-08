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
    public class MeasurementsRepository : IMeasurementsRepository
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
        public MeasurementsRepository(ApplicationDbContext applicationDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get Measurement by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<MeasurementDto>> Get(int Id)
        {
            var result = new DataResponse<MeasurementDto>() { Data = null, ErrorMessage = null, Succeeded = false };

            try
            {
                var measurement = await _applicationDbContext.Measurements.FirstOrDefaultAsync(x => x.Id == Id);

                if (measurement == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Measurement), Id);
                    result.Data = null;

                    return result;
                }

                var measurementDto = _mapper.Map<Measurement, MeasurementDto>(measurement);
                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = measurementDto;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GetEntityFailed, nameof(Measurement), Id);
                result.Data = null;

                return result;
            }
        }

        /// <summary>
        /// Get all Measurements for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<DataResponse<List<MeasurementDto>>> GetAllByUserId(string userId)
        {
            var result = new DataResponse<List<MeasurementDto>>() { Data = null, ErrorMessage = null, Succeeded = false };

            try
            {
                var measurements = await _applicationDbContext
                    .Measurements
                    .Where(x => x.UserId == userId)
                    .OrderByDescending(x => x.DateCreated)
                    .ToListAsync();

                if (measurements == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = ResponseMessages.NoDataFound;
                    result.Succeeded = false;
                    result.Data = null;

                    return result;
                }

                var measurementDtos = _mapper.Map<List<Measurement>, List<MeasurementDto>>(measurements);
                result.ResponseCode = EDataResponseCode.Success;
                result.Succeeded = true;
                result.Data = measurementDtos;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.GettingEntitiesFailed, nameof(Measurement));
                result.Succeeded = false;
                result.Data = null;

                return result;
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create Measurement.
        /// </summary>
        /// <param name="measurementDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(MeasurementDto measurementDto)
        {
            var result = new DataResponse<int>();

            try
            {
                if (measurementDto == null)
                {
                    result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                    result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Measurement));

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                measurementDto.CreatedBy = user;
                measurementDto.ModifiedBy = user;

                var measurement = new Measurement
                {
                    Gender = measurementDto.Gender,
                    Weight = measurementDto.Weight,
                    Height = measurementDto.Height,
                    BodyFatPercentage = measurementDto.BodyFatPercentage,
                    Chest = measurementDto.Chest,
                    Waist = measurementDto.Waist,
                    Hips = measurementDto.Hips,
                    Arms = measurementDto.Arms,
                    UserId = measurementDto.UserId,
                    CreatedBy = measurementDto.CreatedBy,
                    ModifiedBy = measurementDto.ModifiedBy,
                    DateCreated = DateTime.UtcNow,
                    DateModified = DateTime.UtcNow,
                };

                await _applicationDbContext.Measurements.AddAsync(measurement);
                await _applicationDbContext.SaveChangesAsync();

                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;
                result.Data = measurement.Id;

                return result;

            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.UnsuccessfulCreationOfEntity, nameof(Measurement));

                return result;
            }
        }

        #endregion

        #region UPDATE

        /// <summary>
        /// Update an existing Measurement.
        /// </summary>
        /// <param name="measurementDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Update(MeasurementDto measurementDto)
        {
            var result = new DataResponse<bool>() { Data = false, Succeeded = false };

            if (measurementDto == null)
            {
                result.ResponseCode = EDataResponseCode.InvalidInputParameter;
                result.ErrorMessage = string.Format(ResponseMessages.InvalidInputParameter, nameof(Measurement));

                return result;
            }

            try
            {
                var existingMeasurement = await _applicationDbContext.Measurements.FirstOrDefaultAsync(x => x.Id == measurementDto.Id);

                if (existingMeasurement == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Measurement), measurementDto.Id);
                    result.Data = false;

                    return result;
                }

                var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
                measurementDto.ModifiedBy = user;
                measurementDto.DateModified = DateTime.UtcNow;

                _mapper.Map(measurementDto, existingMeasurement);

                await _applicationDbContext.SaveChangesAsync();

                result.Data = true;
                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.UnsuccessfulUpdateOfEntity, nameof(Measurement), measurementDto.Id);
                result.Data = false;

                return result;
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Delete Measurement by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Delete(int Id)
        {
            var result = new DataResponse<bool>() { Data = false, ErrorMessage = null, Succeeded = false };
            try
            {
                var measurement = await _applicationDbContext.Measurements.FirstOrDefaultAsync(x => x.Id == Id);

                if (measurement == null)
                {
                    result.ResponseCode = EDataResponseCode.NoDataFound;
                    result.ErrorMessage = string.Format(ResponseMessages.NoDataFoundForKey, nameof(Measurement), Id);
                    result.Data = false;

                    return result;
                }
                _applicationDbContext.Measurements.Remove(measurement);
                await _applicationDbContext.SaveChangesAsync();

                result.Succeeded = true;
                result.ResponseCode = EDataResponseCode.Success;
                result.Data = true;

                return result;
            }
            catch (Exception)
            {
                result.ResponseCode = EDataResponseCode.GenericError;
                result.ErrorMessage = string.Format(ResponseMessages.DeletionFailed, nameof(Measurement), Id);
                result.Data = false;
                return result;
            }
        }

        #endregion
    }
}
