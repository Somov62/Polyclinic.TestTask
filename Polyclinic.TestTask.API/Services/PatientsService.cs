using Microsoft.EntityFrameworkCore;
using Polyclinic.TestTask.API.DataAccess;
using Polyclinic.TestTask.API.Helpers;
using Polyclinic.TestTask.API.Models.Entities;
using Polyclinic.TestTask.API.Requests.Patients;
using Polyclinic.TestTask.API.Responses.Patients;
using System.Reflection;

namespace Polyclinic.TestTask.API.Services
{
    /// <summary>
    /// Сервис операций над сущностями пациентов.
    /// </summary>
    public class PatientsService(PolyclinicDbContext dbContext)
    {
        /// <summary>
        /// Возвращает пациента по указанному id.
        /// </summary>
        public async Task<GetPatientByIdResponse?> GetById(int id, CancellationToken ct)
        {
            var patient = await dbContext.Patients
                .AsNoTracking()
                .Include(nav => nav.MedicalDistrict)
                .FirstOrDefaultAsync(p => p.Id == id, ct);

            if (patient == null)
                return null;

            return new(
                patient.FirstName,
                patient.LastName,
                patient.Patronymic,
                patient.Address,
                patient.Gender,
                patient.BirthDate,
                patient.MedicalDistrict.Id);
        }

        /// <summary>
        /// Проводит выборку сущностей постранично с сортировкой.
        /// </summary>
        public async Task<GetPatientByPageResponse> GetByPage(GetPatientsByPageRequest request, CancellationToken ct)
        {
            if (request.Size < 1)
                throw new ArgumentException("Размерность страницы не может быть меньше одного.");

            if (request.Page < 1)
                throw new ArgumentException("Пагинация начинается с одного.");

            var totalCount = await dbContext.Patients.CountAsync(ct);

            // Выборка моделей.
            var query = dbContext.Patients.AsNoTracking()
                .Include(nav => nav.MedicalDistrict)
                .Select(p => new
                {
                    p.Id,
                    p.FirstName,
                    p.LastName,
                    p.Patronymic,
                    p.Address,
                    p.Gender,
                    p.BirthDate,
                    MedicalDistrict = p.MedicalDistrict.Id
                });

            // Сортировка.
            bool desc = "desc".IgnoreCaseEquals(request.OrderDirection?.Trim());
            var orderFieldName = request.OrderBy?.Trim();

            var orderedQuery = query.OrderBy(p => p.Id);

            if (nameof(PatientDto.FirstName).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.FirstName, desc);

            if (nameof(PatientDto.LastName).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.LastName, desc);

            if (nameof(PatientDto.Patronymic).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.Patronymic, desc);

            if (nameof(PatientDto.Address).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.Address, desc);

            if (nameof(PatientDto.Gender).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.Gender, desc);

            if (nameof(PatientDto.BirthDate).IgnoreCaseEquals(orderFieldName))
                orderedQuery = query.OrderBy(p => p.BirthDate, desc);

            // Пагинация.
            var fullQuery = orderedQuery
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size);

            // Получение данных.
            var dtos = await fullQuery
                .Select(e => new PatientDto(e.Id,
                                            e.FirstName,
                                            e.LastName,
                                            e.Patronymic,
                                            e.Address,
                                            e.Gender,
                                            e.BirthDate,
                                            e.MedicalDistrict))
                .ToListAsync(ct);

            return new(dtos, totalCount);
        }

        /// <summary>
        /// Добавляет пациента в бд.
        /// </summary>
        /// <returns> id созданного пациента. </returns>
        public async Task<int> Create(CreatePatientRequest request, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrEmpty(request.FirstName.Trim()))
                throw new ArgumentException("Заполните имя пациента.");

            if (string.IsNullOrEmpty(request.LastName.Trim()))
                throw new ArgumentException("Заполните фамилию пациента.");

            if (string.IsNullOrEmpty(request.Address.Trim()))
                throw new ArgumentException("Заполните адрес пациента.");

            if (request.Gender != "Муж" && request.Gender != "Жен")
                throw new ArgumentException("Пол пациента может быть \"Муж\" или \"Жен\".");

            var district = await dbContext.MedicalDistricts.FindAsync([request.MedicalDistrict], cancellationToken: ct) ??
                throw new ArgumentException("Указанный участок не существует.");

            var patient = new Patient()
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Patronymic = request.Patronymic?.Trim(),
                Address = request.Address.Trim(),
                Gender = request.Gender.Trim(),
                BirthDate = request.BirthDate,
                MedicalDistrict = district
            };

            await dbContext.Patients.AddAsync(patient, ct);
            await dbContext.SaveChangesAsync(ct);

            return patient.Id;
        }

        /// <summary>
        /// Редактирует данные пациента.
        /// </summary>
        /// <returns> id пациента или null, если пациент не найден. </returns>
        public async Task<int?> Update(int patientId, UpdatePatientRequest request, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var patient = await dbContext.Patients.FindAsync([patientId], cancellationToken: ct);
            if (patient == null)
                return null;

            if (string.IsNullOrEmpty(request.FirstName.Trim()))
                throw new ArgumentException("Заполните имя пациента.");

            if (string.IsNullOrEmpty(request.LastName.Trim()))
                throw new ArgumentException("Заполните фамилию пациента.");

            if (string.IsNullOrEmpty(request.Address.Trim()))
                throw new ArgumentException("Заполните адрес пациента.");

            if (request.Gender != "Муж" && request.Gender != "Жен")
                throw new ArgumentException("Пол пациента может быть \"Муж\" или \"Жен\".");

            var district = await dbContext.MedicalDistricts.FindAsync([request.MedicalDistrict], cancellationToken: ct) ??
                throw new ArgumentException("Указанный участок не существует.");

            patient.FirstName = request.FirstName.Trim();
            patient.LastName = request.LastName.Trim();
            patient.Patronymic = request.Patronymic?.Trim();
            patient.Address = request.Address.Trim();
            patient.Gender = request.Gender.Trim();
            patient.BirthDate = request.BirthDate;
            patient.MedicalDistrict = district;

            await dbContext.SaveChangesAsync(ct);

            return patient.Id;
        }

        /// <summary>
        /// Удаляет пациента из бд.
        /// </summary>
        /// <returns> id пациента или null, если пациент не найден. </returns>
        public async Task<int?> Delete(int patientId, CancellationToken ct)
        {
            var patient = await dbContext.Patients.FindAsync([patientId], cancellationToken: ct);
            if (patient == null)
                return null;

            dbContext.Patients.Remove(patient);

            await dbContext.SaveChangesAsync(ct);

            return patient.Id;
        }
    }
}
