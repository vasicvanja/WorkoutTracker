using WorkoutTracker.DTOs;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services
{
    public class MeasurementsService : IMeasurementsService
    {
        #region Declarations

        private readonly IMeasurementsRepository _measurementsRepository;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="measurementsRepository"></param>
        public MeasurementsService(IMeasurementsRepository measurementsRepository)
        {
            _measurementsRepository = measurementsRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get Measurement by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<MeasurementDto>> Get(int Id) => await _measurementsRepository.Get(Id);

        /// <summary>
        /// Get all Measurements for User.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<DataResponse<List<MeasurementDto>>> GetAllByUserId(string userId) => await _measurementsRepository.GetAllByUserId(userId);

        /// <summary>
        /// Create a Measurement.
        /// </summary>
        /// <param name="measurementDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<int>> Create(MeasurementDto measurementDto) => await _measurementsRepository.Create(measurementDto);

        /// <summary>
        /// Update an existing Measurement.
        /// </summary>
        /// <param name="measurementDto"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Update(MeasurementDto measurementDto) => await _measurementsRepository.Update(measurementDto);

        /// <summary>
        /// Delete Measurement by Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<DataResponse<bool>> Delete(int Id) => await _measurementsRepository.Delete(Id);

        #endregion
    }
}
