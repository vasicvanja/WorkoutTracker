using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.DomainModels
{
    public class Workout : AuditColumns
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Volume { get; set; }
        public TimeSpan Duration { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public List<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
