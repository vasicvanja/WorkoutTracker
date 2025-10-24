using AutoMapper;
using WorkoutTracker.DomainModels;
using WorkoutTracker.DTOs;

namespace WorkoutTracker.Mappers
{
    public class DomainToProfile : Profile
    {
        public DomainToProfile()
        {
            // Login
            CreateMap<Login, LoginDto>();

            // Register
            CreateMap<Register, RegisterDto>();

            // Workout
            CreateMap<Workout, WorkoutDto>();
            CreateMap<WorkoutDto, Workout>();

            // Exercise
            CreateMap<Exercise, ExerciseDto>();
            CreateMap<ExerciseDto, Exercise>();

            // Goal
            CreateMap<Goal, GoalDto>();
            CreateMap<GoalDto, Goal>();

            // WorkoutExercise
            CreateMap<WorkoutExercise, WorkoutExerciseDto>();
            CreateMap<WorkoutExerciseDto, WorkoutExercise>();

            // Measurement
            CreateMap<Measurement, MeasurementDto>();
            CreateMap<MeasurementDto, Measurement>();

            // SmtpSettings
            CreateMap<SmtpSettings, SmtpSettingsDto>();
            CreateMap<SmtpSettingsDto, SmtpSettings>();

            // Message
            CreateMap<ContactMessage, ContactMessageDto>();
            CreateMap<ContactMessageDto, ContactMessage>();
        }
    }
}
