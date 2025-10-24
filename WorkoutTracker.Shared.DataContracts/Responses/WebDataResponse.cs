using WorkoutTracker.Shared.DataContracts.Enums;

namespace WorkoutTracker.Shared.DataContracts.Responses
{
    public class WebDataResponse<T> : BaseResponse<EBooleanResponseCode>
    {
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; set; }
    }
}
