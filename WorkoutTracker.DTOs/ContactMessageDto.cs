﻿namespace WorkoutTracker.DTOs
{
    public class ContactMessageDto : AuditColumnsDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
