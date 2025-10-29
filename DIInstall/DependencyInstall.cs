using CustomValidation.Impl;
using CustomValidation.Interface;
using Microsoft.Extensions.DependencyInjection;
using WorkoutTracker.Repositories;
using WorkoutTracker.Repositories.Interfaces;
using WorkoutTracker.Services;
using WorkoutTracker.Services.Interfaces;

namespace DIInstall
{
    /// <summary>
    /// DependencyInstall class.
    /// </summary>
    public static class DependencyInstall
    {
        /// <summary>
        /// Installs application specific services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void InstallAppDependencies(this IServiceCollection serviceCollection)
        {
            // Authentication
            serviceCollection.AddTransient<IAuthService, AuthService>();

            // Measurements
            serviceCollection.AddScoped<IMeasurementsRepository, MeasurementsRepository>();
            serviceCollection.AddScoped<IMeasurementsService, MeasurementsService>();

            // Goals
            serviceCollection.AddScoped<IGoalsRepository, GoalsRepository>();
            serviceCollection.AddScoped<IGoalsService, GoalsService>();

            // Workouts
            serviceCollection.AddScoped<IWorkoutsRepository, WorkoutsRepository>();
            serviceCollection.AddScoped<IWorkoutsService, WorkoutsService>();

            // Exercises
            serviceCollection.AddScoped<IExercisesRepository, ExercisesRepository>();
            serviceCollection.AddScoped<IExercisesService, ExercisesService>();

            // Users
            serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
            serviceCollection.AddScoped<IUsersService, UsersService>();

            // Roles
            serviceCollection.AddScoped<IRolesRepository, RolesRepository>();
            serviceCollection.AddScoped<IRolesService, RolesService>();

            // SMTP Settings
            serviceCollection.AddScoped<ISmtpSettingsService, SmtpSettingsService>();
            serviceCollection.AddScoped<ISmtpSettingsRepository, SmtpSettingsRepository>();

            // Email
            serviceCollection.AddScoped<IEmailService, EmailService>();

            // Password Encryption
            serviceCollection.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();

            // Validator
            serviceCollection.AddSingleton<IEmailValidator, EmailValidator>();
        }
    }
}
