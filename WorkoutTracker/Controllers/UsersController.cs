﻿using Microsoft.AspNetCore.Authorization;
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
    public class UsersController : ControllerBase
    {
        #region Declarations

        private readonly IUsersService _usersService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="userService"></param>
        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get a User by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin + ", " + UserRoles.User)]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var user = await _usersService.Get(id);
                return Ok(Conversion<UserDto>.ReturnResponse(user));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<UserDto>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<UserDto>.ReturnResponse(errRet));
            }
        }

        /// <summary>
        /// Get all Users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _usersService.GetAll();
                return Ok(Conversion<List<UserDto>>.ReturnResponse(users));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<List<UserDto>>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<List<UserDto>>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region CREATE

        /// <summary>
        /// Create new User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserDto user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _usersService.Create(user);
                return Ok(Conversion<string>.ReturnResponse(result));
            }
            catch (Exception ex)

            {
                var errRet = new DataResponse<string>
                {
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<string>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region UPDATE

        /// <summary>
        /// Update existing User.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("update")]
        public async Task<IActionResult> Update(UserDto user)
        {
            try
            {
                var result = await _usersService.Update(user);
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

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("{id}/enableDisableUser")]
        public async Task<IActionResult> EnableDisableUser(string id, bool enabled)
        {
            try
            {
                var result = await _usersService.EnableDisableUser(id, enabled);
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

        #region USER ROLE MANAGEMENT

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("{id}/addRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string id, string roleName)
        {
            try
            {
                var result = await _usersService.AddRoleToUser(id, roleName);
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

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("{id}/removeRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser(string id, string roleName)
        {
            try
            {
                var result = await _usersService.RemoveRoleFromUser(id, roleName);
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
        /// Delete a User by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                var result = await _usersService.Delete(id);
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
