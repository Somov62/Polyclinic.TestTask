using Polyclinic.TestTask.API.Services.Doctors;

namespace Polyclinic.TestTask.API.Services
{
    /// <summary>
    /// Регистрация зависимостей - сервисов бизнес логики.
    /// </summary>
    public static class DependencyRegistration
    {
        /// <summary>
        /// Регистрация сервисов бизнес логики.
        /// </summary>
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddScoped<DoctorsService>()
                ;
    }
}
