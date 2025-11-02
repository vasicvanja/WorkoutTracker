namespace WorkoutTracker.DTOs
{
    public class AddExerciseToWorkoutDto : AuditColumnsDto
    {
        public int ExerciseId { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public decimal Weight { get; set; }
    }
}
