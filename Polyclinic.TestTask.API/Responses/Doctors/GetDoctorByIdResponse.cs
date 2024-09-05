namespace Polyclinic.TestTask.API.Responses.Doctors;

/// <summary>
/// Ответ на запрос врача по id.
/// </summary>
/// <param name="FIO"> ФИО </param>
/// <param name="CabinetId"> Номер кабинета. </param>
/// <param name="SpecializationId"> Id специализации </param>
/// <param name="MedicalDistrict"> Номер участка (для участковых врачей) </param>
public record GetDoctorByIdResponse(
    int Id,
    string FIO,
    int CabinetId,
    int SpecializationId,
    int? MedicalDistrict);
