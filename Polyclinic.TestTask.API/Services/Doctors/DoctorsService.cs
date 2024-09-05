using Microsoft.EntityFrameworkCore;
using Polyclinic.TestTask.API.DataAccess;
using Polyclinic.TestTask.API.Helpers;
using Polyclinic.TestTask.API.Models.Entities;
using Polyclinic.TestTask.API.Requests.Doctors;
using Polyclinic.TestTask.API.Responses.Doctors;

namespace Polyclinic.TestTask.API.Services.Doctors
{
    /// <summary>
    /// Сервис операций над сущностями врачей.
    /// </summary>
    public class DoctorsService(PolyclinicDbContext dbContext)
    {
        /// <summary>
        /// Возвращает врача по указанному id.
        /// </summary>
        public async Task<GetDoctorByIdResponse?> GetById(int id, CancellationToken ct)
        {
            var doctor = await dbContext.Doctors
                .AsNoTracking()
                .Include(nav => nav.Cabinet)
                .Include(nav => nav.Specialization)
                .Include(nav => nav.MedicalDistrict)
                .FirstOrDefaultAsync(p => p.Id == id, ct);
            
            if (doctor == null)
                return null;

            return new(
                doctor.FIO,
                doctor.Cabinet.Id,
                doctor.Specialization.Id,
                doctor.MedicalDistrict?.Id);
        }

        /// <summary>
        /// Проводит выборку сущностей постранично с сортировкой.
        /// </summary>
        public async Task<GetDoctorByPageResponse> GetByPage(GetDoctorsByPageRequest request, CancellationToken ct)
        {
            if (request.Size < 1)
                throw new ArgumentException("Размерность страницы не может быть меньше одного.");

            if (request.Page < 1)
                throw new ArgumentException("Пагинация начинается с одного.");

            var totalCount = await dbContext.Doctors.CountAsync(ct);

            // Выборка моделей.
            var query = dbContext.Doctors.AsNoTracking()
                .Include(nav => nav.Cabinet)
                .Include(nav => nav.Specialization)
                .Include(nav => nav.MedicalDistrict)
                .Select(p => new { 
                    p.Id, 
                    p.FIO, 
                    CabinetNumber = p.Cabinet.Id,
                    Specialization = p.Specialization.Name, 
                    MedicalDistrict = p.MedicalDistrict == null ? 0 : p.MedicalDistrict.Id });

            // Сортировка.
            bool desc = request.OrderDirection?.Trim().ToLower() == "desc";
            var orderFieldName = request.OrderBy?.Trim().ToLower();

            var orderedQuery = query.OrderBy(p => p.Id);

            if (nameof(DoctorDto.FIO).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.FIO, desc);

            if (nameof(DoctorDto.Specialization).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.Specialization, desc);

            if (nameof(DoctorDto.MedicalDistrict).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.MedicalDistrict, desc);

            if (nameof(DoctorDto.CabinetNumber).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.CabinetNumber, desc);

            // Пагинация.
            var fullQuery = orderedQuery
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            // Получение данных.
            var dtos = await fullQuery
                .Select(e => new DoctorDto(e.Id,
                                           e.FIO,
                                           e.CabinetNumber,
                                           e.Specialization,
                                           e.MedicalDistrict == 0 ? null : e.MedicalDistrict))
                .ToListAsync(ct);

            return new(dtos, totalCount);
        }

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

        /// <summary>
        /// Редактирует данные врача.
        /// </summary>
        /// <returns> id врача или null, если врач не найден. </returns>
        public async Task<int?> Update(int doctorId, UpdateDoctorRequest request, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var doctor = await dbContext.Doctors.FindAsync([doctorId], cancellationToken: ct);
            if (doctor == null)
                return null;

            var cabinet = await dbContext.Cabinets.FindAsync([request.CabinetId], cancellationToken: ct) ??
                throw new ArgumentException("Указанный кабинет не существует.");

            var specialization = await dbContext.Specializations.FindAsync([request.SpecializationId], cancellationToken: ct) ??
                throw new ArgumentException("Указанная специализация не существует.");

            MedicalDistrict? district = null;
            if (request.MedicalDistrict.HasValue)
            {
                if (!specialization.CanWorkOnMedicalDistrict())
                    throw new ArgumentException("Указать участок возможно только врачу терапевту.");

                district = await dbContext.MedicalDistricts.FindAsync([request.MedicalDistrict.Value], cancellationToken: ct) ??
                    throw new ArgumentException("Указанный участок не существует.");
            }

            doctor.FIO = request.FIO;
            doctor.Cabinet = cabinet;
            doctor.Specialization = specialization;
            doctor.MedicalDistrict = district;

            await dbContext.SaveChangesAsync(ct);

            return doctor.Id;
        }

        /// <summary>
        /// Удаляет врача из бд.
        /// </summary>
        /// <returns> id врача или null, если врач не найден. </returns>
        public async Task<int?> Delete(int doctorId, CancellationToken ct)
        {
            var doctor = await dbContext.Doctors.FindAsync([doctorId], cancellationToken: ct);
            if (doctor == null)
                return null;
            
            dbContext.Doctors.Remove(doctor);

            await dbContext.SaveChangesAsync(ct);

            return doctor.Id;
        }
    }
}
