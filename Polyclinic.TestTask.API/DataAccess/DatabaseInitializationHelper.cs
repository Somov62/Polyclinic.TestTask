using Microsoft.EntityFrameworkCore;
using Polyclinic.TestTask.API.Models.Entities;

namespace Polyclinic.TestTask.API.DataAccess
{
    /// <summary>
    /// Помогает инициализировать базу данных поликлиники.
    /// </summary>
    public static class DatabaseInitializationHelper
    {
        /// <summary>
        ///     Инициализирует профиль development, 
        ///     создавая базу, применяя к ней миграции и 
        ///     заполняя таблицы начальными данными.
        /// </summary>
        public static async Task InitializeDevelopment(IServiceScope scope)
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<PolyclinicDbContext>();

            bool databaseAlreadyCreated = await dbContext.Database.CanConnectAsync();

            // Создаем базу и применяем миграции.
            await dbContext.Database.MigrateAsync();

            if (databaseAlreadyCreated)
                return;

            // Наполняем отладочными данными.
            var therapist = new Specialization() { Name = Specialization.THERAPIST_SPECIALIZATION_NAME };

            dbContext.Doctors.Add(new()
            {
                FIO = "Тихонова Софья Кирилловна",
                Cabinet = new() { Id = 10 },
                Specialization = therapist,
                MedicalDistrict = new() { Id = 1 }
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Родионов Дмитрий Даниилович",
                Cabinet = new() { Id = 11 },
                Specialization = therapist,
                MedicalDistrict = new() { Id = 2 }
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Еремин Марк Тимофеевич",
                Cabinet = new() { Id = 12 },
                Specialization = therapist,
                MedicalDistrict = new() { Id = 3 }
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Литвинов Александр Тихонович",
                Cabinet = new() { Id = 13 },
                Specialization = therapist,
                MedicalDistrict = new() { Id = 4 }
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Гаврилова Юлия Владиславовна",
                Cabinet = new() { Id = 20 },
                Specialization = new() { Name = "Хирург" }
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Абрамова Елизавета Михайловна",
                Cabinet = new() { Id = 21 },
                Specialization = new() { Name = "Офтальмолог" }
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
