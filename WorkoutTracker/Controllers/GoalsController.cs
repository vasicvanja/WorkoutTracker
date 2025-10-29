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
    public class GoalsController : ControllerBase
    {
        #region Declarations

        private readonly IGoalsService _goalsService;

        #endregion

        #region Ctor.

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="goalsService"></param>
        public GoalsController(IGoalsService goalsService)
        {
            _goalsService = goalsService;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get a Goal by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var goal = await _goalsService.Get(id);
                return Ok(Conversion<GoalDto>.ReturnResponse(goal));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<GoalDto>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<GoalDto>.ReturnResponse(errRet));
            }
        }

        /// <summary>
        /// Get all Goals for User.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("allByUserId")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            try
            {
                var goals = await _goalsService.GetAllByUserId(userId);
                return Ok(Conversion<List<GoalDto>>.ReturnResponse(goals));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<List<GoalDto>>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<List<GoalDto>>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create a new Goal.
        /// </summary>
        /// <param name="goal"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] GoalDto goal)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _goalsService.Create(goal);
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
        /// Update an existing Goal.
        /// </summary>
        /// <param name="goal"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("update")]
        public async Task<IActionResult> Update(GoalDto goal)
        {
            try
            {
                var result = await _goalsService.Update(goal);
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
        /// Delete a Goal by Id.
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
                var result = await _goalsService.Delete(id);
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
