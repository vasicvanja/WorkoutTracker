using WorkoutTracker.DTOs;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Repositories.Interfaces
{
    public interface ISmtpSettingsRepository
    {
        /// <summary>
        /// Get SmtpSettings by Id.
        /// </summary>
        /// <returns></returns>
        Task<DataResponse<SmtpSettingsDto>> GetSmtpSettings();

        /// <summary>
        /// Update an SmtpSetting.
        /// </summary>
        /// <param name="smtpSettingsDto"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> Update(SmtpSettingsDto smtpSettingsDto);
    }
}
