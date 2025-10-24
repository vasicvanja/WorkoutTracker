namespace WorkoutTracker.DTOs
{
    public class GoalDto : AuditColumnsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentValue { get; set; }
        public string TargetValue { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
        public List<WorkoutExerciseDto> WorkoutExercises { get; set; }
    }
}
