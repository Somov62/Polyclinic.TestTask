using Polyclinic.TestTask.API.DataAccess;
using Polyclinic.TestTask.API.Models.Entities;
using Polyclinic.TestTask.API.Requests.Doctors;

namespace Polyclinic.TestTask.API.Services.Doctors
{
    /// <summary>
    /// Сервис операций над сущностями врачей.
    /// </summary>
    public class DoctorsService(PolyclinicDbContext dbContext)
    {
        /// <summary>
        /// Добавляет врача в бд.
        /// </summary>
        /// <returns> id созданного врача. </returns>
        public async Task<int> Create(AddDoctorRequest request, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var cabinet = await dbContext.Cabinets.FindAsync([request.CabinetId], cancellationToken: ct) ?? 
                throw new ArgumentException("Указанный кабинет не существует.");

            var specialization = await dbContext.Specializations.FindAsync([request.SpecializationId], cancellationToken: ct) ??
                throw new ArgumentException("Указанная специализация не существует.");

            MedicalDistrict? district = null;
            if (request.MedicalDistrict.HasValue)
            {
                if(!specialization.CanWorkOnMedicalDistrict())
                    throw new ArgumentException("Указать участок возможно только врачу терапевту.");

                district = await dbContext.MedicalDistricts.FindAsync([request.MedicalDistrict.Value], cancellationToken: ct) ??
                    throw new ArgumentException("Указанный участок не существует.");
            }

            var doctor = new Doctor()
            {
                FIO = request.FIO,
                Cabinet = cabinet,
                Specialization = specialization,
                MedicalDistrict = district
            };

            await dbContext.Doctors.AddAsync(doctor, ct);
            await dbContext.SaveChangesAsync(ct);

            return doctor.Id;
        }
    }
}
