using WorkoutTracker.DTOs;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services.Interfaces
{
    public interface IMeasurementsService
    {
        /// <summary>
        /// Get Measurement by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<MeasurementDto>> Get(int Id);

        /// <summary>
        /// Get All Measurements for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<DataResponse<List<MeasurementDto>>> GetAllByUserId(string userId);

        /// <summary>
        /// Create a Measurement.
        /// </summary>
        /// <param name="measurementDto"></param>
        /// <returns></returns>
        Task<DataResponse<int>> Create(MeasurementDto measurementDto);

        /// <summary>
        /// Update an existing Measurement.
        /// </summary>
        /// <param name="measurementDto"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Update(MeasurementDto measurementDto);

        /// <summary>
        /// Delete a Measurement by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Delete(int Id);
    }
}
