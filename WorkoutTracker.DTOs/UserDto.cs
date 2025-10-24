namespace WorkoutTracker.DTOs
{
    public class UserDto : AuditColumnsDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Enabled { get; set; }
        public string Role { get; set; }
        public string ConcurrencyStamp { get; set; }
        public List<MeasurementDto> Measurements { get; set; }
        public List<WorkoutDto> Workouts { get; set; }
        public List<GoalDto> Goals { get; set; }
    }
}
