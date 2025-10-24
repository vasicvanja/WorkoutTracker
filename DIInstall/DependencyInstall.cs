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
            // Users
            serviceCollection.AddScoped<IUsersRepository, UsersRepository>();
            serviceCollection.AddScoped<IUsersService, UsersService>();
        }
    }
}
