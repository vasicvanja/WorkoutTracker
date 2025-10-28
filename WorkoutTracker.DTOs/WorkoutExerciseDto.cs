namespace WorkoutTracker.DTOs
{
    public class WorkoutExerciseDto : AuditColumnsDto
    {
        public int Id { get; set; }
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public decimal Weight { get; set; }
        public int WorkoutId { get; set; }
        public int ExerciseId { get; set; }
    }
}
