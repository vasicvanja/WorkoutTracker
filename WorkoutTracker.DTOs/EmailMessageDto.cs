﻿namespace WorkoutTracker.DTOs
{
    public class EmailMessageDto
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}
