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

            // Users
            serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
            serviceCollection.AddScoped<IUsersService, UsersService>();

            // SMTP Settings
            serviceCollection.AddScoped<ISmtpSettingsService, SmtpSettingsService>();
            serviceCollection.AddScoped<ISmtpSettingsRepository, SmtpSettingsRepository>();

            // Email
            serviceCollection.AddScoped<IEmailService, EmailService>();

            // Password Encryption
            serviceCollection.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
        }
    }
}
