using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.DomainModels
{
    public class WorkoutExercise
    {
        public int Sets { get; set; }
        public int Repetitions { get; set; }
        public decimal Weight { get; set; }
        public int WorkoutId { get; set; }
        public Workout Workout { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
