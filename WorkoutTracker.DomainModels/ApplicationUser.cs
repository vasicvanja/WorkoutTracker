using Microsoft.AspNetCore.Identity;

namespace WorkoutTracker.DomainModels
{
    public class ApplicationUser : IdentityUser, IAuditColumns
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public List<Measurement> Measurements { get; set; }
        public List<Workout> Workouts { get; set; }
        public List<Goal> Goals { get; set; }
    }
}
