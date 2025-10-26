using WorkoutTracker.DTOs;
using WorkoutTracker.Shared.DataContracts.Responses;

namespace WorkoutTracker.Services.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends Emails to Users.
        /// </summary>
        /// <param name="emailMessage"></param>
        /// <returns></returns>
        Task<DataResponse<bool>> SendEmail(EmailMessageDto emailMessage);
    }
}
