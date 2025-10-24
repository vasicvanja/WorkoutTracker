using WorkoutTracker.Shared.DataContracts.Enums;

namespace WorkoutTracker.Shared.DataContracts.Responses
{
    public class DataResponse<T> : BaseResponse<EDataResponseCode>
    {
        public T? Data { get; set; }
        public int Count { get; set; }
        public string ErrorMessage { get; set; }
        public bool Succeeded { get; set; }

    }
}
