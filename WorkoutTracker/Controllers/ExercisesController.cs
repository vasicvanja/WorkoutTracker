using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.DTOs;
using WorkoutTracker.Services;
using WorkoutTracker.Services.Interfaces;
using WorkoutTracker.Shared.DataContracts.Constants;
using WorkoutTracker.Shared.DataContracts.Enums;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        #region Declarations

        private readonly IExercisesService _exercisesService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="exercisesService"></param>
        public ExercisesController(IExercisesService exercisesService)
        {
            _exercisesService = exercisesService;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get an Exercise by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var exercise = await _exercisesService.Get(id);
                return Ok(Conversion<ExerciseDto>.ReturnResponse(exercise));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<ExerciseDto>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<ExerciseDto>.ReturnResponse(errRet));
            }
        }

        /// <summary>
        /// Get all Exercises.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var exercises = await _exercisesService.GetAll();
                return Ok(Conversion<List<ExerciseDto>>.ReturnResponse(exercises));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<List<ExerciseDto>>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<List<ExerciseDto>>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create a new Exercise.
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ExerciseDto exercise)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _exercisesService.Create(exercise);
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
        /// Update an existing Exercise.
        /// </summary>
        /// <param name="exercise"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("update")]
        public async Task<IActionResult> Update(ExerciseDto exercise)
        {
            try
            {
                var result = await _exercisesService.Update(exercise);
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
        /// Delete an Exercise by Id.
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
                var result = await _exercisesService.Delete(id);
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
