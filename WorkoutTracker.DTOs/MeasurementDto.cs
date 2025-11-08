using WorkoutTracker.Shared.DataContracts.Enums;

namespace WorkoutTracker.DTOs
{
    public class MeasurementDto : AuditColumnsDto
    {
        public int Id { get; set; }
        public EGender Gender { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public decimal BodyFatPercentage { get; set; }
        public decimal Chest { get; set; }
        public decimal Waist { get; set; }
        public decimal Hips { get; set; }
        public decimal Arms { get; set; }
        public string UserId { get; set; }
    }
}
