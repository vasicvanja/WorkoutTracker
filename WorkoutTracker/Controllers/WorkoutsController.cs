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
    public class WorkoutsController : ControllerBase
    {
        #region Declarations

        private readonly IWorkoutsService _workoutsService;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="workoutsService"></param>
        public WorkoutsController(IWorkoutsService workoutsService)
        {
            _workoutsService = workoutsService;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get a Workout by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var workout = await _workoutsService.Get(id);
                return Ok(Conversion<WorkoutDto>.ReturnResponse(workout));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<WorkoutDto>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<WorkoutDto>.ReturnResponse(errRet));
            }
        }

        /// <summary>
        /// Get all Workouts for User.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            try
            {
                var workouts = await _workoutsService.GetAllByUserId(userId);
                return Ok(Conversion<List<WorkoutDto>>.ReturnResponse(workouts));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<List<WorkoutDto>>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<List<WorkoutDto>>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create a new Workout.
        /// </summary>
        /// <param name="createWorkoutDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateWorkoutDto createWorkoutDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _workoutsService.Create(createWorkoutDto);
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

        #endregion

        #region UPDATE

        /// <summary>
        /// Update an existing Workout.
        /// </summary>
        /// <param name="updateWorkoutDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("update")]
        public async Task<IActionResult> Update(CreateWorkoutDto updateWorkoutDto)
        {
            try
            {
                var result = await _workoutsService.Update(updateWorkoutDto);
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
        /// Delete a Workout by Id.
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
                var result = await _workoutsService.Delete(id);
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
