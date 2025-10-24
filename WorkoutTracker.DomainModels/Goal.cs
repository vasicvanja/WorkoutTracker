using System.ComponentModel.DataAnnotations.Schema;

namespace WorkoutTracker.DomainModels
{
    public class Goal : AuditColumns
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CurrentValue { get; set; }
        public string TargetValue { get; set; }
        public bool IsCompleted { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
