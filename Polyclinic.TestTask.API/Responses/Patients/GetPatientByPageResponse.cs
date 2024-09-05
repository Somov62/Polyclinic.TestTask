namespace Polyclinic.TestTask.API.Responses.Patients;

/// <summary>
/// Ответ на запрос страницы пациентов.
/// </summary>
public record GetPatientByPageResponse(IEnumerable<PatientDto> Patients, int TotalCount);

/// <summary>
/// Дто для вывода пациента с осмысленными для пользователя связями.
/// </summary>
/// <param name="FirstName"> Имя. </param>
/// <param name="LastName"> Фамилия. </param>
/// <param name="Patronymic"> Отчество. </param>
/// <param name="Address"> Адрес. </param>
/// <param name="Gender"> Пол. </param>
/// <param name="BirthDate"> Дата рождения. </param>
/// <param name="MedicalDistrict"> Участок, к которому прикреплен. </param>
public record PatientDto(
    int Id,
    string FirstName,
    string LastName,
    string? Patronymic,
    string Address,
    string Gender,
    DateTime BirthDate,
    int? MedicalDistrict);
