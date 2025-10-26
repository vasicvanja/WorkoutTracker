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
    public class SmtpSettingsController : ControllerBase
    {
        #region Declarations

        private readonly ISmtpSettingsService _smtpSettingsService;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="smtpSettingsService"></param>
        public SmtpSettingsController(ISmtpSettingsService smtpSettingsService)
        {
            _smtpSettingsService = smtpSettingsService;
        }

        #endregion

        #region GET

        /// <summary>
        /// Get all SmtpSettings.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("getSmtpSettings")]
        public async Task<IActionResult> GetSmtpSettings()
        {
            try
            {
                var smtpSettings = await _smtpSettingsService.GetSmtpSettings();
                return Ok(Conversion<SmtpSettingsDto>.ReturnResponse(smtpSettings));
            }
            catch (Exception ex)
            {
                var errRet = new DataResponse<SmtpSettingsDto>
                {
                    Data = null,
                    ResponseCode = EDataResponseCode.GenericError,
                    Succeeded = false,
                    ErrorMessage = ex.Message
                };
                return BadRequest(Conversion<SmtpSettingsDto>.ReturnResponse(errRet));
            }
        }

        #endregion

        #region UPDATE

        /// <summary>
        /// Update existing Artwork.
        /// </summary>
        /// <param name="smtpSettings"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [Route("update")]
        public async Task<IActionResult> Update(SmtpSettingsDto smtpSettings)
        {
            try
            {
                var result = await _smtpSettingsService.Update(smtpSettings);
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
