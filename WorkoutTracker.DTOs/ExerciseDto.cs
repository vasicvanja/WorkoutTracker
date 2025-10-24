using WorkoutTracker.Shared.DataContracts.Enums;

namespace WorkoutTracker.DTOs
{
    public class ExerciseDto : AuditColumnsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EMuscleGroup MuscleGroup { get; set; }
        public List<WorkoutExerciseDto> WorkoutExercises { get; set; }
    }
}
