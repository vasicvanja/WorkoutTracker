using Microsoft.AspNetCore.Identity;

namespace WorkoutTracker.DomainModels
{
    public class ApplicationUser : IdentityUser, IAuditColumns
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BodyFatPercentage { get; set; }
        public bool Enabled { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    }
}
