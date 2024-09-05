namespace Polyclinic.TestTask.API.Responses.Doctors;

/// <summary>
/// Ответ на запрос страницы врачей.
/// </summary>
public record GetDoctorByPageResponse(IEnumerable<DoctorDto> Doctors, int TotalCount);


/// <summary>
/// Дто для вывода врача с осмысленными для пользователя связями.
/// </summary>
public record DoctorDto(
    int Id,
    string FIO,
    int CabinetNumber,
    string Specialization,
    int? MedicalDistrict);