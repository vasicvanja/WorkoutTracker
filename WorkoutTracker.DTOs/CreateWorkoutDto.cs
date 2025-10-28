namespace WorkoutTracker.DTOs
{
    public class CreateWorkoutDto : AuditColumnsDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Volume { get; set; }
        public TimeSpan Duration { get; set; }
        public string UserId { get; set; }
        public List<int> WorkoutExercisesIds { get; set; }
    }
}
