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
            dbContext
                .InitializeCabinets()
                .InitializeMedicalDistricts()
                .InitializeSpecializations()
                .InitializeDoctors()
                .InitializePatients()
                ;
        }

        #region фабрики отладочных данных
        private static PolyclinicDbContext InitializeCabinets(this PolyclinicDbContext dbContext)
        {
            dbContext.Cabinets.AddRange(Enumerable.Range(1, 100).Select(n => new Cabinet() { Id = n }));
            dbContext.SaveChanges();
            return dbContext;
        }

        private static PolyclinicDbContext InitializeMedicalDistricts(this PolyclinicDbContext dbContext)
        {
            dbContext.MedicalDistricts.AddRange(Enumerable.Range(1, 4).Select(n => new MedicalDistrict() { Id = n }));
            dbContext.SaveChanges();
            return dbContext;
        }

        private static PolyclinicDbContext InitializeDoctors(this PolyclinicDbContext dbContext)
        {
            dbContext.Doctors.Add(new()
            {
                FIO = "Тихонова Софья Кирилловна",
                Cabinet = dbContext.Cabinets.Find(10)!,
                Specialization = dbContext.Specializations.Find(1)!,
                MedicalDistrict = dbContext.MedicalDistricts.Find(1)!
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Родионов Дмитрий Даниилович",
                Cabinet = dbContext.Cabinets.Find(11)!,
                Specialization = dbContext.Specializations.Find(1)!,
                MedicalDistrict = dbContext.MedicalDistricts.Find(2)!
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Еремин Марк Тимофеевич",
                Cabinet = dbContext.Cabinets.Find(12)!,
                Specialization = dbContext.Specializations.Find(1)!,
                MedicalDistrict = dbContext.MedicalDistricts.Find(3)!
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Литвинов Александр Тихонович",
                Cabinet = dbContext.Cabinets.Find(13)!,
                Specialization = dbContext.Specializations.Find(1)!,
                MedicalDistrict = dbContext.MedicalDistricts.Find(4)!
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Гаврилова Юлия Владиславовна",
                Cabinet = dbContext.Cabinets.Find(20)!,
                Specialization = dbContext.Specializations.Find(2)!,
            });

            dbContext.Doctors.Add(new()
            {
                FIO = "Абрамова Елизавета Михайловна",
                Cabinet = dbContext.Cabinets.Find(21)!,
                Specialization = dbContext.Specializations.Find(3)!,
            });

            dbContext.SaveChanges();
            return dbContext;
        }

        private static PolyclinicDbContext InitializeSpecializations(this PolyclinicDbContext dbContext)
        {
            dbContext.Specializations.Add(new()
            {
                Name = Specialization.THERAPIST_SPECIALIZATION_NAME
            });

            dbContext.Specializations.Add(new()
            {
                Name = "Хирург"
            });

            dbContext.Specializations.Add(new()
            {
                Name = "Офтальмолог"
            });

            dbContext.Specializations.Add(new()
            {
                Name = "Кардиолог"
            });

            dbContext.SaveChanges();
            return dbContext;
        }

        private static PolyclinicDbContext InitializePatients(this PolyclinicDbContext dbContext)
        {
            var random = new Random();

            dbContext.Patients.Add(new()
            {
                FirstName = "Егор",
                LastName = "Астафьев",
                Patronymic = "Витальевич",
                Address = "Россия, г. Челябинск, Школьная ул., д. 4 кв.185",
                Gender = "Муж",
                BirthDate = DateTime.Now.AddYears(random.Next(20, 60) * -1),
                MedicalDistrict = dbContext.MedicalDistricts.Find(random.Next(1, 5))!
            });

            dbContext.Patients.Add(new()
            {
                FirstName = "Иван",
                LastName = "Ручкин",
                Patronymic = "Антонович",
                Address = "Россия, г. Улан-Удэ, Школьный пер., д. 18 кв.168",
                Gender = "Муж",
                BirthDate = DateTime.Now.AddYears(random.Next(20, 60) * -1),
                MedicalDistrict = dbContext.MedicalDistricts.Find(random.Next(1, 5))!
            });

            dbContext.Patients.Add(new()
            {
                FirstName = "Ева",
                LastName = "Зинченко",
                Patronymic = "Ивановна",
                Address = "Россия, г. Йошкар-Ола, Луговая ул., д. 22 кв.132",
                Gender = "Жен",
                BirthDate = DateTime.Now.AddYears(random.Next(20, 60) * -1),
                MedicalDistrict = dbContext.MedicalDistricts.Find(random.Next(1, 5))!
            });

            dbContext.Patients.Add(new()
            {
                FirstName = "Ксения",
                LastName = "Игнатова",
                Patronymic = "Данииловна",
                Address = "Россия, г. Сыктывкар, Школьный пер., д. 22 кв.191",
                Gender = "Жен",
                BirthDate = DateTime.Now.AddYears(random.Next(20, 60) * -1),
                MedicalDistrict = dbContext.MedicalDistricts.Find(random.Next(1, 5))!
            });

            dbContext.Patients.Add(new()
            {
                FirstName = "Серафима",
                LastName = "Тургенева",
                Patronymic = "Ефремовна",
                Address = "Россия, г. Тамбов, Полевая ул., д. 20 кв.203",
                Gender = "Жен",
                BirthDate = DateTime.Now.AddYears(random.Next(20, 60) * -1),
                MedicalDistrict = dbContext.MedicalDistricts.Find(random.Next(1, 5))!
            });

            dbContext.Patients.Add(new()
            {
                FirstName = "Никита",
                LastName = "Рудаков",
                Patronymic = "Алексеевич",
                Address = "Россия, г. Рязань, Шоссейная ул., д. 5 кв.144",
                Gender = "Муж",
                BirthDate = DateTime.Now.AddYears(random.Next(20, 60) * -1),
                MedicalDistrict = dbContext.MedicalDistricts.Find(random.Next(1, 5))!
            });

            dbContext.SaveChanges();
            return dbContext;
        } 
        #endregion
    }
}
