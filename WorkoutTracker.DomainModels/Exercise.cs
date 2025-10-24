using System.ComponentModel.DataAnnotations.Schema;
using WorkoutTracker.Shared.DataContracts.Enums;

namespace WorkoutTracker.DomainModels
{
    public class Exercise : AuditColumns
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public EMuscleGroup MuscleGroup { get; set; }
        public List<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
