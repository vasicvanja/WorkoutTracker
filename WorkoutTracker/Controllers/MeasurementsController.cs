using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.DTOs;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Shared.DataContracts.Constants;
using WorkoutTracker.Shared.DataContracts.Enums;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        #region Declarations

        private readonly IMeasurementsService _measurementsService;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="measurementsService"></param>
        public MeasurementsController(IMeasurementsService measurementsService)
        {
            _measurementsService = measurementsService;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get a Measurement by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var measurement = await _measurementsService.Get(id);
                return Ok(Conversion<MeasurementDto>.ReturnResponse(measurement));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<MeasurementDto>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<MeasurementDto>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create a new Measurement.
        /// </summary>
        /// <param name="artwork"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] MeasurementDto artwork)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _measurementsService.Create(artwork);
                return Ok(Conversion<int>.ReturnResponse(result));
            }
            catch (Exception ex)

            {
                var errRet = new DataResponse<int>
                {
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<int>.ReturnResponse(errRet));
            }
        }

        /// <summary>
        /// Get all Measurements for User.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            try
            {
                var measurements = await _measurementsService.GetAllByUserId(userId);
                return Ok(Conversion<List<MeasurementDto>>.ReturnResponse(measurements));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<List<MeasurementDto>>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<List<MeasurementDto>>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region UPDATE

        /// <summary>
        /// Update an existing Measurement.
        /// </summary>
        /// <param name="artwork"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("update")]
        public async Task<IActionResult> Update(MeasurementDto artwork)
        {
            try
            {
                var result = await _measurementsService.Update(artwork);
                return Ok(Conversion<bool>.ReturnResponse(result));
            }
            catch (Exception ex)

            {
                var errRet = new DataResponse<bool>
                {
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<bool>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region DELETE

        /// <summary>
        /// Delete a Measurement by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _measurementsService.Delete(id);
                return Ok(Conversion<bool>.ReturnResponse(result));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<bool>
                {
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<bool>.ReturnResponse(errRet));
            }
        }

        #endregion
    }
}
