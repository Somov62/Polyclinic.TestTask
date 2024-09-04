namespace Polyclinic.TestTask.API.Requests.Doctors
{
    /// <summary>
    /// Запрос на редактирование данных врача.
    /// </summary>
    /// <param name="FIO"> ФИО </param>
    /// <param name="CabinetId"> Номер кабинета. </param>
    /// <param name="SpecializationId"> Id специализации </param>
    /// <param name="MedicalDistrict"> Номер участка (для участковых врачей) </param>
    public record UpdateDoctorRequest(
        string FIO,
        int CabinetId,
        int SpecializationId,
        int? MedicalDistrict);
}